using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.ComponentModel.Translations;
using Bb.Sql;
using Bb.Translations.Services;
using System.Data.Common;
using System.Globalization;

namespace Bb.Translations
{

    [ExposeClass(ConstantsCore.Initialization, ExposedType = typeof(TranslateServiceDataAccess), LifeCycle = IocScopeEnum.Singleton)]
    public class TranslateServiceDataAccess
    {


        public TranslateServiceDataAccess(TranslationConfiguration connection)
        {

            this._tableName = connection.TableName;
            var cnx = connection.GetConnection() ?? throw new NullReferenceException(nameof(connection));
            Sql = SqlProcessor.GetSqlProcessor(cnx);

            _availableCultures = connection.Cultures.ToArray();

        }


        public CultureInfo[] AvailableCultures { get { return _availableCultures; } }


        public DateTimeOffset? LastUpdate { get; private set; }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {

                if (disposing)
                {
                    Sql?.Dispose();
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


        public TranslateServiceDataModel GetNew(string path, string key)
        {
            return new TranslateServiceDataModel()
            {
                _id = Guid.Empty,
                Path = SplitPath(path),
                Key = key,
                IsDirty = true,
            };
        }


        public bool InsertTranslation(TranslateServiceDataModel settings)
        {

            if (settings._id == Guid.Empty)
            {

                settings._id = Guid.NewGuid();

                var results = Sql.ExecuteNonQuery(
                        GetSql(_sql_settings_Insert),
                        Sql.GetParameter("id", settings._id),
                        Sql.GetParameter("path", settings.GetConcatPath),
                        Sql.GetParameter("key", settings.Key),
                        Sql.GetParameter("culture", settings.Culture.IetfLanguageTag),
                        Sql.GetParameter("version", 1),
                        Sql.GetParameter("value", settings.Value)
                       );


                if (results.InpactedObject > 0)
                {

                    var newConfig = Load(settings._id);
                    if (newConfig != null)
                    {
                        settings.CreationDtm = newConfig.CreationDtm;
                        settings.LastUpdate = newConfig.LastUpdate;
                        settings.Value = newConfig.Value;
                        settings.IsDirty = false;
                        return true;
                    }

                }
            }

            return false;

        }

        public void Save(ITranslateService service, Func<bool> cancel)
        {

            var container = (TranslateContainer)service.Container;

            foreach (TranslateServiceDataModel model in container.Parse())
            {

                if (cancel != null &&  cancel())
                    break;

                if (model.Local)
                {
                    InsertTranslation(model);
                }
                else if (model.IsDirty)
                {

                    UpdateTranslation(model);

                }

            }

        }

        public bool UpdateTranslation(TranslateServiceDataModel settings)
        {

            var results = Sql.ExecuteNonQuery(
                GetSql(_sql_Update),
                    Sql.GetParameter("id", settings._id),
                    Sql.GetParameter("path", settings.GetConcatPath),
                    Sql.GetParameter("key", settings.Key),
                    Sql.GetParameter("value", settings.Value),
                    Sql.GetParameter("version", 1)
                );

            if (results.InpactedObject > 0)
            {
                var newConfig = Load(settings._id);
                if (newConfig != null)
                {
                    settings.LastUpdate = newConfig.LastUpdate;
                    settings.Version = newConfig.Version;
                    settings.Path = ClonePath(newConfig.Path);
                    settings.Key = newConfig.Key;
                    settings.Value = newConfig.Value;
                    settings.IsDirty = false;
                    return true;
                }
            }



            return false;

        }

        public TranslateServiceDataModel? Load(Guid id)
        {

            var queryString = GetSql(_sql_selectAll) + " WHERE [_id] = @id";
            var argument = Sql.GetParameter("id", id);

            foreach (var item in Sql.Read(queryString, argument))
            {

                var row = new TranslateServiceDataModel()
                {
                    _id = item.GetGuid(item.GetOrdinal("_id")),
                    Path = SplitPath(item.GetString(item.GetOrdinal("Path"))),
                    Key = item.GetString(item.GetOrdinal("Key")),
                    Value = item.GetString(item.GetOrdinal("Value")),
                    Culture = GetCulture(item.GetString(item.GetOrdinal("culture"))),
                    Version = item.GetInt32(item.GetOrdinal("Version")),
                    CreationDtm = item.GetDateTime(item.GetOrdinal("CreationDtm")),
                    LastUpdate = item.GetDateTime(item.GetOrdinal("LastUpdate")),
                };

                row.IsDirty = false;
                CheckLastDate(row);

                return row;

            }

            return null;

        }


        //public void Append(TranslateServiceDataModel item)
        //{
        //    _toAppend.Add(item, _availableCultures);
        //}


        //public IEnumerable<TranslateServiceDataModel> ToAdd()
        //{

        //    foreach (var item1 in _toAppend)
        //        foreach (var item2 in item1.Value)
        //            foreach (var item3 in item2.Value)
        //                yield return item3.Value;

        //}


        public string[] SplitPath(string path)
        {
            if (path != null)
                return path.Trim().Split('.', StringSplitOptions.RemoveEmptyEntries);
            return new string[0];
        }

        private string[] ClonePath(string[] path)
        {
            return path?.ToArray() ?? new string[0];
        }

        private CultureInfo GetCulture(string key)
        {
            var result = CultureInfo.GetCultureInfo(key);
            return result;
        }


        public IEnumerable<TranslateServiceDataModel> GetAll()
        {

            var query = GetSql(_sql_selectAll);

            DbParameter? parameter = null;
            if (LastUpdate.HasValue)
            {
                query += " WHERE [LastUpdate] = @lastUpdate";
                parameter = Sql.GetParameter("lastUpdate", LastUpdate.Value);
            }

            foreach (var item in Sql.Read(query, parameter))
            {

                var row = new TranslateServiceDataModel()
                {
                    _id = item.GetGuid(item.GetOrdinal("_id")),
                    Path = SplitPath(item.GetString(item.GetOrdinal("Path"))),
                    Key = item.GetString(item.GetOrdinal("Key")),
                    Value = item.GetString(item.GetOrdinal("Value")),
                    Culture = GetCulture(item.GetString(item.GetOrdinal("culture"))),
                    Version = item.GetInt32(item.GetOrdinal("Version")),
                    CreationDtm = item.GetDateTime(item.GetOrdinal("CreationDtm")),
                    LastUpdate = item.GetDateTime(item.GetOrdinal("LastUpdate")),
                };
                row.IsDirty = false;
                CheckLastDate(row);

                yield return row;

            }

        }

        public SqlProcessor Sql { get; }

        public bool CreateTables()
        {
            var results = Sql.ExecuteNonQuery(GetSql(_sql_create));
            return results.Success;
        }

        private string GetSql(string sql)
        {
            return sql.Replace("%TableName%", this._tableName);
        }

        private void CheckLastDate(TranslateServiceDataModel row)
        {

            if (row.LastUpdate > this.LastUpdate || !this.LastUpdate.HasValue)
                this.LastUpdate = row.LastUpdate;

        }


        private readonly string _tableName;

        private string _sql_settings_Insert = @"

INSERT INTO [dbo].[%TableName%] ( [_id], [Path], [Key], [Value], [Culture], [Version], [CreationDtm], [LastUpdate]) 
VALUES ( @id, @path, @key, @value, @culture, @version, SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET())
GO
INSERT INTO [dbo].[%TableName%_history] ( [_id], [Path], [Key], [Value], [Culture], [Version], [CreationDtm]) 
VALUES ( @id, @path, @key, @value, @culture, @version, SYSDATETIMEOFFSET())
";


        private string _sql_Update = @"
UPDATE [dbo].[%TableName%] 
SET [Path] = @path, [Key] = @key, [Value] = @value, [LastUpdate] = SYSDATETIMEOFFSET(), [Version] = @version + 1  
WHERE [_id] = @id AND [Version] = @version";

        private string _sql_selectAll = @"SELECT [_id], [Path], [Key], [Value], [culture], [Version], [CreationDtm], [LastUpdate] FROM [%TableName%] WITH (NOLOCK)";




        private string _sql_create =
 @"

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[%TableName%](
	[_id] [uniqueidentifier] NOT NULL,
	[Path] [varchar](250) NOT NULL,
	[Key] [varchar](150) NOT NULL,
	[Value] [nvarchar](1024) NOT NULL,
	[culture] [varchar](15) NOT NULL,
	[Version] [int] NOT NULL,
	[CreationDtm] [datetime] NOT NULL,
	[LastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_%TableName%] PRIMARY KEY CLUSTERED 
(
	[_id] ASC
)WITH 
	(PAD_INDEX = OFF
	, STATISTICS_NORECOMPUTE = OFF
	, IGNORE_DUP_KEY = OFF
	, ALLOW_ROW_LOCKS = ON
	, ALLOW_PAGE_LOCKS = ON
	, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
	) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [NonClusteredIndexLastUpdate] ON [dbo].[translations]
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
        //private containerByPath _toAppend = new containerByPath();
        private CultureInfo[] _availableCultures;

        //private class containerByPath : Dictionary<string, containerByKey>
        //{
        //    internal void Add(TranslateServiceDataModel item, CultureInfo[] _availableCultures)
        //    {

        //        var path = item.GetConcatPath;

        //        if (!this.TryGetValue(path, out var container))
        //            this.Add(item.GetConcatPath, container = new containerByKey());

        //        container.Add(item, _availableCultures);

        //    }
        //}

        //private class containerByKey : Dictionary<string, containerByCulture>
        //{

        //    internal void Add(TranslateServiceDataModel item, CultureInfo[] _availableCultures)
        //    {

        //        var key = item.Key;

        //        if (!this.TryGetValue(key, out var container))
        //            this.Add(key, container = new containerByCulture());
        //        container.Add(item, _availableCultures);

        //    }

        //}

        //private class containerByCulture : Dictionary<CultureInfo, TranslateServiceDataModel>
        //{

        //    internal void Add(TranslateServiceDataModel item, CultureInfo[] _availableCultures)
        //    {

        //        var culture = item.Culture;

        //        if (!this.TryGetValue(culture, out var container))
        //            this.Add(culture, item);

        //        else if (container._id != item._id)
        //        {

        //        }

        //    }

        //}

    }

}


