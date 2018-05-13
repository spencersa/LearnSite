using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class BaseRepository
    {
        private readonly string _connectionString;
        private const string SqlTimeoutErrorMessage = "experienced a SQL timeout";
        private const string SqlExceptionErrorMessage = "experienced a SQL exception (not a timeout)";

        protected BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected async Task<T> QueryAsync<T>(Func<IDbConnection, Task<T>> queryDatabase)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    return await queryDatabase(connection);
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(($"{GetType().FullName} {SqlTimeoutErrorMessage}"), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(($"{GetType().FullName} {SqlExceptionErrorMessage}"), ex);
            }
        }


        protected async Task<T> UpsertAsync<T>(Func<IDbConnection, SqlTransaction, Task<T>> queryDatabase)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var sqlTransaction = connection.BeginTransaction();
                    var result = await queryDatabase(connection, sqlTransaction);
                    sqlTransaction.Commit();
                    return result;
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(($"{GetType().FullName} {SqlTimeoutErrorMessage}"), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(($"{GetType().FullName} {SqlExceptionErrorMessage}"), ex);
            }
        }

        protected int Upsert(Func<IDbConnection, SqlTransaction, int> queryDatabase)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sqlTransaction = connection.BeginTransaction();
                    var result = queryDatabase(connection, sqlTransaction);
                    sqlTransaction.Commit();
                    return result;
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(($"{GetType().FullName} {SqlTimeoutErrorMessage}"), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(($"{GetType().FullName} {SqlExceptionErrorMessage}"), ex);
            }
        }

        protected T Query<T>(Func<IDbConnection, T> queryDatabase)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    return queryDatabase(connection);
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(($"{GetType().FullName} {SqlTimeoutErrorMessage}"), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(($"{GetType().FullName} {SqlExceptionErrorMessage}"), ex);
            }
        }
    }
}
