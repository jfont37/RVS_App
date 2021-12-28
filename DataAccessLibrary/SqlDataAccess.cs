using Dapper;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace DataAccessLibrary
{
    public class SqlDataAccess
    {
        private readonly IConfiguration _config;
        public string ConnectionStringName { get; set; } = "MyConnection";
        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<T>> LoadData<T, U>(string sql, U parameters)
        {
            var connectionString = _config.GetConnectionString(ConnectionStringName);

            using (var connection = new NpgsqlConnection(connectionString))
            {
                IEnumerable<T> data = await connection.QueryAsync<T>(sql, parameters);
                return data.ToList();
            }
        }

        public async Task SaveData<T>(string sql, T parameters)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);

            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            using var cmd = new NpgsqlCommand();
            {
                await connection.ExecuteAsync(sql, parameters);
            }
        }

    }
}