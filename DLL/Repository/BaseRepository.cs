using Dapper;
using DLL.IRepository;
using System.Data;
using System.Linq.Expressions;

namespace DLL.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IDbConnection _dbConnection;
        public BaseRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {

                var query = $"SELECT * FROM {typeof(T).Name} WHERE Id = @Id";
                return await _dbConnection.QueryFirstOrDefaultAsync<T>(query, new { Id = id });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            try
            {
                if (filter == null)
                {
                    var query = $"SELECT * FROM {typeof(T).Name}";
                    return await _dbConnection.QueryAsync<T>(query);
                }
                else
                {
                    var query = $"SELECT * FROM {typeof(T).Name} WHERE ";
                    var whereClause = ((LambdaExpression)filter).Body.ToString().Replace("AndAlso", "AND").Replace("OrElse", "OR");
                    query += whereClause.Substring(1, whereClause.Length - 2);
                    return await _dbConnection.QueryAsync<T>(query, filter);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> AddAsync(T entity)
        {
            try
            {
                var properties = typeof(T).GetProperties().Where(p => p.Name != "Id").Select(p => p.Name);
                var parameters = string.Join(", ", properties.Select(p => $"@{p}"));
                var columns = string.Join(", ", properties);
                var query = $"INSERT INTO {typeof(T).Name} ({columns}) VALUES ({parameters})";
                return await _dbConnection.ExecuteAsync(query, entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> UpdateAsync(T entity)
        {
            try
            {
                var properties = typeof(T).GetProperties().Where(p => p.Name != "Id");
                var assignments = properties.Select(p => $"{p.Name} = @{p.Name}");
                var setClause = string.Join(", ", assignments);
                var query = $"UPDATE {typeof(T).Name} SET {setClause} WHERE Id = @Id";
                return await _dbConnection.ExecuteAsync(query, entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                var query = $"DELETE FROM {typeof(T).Name} WHERE Id = @Id";
                return await _dbConnection.ExecuteAsync(query, new { Id = id });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
