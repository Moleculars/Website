using Microsoft.Data.SqlClient;
using System.Data;

namespace Bb.Storages.ConfigurationProviders.SqlServer
{



    public class SqlServerConfigurationDataAccess : IDisposable
    {


        public SqlServerConfigurationDataAccess(string SettingConnectionString, int refreshInterval)
        {
            ConnectionString = SettingConnectionString;
            SqlServerWatcher = new SqlServerPeriodicalWatcher(TimeSpan.FromSeconds(refreshInterval));
        }


        public string ConnectionString { get; }


        public ISqlServerWatcher SqlServerWatcher { get; set; }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
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

            using (var connection = new SqlConnection(this.ConnectionString))
            {

                var query = new SqlCommand(sql_Insert, connection);
                query.Parameters.Add(new SqlParameter("sectionName", settings.SectionName));
                query.Parameters.Add(new SqlParameter("context", settings.Context));
                query.Parameters.Add(new SqlParameter("kind", settings.Kind));
                query.Parameters.Add(new SqlParameter("version", 1));
                query.Parameters.Add(new SqlParameter("value", settings.Value));

                query.Connection.Open();

                var result = query.ExecuteNonQuery();

                return result > 0;

            }

        }


        public bool UpdateConfiguration(ConfigurationSettings settings)
        {

            using (var connection = new SqlConnection(this.ConnectionString))
            {

                var query = new SqlCommand(sql_Update, connection);
                query.Parameters.Add(new SqlParameter("sectionName", settings.SectionName));
                query.Parameters.Add(new SqlParameter("value", settings.Value));
                query.Parameters.Add(new SqlParameter("version", settings.Version));


                query.Connection.Open();

                var result = query.ExecuteNonQuery();

                if ( result > 0)
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

            }

            return false;

        }


        public ConfigurationSettings? LoadConfiguration(string sectionName)
        {

            foreach (var item in this.Read(sql_selectAll + "WHERE [SectionName] = @sectionName", new SqlParameter("sectionName", sectionName)))
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

                return row;

            }

            return null;

        }


        public Dictionary<string, ConfigurationSettings> LoadConfigurations()
        {

            var datas = new Dictionary<string, ConfigurationSettings>();

            foreach (var item in this.Read(sql_selectAll))
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

                datas.Add(row.SectionName, row);

            }

            return datas;
        }


        private IEnumerable<IDataReader> Read(string queryString, params SqlParameter[] arguments)
        {

            using (var connection = new SqlConnection(this.ConnectionString))
            {

                var query = new SqlCommand(queryString, connection);

                foreach (var item in arguments)
                    query.Parameters.Add(item);

                query.Connection.Open();

                using (var reader = query.ExecuteReader())
                    while (reader.Read())
                        yield return reader;

            }

        }


        private string sql_Insert = "INSERT INTO [dbo].[settings] ([SectionName], [Context], [Kind], [Version], [Value], [CreationDtm], [LastUpdate]) VALUES (@sectionName, @context, @kind, @version, @value, SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET())";
        private string sql_Update = "UPDATE [dbo].[settings] SET [Value] = @value, [LastUpdate] = SYSDATETIMEOFFSET(), [Version] = @version + 1  WHERE [SectionName]=@sectionName AND [Version] = @version";
        private string sql_selectAll = "SELECT [SectionName], [Context], [Kind], [Version], [Value], [CreationDtm], [LastUpdate] FROM [Settings]";        
        private string sql_create =
 @"
    SET ANSI_NULLS ON
    GO

    SET QUOTED_IDENTIFIER ON
    GO

CREATE TABLE [dbo].[settings](
	[SectionName] [varchar](100) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
	[Context] [varchar](100) NOT NULL,
	[Kind] [varchar](20) NOT NULL,
	[Version] [int] NOT NULL,
	[CreationDtm] [datetime] NOT NULL,
	[LastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_settings] PRIMARY KEY CLUSTERED 
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
";

        private bool disposedValue;


    }


}


