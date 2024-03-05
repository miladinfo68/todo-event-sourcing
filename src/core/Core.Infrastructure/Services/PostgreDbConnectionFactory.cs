using Core.Application.Services;
using Npgsql;
using System.Data;

namespace Core.Infrastructure.Services
{
    public class PostgreDbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;
        private IDbConnection _connection;
        public PostgreDbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Dispose()
        {
            if (this._connection != null && this._connection.State == ConnectionState.Open)
            {
                this._connection.Dispose();
            }
        }

        public IDbConnection GetOpenConnection()
        {
            if (this._connection == null || this._connection.State != ConnectionState.Open)
            {
                this._connection = new NpgsqlConnection(_connectionString);
                this._connection.Open();
            }

            return this._connection;
        }
    }
}
