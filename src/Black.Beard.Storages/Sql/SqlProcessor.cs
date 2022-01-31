﻿using System.Data;
using System.Data.Common;

namespace Bb.Sql
{

    public class SqlProcessor
    {

        public SqlProcessor(string connexionString, DbProviderFactory factory)
        {

            this._connexionString = connexionString ?? throw new NullReferenceException(nameof(connexionString));
            this._factory = factory ?? throw new NullReferenceException(nameof(factory));
        }

        public SqlProcessor(DbConnectionStringBuilder builder, DbProviderFactory factory)
        {
            this._builder = builder ?? throw new NullReferenceException(nameof(builder));
            this._factory = factory ?? throw new NullReferenceException(nameof(factory));
        }

        public SqlProcessorResult ExecuteNonQuery(string commandText, params DbParameter[] parameters)
        {

            SqlProcessorResult result = new SqlProcessorResult();
            result.Success = true;

            using (var cnx = GetConnexion())
            {

                var scripts = commandText.Split(new string[] { "GO", "go", "Go", "gO" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var script in scripts)
                    using (var cmd = Getcommand(cnx, script, parameters))
                    {
                        cmd.Connection = cnx;

                        try
                        {

                            var i = cmd.ExecuteNonQuery();
                            if (i > 0)
                                result.InpactedObject += i;

                            result.Success = true;

                        }
                        catch (Exception e)
                        {
                            result.Exception = e;
                            result.Success = false;
                        }

                    }

            }

            return result;

        }

        public IEnumerable<SqlProcessorResult1<T>> ExecuteNonQueryInBlock<T>(string commandText, DbParameter[] parameters, IEnumerable<T> items, Action<T> action)
        {

            using (var cnx = GetConnexion())
                return ExecuteNonQueryInBlock<T>(cnx, commandText, parameters, items, action);

        }

        public IEnumerable<SqlProcessorResult1<T>> ExecuteNonQueryInBlock<T>(DbConnection cnx, string commandText, DbParameter[] parameters, IEnumerable<T> items, Action<T> action)
        {

            List<SqlProcessorResult1<T>> results = new List<SqlProcessorResult1<T>>();

            using (var cmd = Getcommand(cnx, commandText, parameters))
            {

                cmd.Connection = cnx;

                foreach (var item in items)
                {

                    var i = new SqlProcessorResult1<T>();
                    results.Add(i);

                    action(item);

                    try
                    {

                        var r = cmd.ExecuteNonQuery();
                        i.Success = true;

                        if (r > 0)
                            i.InpactedObject += r;

                    }
                    catch (Exception e)
                    {
                        i.Exception = e;
                    }

                }

            }

            return results;

        }

        public IEnumerable<IDataReader> Read(string queryString, params DbParameter[] arguments)
        {

            using (var cnx = GetConnexion())
            using (var query = Getcommand(cnx, queryString, arguments))
            {

                using (var reader = query.ExecuteReader())
                    while (reader.Read())
                        yield return reader;

            }

        }

        public SqlProcessorResult1<T> ExecuteScalar<T>(string commandText, params DbParameter[] parameters)
        {

            using (var cnx = GetConnexion())
            using (var cmd = Getcommand(cnx, commandText, parameters))
            {

                cmd.Connection = cnx;
                var result = new SqlProcessorResult1<T>();

                try
                {
                    var r = cmd.ExecuteScalar();
                    result.Item = (T)Convert.ChangeType(r, typeof(T));
                    result.Success = true;
                }
                catch (Exception e)
                {
                    result.Exception = e;
                }

                return result;

            }
        }

        public DbParameter GetParameterReturnValue(string parameterName, object? value, DbType? dbType = null, int? size = null, byte? scale = null)
        {
            var p = GetParameter(parameterName, value, dbType, size, scale);
            p.Direction = ParameterDirection.ReturnValue;
            return p;
        }

        public DbParameter GetParameterOut(string parameterName, object? value, DbType? dbType = null, int? size = null, byte? scale = null)
        {
            var p = GetParameter(parameterName, value, dbType, size, scale);
            p.Direction = ParameterDirection.Output;
            return p;
        }

        public DbParameter GetParameter(string parameterName, object? value, DbType? dbType = null, int? size = null, byte? scale = null)
        {

            if (_factory == null)
                throw new NullReferenceException("no connexion string initialized");

            var parameter = _factory.CreateParameter();

            parameter.ParameterName = parameterName;
            parameter.Value = value;

            if (dbType.HasValue)
                parameter.DbType = dbType.Value;

            if (size.HasValue)
                parameter.Size = size.Value;

            if (scale.HasValue)
                parameter.Scale = scale.Value;

            return parameter;

        }

        private DbCommand Getcommand(DbConnection cnx, string commandText, DbParameter[] parameters)
        {

            var cmd = this._factory.CreateCommand();
            cmd.CommandText = commandText;
            cmd.Connection = cnx;

            if (parameters != null)
                foreach (var param in parameters)
                    if (param != null)
                        cmd.Parameters.Add(param);

            return cmd;

        }

        public DbConnection GetConnexion()
        {

            if (_factory == null)
                throw new NullReferenceException("no connexion string initialized");

            var cnx = _factory.CreateConnection();

            cnx.ConnectionString = this._connexionString ?? this._builder?.ConnectionString ?? throw new NullReferenceException("no connexion string initialized");
            cnx.Open();

            return cnx;

        }

        private DbConnectionStringBuilder? _builder;
        private readonly DbProviderFactory? _factory;
        private readonly string? _connexionString;

    }


}


