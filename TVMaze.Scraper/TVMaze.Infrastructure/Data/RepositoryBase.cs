using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TVMaze.Core.Contracts;

namespace TVMaze.Infrastructure.Data
{
    public abstract class RepositoryBase<T> : IAsyncGenericRepository<T> where T : EntityBase
    {

        private readonly string _tableName;
        protected string _connectionString;

        public RepositoryBase(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _tableName = typeof(T).Name;
        }

        public async Task<int> AddAsync(T entity)
        {
            try
            {
                var columns = GetColumns();
                var stringOfColumns = string.Join(", ", columns);
                var stringOfParameters = string.Join(", ", columns.Select(e => "@" + e));
                var query = $"INSERT INTO {_tableName} ({stringOfColumns}) VALUES ({stringOfParameters})";
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    return await conn.ExecuteAsync(query, entity);

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            
        }
        public async Task DeleteAsync(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                await conn.ExecuteAsync($"DELETE FROM {_tableName} WHERE [Id] = @Id", new { Id = id });
            }
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {

                    conn.Open();
                    return await conn.QueryAsync<T>($"SELECT * FROM {_tableName}");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

          
        }
        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    var data = await conn.QueryAsync<T>($"SELECT * FROM {_tableName} WHERE Id = @Id", new { Id = id });
                    return data.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

           

        }
        public async Task UpdateAsync(T entity)
        {
            var columns = GetColumns();
            var stringOfColumns = string.Join(", ", columns.Select(e => $"{e} = @{e}"));
            var query = $"UPDATE {_tableName} SET {stringOfColumns} WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                await conn.ExecuteAsync(query, entity);
            }
        }
        public async Task<IEnumerable<T>> Query(string where = null)
        {
            var query = $"SELECT * FROM {_tableName} ";

            if (!string.IsNullOrWhiteSpace(where))
                query += where;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return await conn.QueryAsync<T>(query);

            }
        }
        public async Task<IEnumerable<T>> QueryPerPage(int page)
        {
            int pageLimit = 20;
            int firstPage = page == 1 ? 1 : (pageLimit * (page - 1)) + 1;
            int lastPage = pageLimit * page + 1;

            var query = $"SELECT  * FROM(SELECT ROW_NUMBER() OVER(ORDER BY Id) AS RowNum, * " +
                $"FROM {_tableName} ) AS OrderedRow " +
                $"WHERE RowNum >= {firstPage} AND RowNum < {lastPage} " +
                "ORDER BY RowNum";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return await conn.QueryAsync<T>(query);

            }
        }

        private IEnumerable<string> GetColumns()
        {
            return typeof(T)
                    .GetProperties()
                    .Where(e => !e.PropertyType.GetTypeInfo().IsGenericType)
                    .Select(e => e.Name);

        }
    }
}

