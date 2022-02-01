using Bb.Sql;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace Bb.Storages.ConfigurationProviders.SqlServer
{



    public class SqlServerConfigurationDataAccess : IDisposable
    {

        public SqlServerConfigurationDataAccess(ConnectionSettings settings, string connectionStringName, int? refreshInterval, string tableName = "settings")
        {

            this._tableName = tableName;
            _sql =  SqlProcessor.GetSqlProcessor(settings.ConnectionStringSettings.GetConnectionString(connectionStringName));

            if (refreshInterval.HasValue)
                SqlServerWatcher = new SqlServerPeriodicalWatcher(TimeSpan.FromSeconds(refreshInterval.Value));

        }

        public SqlServerConfigurationDataAccess(string SettingConnectionString, int? refreshInterval, string tableName = "settings")
        {
            this._tableName = tableName;
            _sql = SqlProcessor.GetSqlProcessor(SettingConnectionString, SqlClientFactory.Instance);

            if (refreshInterval.HasValue)
                SqlServerWatcher = new SqlServerPeriodicalWatcher(TimeSpan.FromSeconds(refreshInterval.Value));

        }

        public ISqlServerWatcher? SqlServerWatcher { get; }

        public DateTimeOffset? LastUpdate { get; private set; }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    SqlServerWatcher?.Dispose();
                    // TODO: supprimer l'état managé (objets managés)
                }

                // TODO: libérer les ressources non managées (objets non managés) et substituer le finaliseur
                // TODO: affecter aux grands champs une valeur null
                disposedValue = true;
            }
        }

        // // TODO: substituer le finaliseur uniquement si 'Dispose(bool disposing)' a du code pour libérer les ressources non managées
        // ~SqlServerConfigurationDataAccess()
        // {
        //     // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }


        public ConfigurationSettings GetNew(string sectionName, string context, string kind)
        {
            return new ConfigurationSettings()
            {
                SectionName = sectionName,
                Context = context,
                Kind = kind,
            };
        }


        public bool InsertConfiguration(ConfigurationSettings settings)
        {

            var results = _sql.ExecuteNonQuery(
                   GetSql(_sql_Insert),
                    _sql.GetParameter("sectionName", settings.SectionName),
                    _sql.GetParameter("context", settings.Context),
                    _sql.GetParameter("kind", settings.Kind),
                    _sql.GetParameter("version", 1),
                    _sql.GetParameter("value", settings.Value)
                   );

            return results.InpactedObject > 0;

        }


        public bool UpdateConfiguration(ConfigurationSettings settings)
        {

            var results = _sql.ExecuteNonQuery(
                GetSql(_sql_Update),
                _sql.GetParameter("sectionName", settings.SectionName),
                _sql.GetParameter("value", settings.Value),
                _sql.GetParameter("version", settings.Version)
                );

            if (results.InpactedObject > 0)
            {
                var newConfig = LoadConfiguration(settings.SectionName);
                if (newConfig != null)
                {
                    settings.LastUpdate = newConfig.LastUpdate;
                    settings.Version = newConfig.Version;
                    settings.Value = newConfig.Value;
                    settings.IsDirty = false;
                    return true;
                }
            }



            return false;

        }


        public ConfigurationSettings? LoadConfiguration(string sectionName)
        {

            var queryString = GetSql(_sql_selectAll) + "WHERE [SectionName] = @sectionName";
            var argument = _sql.GetParameter("sectionName", sectionName);

            foreach (var item in _sql.Read(queryString, argument))
            {

                var row = new ConfigurationSettings()
                {
                    SectionName = item.GetString(item.GetOrdinal("SectionName")),
                    Context = item.GetString(item.GetOrdinal("Context")),
                    Kind = item.GetString(item.GetOrdinal("Kind")),
                    Version = item.GetInt32(item.GetOrdinal("Version")),
                    Value = item.GetString(item.GetOrdinal("Value")),
                    CreationDtm = item.GetDateTime(item.GetOrdinal("CreationDtm")),
                    LastUpdate = item.GetDateTime(item.GetOrdinal("LastUpdate")),
                };

                CheckLastDate(row);

                return row;

            }

            return null;

        }


        public Dictionary<string, ConfigurationSettings> LoadConfigurations()
        {

            var datas = new Dictionary<string, ConfigurationSettings>();

            var query = GetSql(_sql_selectAll);

            DbParameter? parameter = null;
            if (LastUpdate.HasValue)
            {
                query += " WHERE [LastUpdate] = @lastUpdate";
                parameter = _sql.GetParameter("lastUpdate", LastUpdate.Value);
            }

            foreach (var item in _sql.Read(query, parameter))
            {

                var row = new ConfigurationSettings()
                {
                    SectionName = item.GetString(item.GetOrdinal("SectionName")),
                    Context = item.GetString(item.GetOrdinal("Context")),
                    Kind = item.GetString(item.GetOrdinal("Kind")),
                    Version = item.GetInt32(item.GetOrdinal("Version")),
                    Value = item.GetString(item.GetOrdinal("Value")),
                    CreationDtm = item.GetDateTime(item.GetOrdinal("CreationDtm")),
                    LastUpdate = item.GetDateTime(item.GetOrdinal("LastUpdate")),
                };

                CheckLastDate(row);

                datas.Add(row.SectionName, row);

            }

            return datas;
        }

        public bool CreateTables()
        {
            var results = _sql.ExecuteNonQuery(GetSql(_sql_create));
            return results.Success;
        }

        private string GetSql(string sql)
        {
            return sql.Replace("%TableName%", this._tableName);
        }

        private void CheckLastDate(ConfigurationSettings row)
        {

            if (row.LastUpdate > this.LastUpdate || !this.LastUpdate.HasValue)
                this.LastUpdate = row.LastUpdate;

        }

        private readonly string _tableName;
        private readonly SqlProcessor _sql;

        private string _sql_Insert = "INSERT INTO [dbo].[%TableName%] ([SectionName], [Context], [Kind], [Version], [Value], [CreationDtm], [LastUpdate]) VALUES (@sectionName, @context, @kind, @version, @value, SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET())";
        private string _sql_Update = "UPDATE [dbo].[%TableName%] SET [Value] = @value, [LastUpdate] = SYSDATETIMEOFFSET(), [Version] = @version + 1  WHERE [SectionName]=@sectionName AND [Version] = @version";
        private string _sql_selectAll = "SELECT [SectionName], [Context], [Kind], [Version], [Value], [CreationDtm], [LastUpdate] FROM [%TableName%]";
        private string _sql_create =
 @"

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[%TableName%](
	[SectionName] [varchar](100) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
	[Context] [varchar](100) NOT NULL,
	[Kind] [varchar](20) NOT NULL,
	[Version] [int] NOT NULL,
	[CreationDtm] [datetime] NOT NULL,
	[LastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_%TableName%] PRIMARY KEY CLUSTERED 
(
	[SectionName] ASC
)WITH 
	(PAD_INDEX = OFF
	, STATISTICS_NORECOMPUTE = OFF
	, IGNORE_DUP_KEY = OFF
	, ALLOW_ROW_LOCKS = ON
	, ALLOW_PAGE_LOCKS = ON
	, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
	) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [NonClusteredIndexLastUpdate] ON [dbo].[%TableName%]
(
	[LastUpdate] ASC
)WITH 
    ( PAD_INDEX = OFF
    , STATISTICS_NORECOMPUTE = OFF
    , SORT_IN_TEMPDB = OFF
    , DROP_EXISTING = OFF
    , ONLINE = OFF
    , ALLOW_ROW_LOCKS = ON
    , ALLOW_PAGE_LOCKS = ON
    , OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
) ON [PRIMARY]
GO
";

        private bool disposedValue;


    }


}


