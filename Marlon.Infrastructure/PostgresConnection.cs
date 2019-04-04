using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Marlon.Infrastructure
{
    public interface IPostgresConnection
    {
        void writeData(string statement, object paramenters);
        IEnumerable<T> readData<T>(string statement, object parameters);
    }

    public class PostgresConnection : IPostgresConnection
    {
        private readonly string _connString;

        public PostgresConnection(IConfiguration configuration)
        {
            _connString = configuration["marlon-psql-connString"];
        }

        public void writeData(string statement, object parameters)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();
                conn.Execute(statement, parameters);
            }
        }

        public IEnumerable<T> readData<T>(string statement, object parameters)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();
                var result = conn.Query<T>(statement, parameters);
                
                return result;
            }
        }
    }
}
