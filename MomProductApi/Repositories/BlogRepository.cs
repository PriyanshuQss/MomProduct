using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MomProduct.Model;

namespace MomProductApi.Repositories
{
        public interface IBlogRepository
        {
            Task<IEnumerable<Blog>> GetAllAsync();
            Task<Blog?> GetByIdAsync(int id);
            Task<int> AddAsync(Blog Blog);
            Task<int> UpdateAsync(Blog Blog);
            Task<int> DeleteAsync(int id);
        }

        public class BlogRepository : IBlogRepository
        {
            private readonly string _connectionString;

            public BlogRepository(IConfiguration configuration)
            {
                _connectionString = configuration.GetConnectionString("DefaultConnection");
            }

            private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

            public async Task<IEnumerable<Blog>> GetAllAsync()
            {
                const string query = "SELECT * FROM BlogType";
                using var connection = CreateConnection();
                return await connection.QueryAsync<Blog>(query);
            }

            public async Task<Blog?> GetByIdAsync(int id)
            {
                const string query = "SELECT * FROM Blogs WHERE Id = @Id";
                using var connection = CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<Blog>(query, new { Id = id });
            }

            public async Task<int> AddAsync(Blog blog)
            {
                const string query = "INSERT INTO Blogs (Name) VALUES (@Name)";
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, blog);
            }

            public async Task<int> UpdateAsync(Blog blog)
            {
                const string query = "UPDATE Blogs SET Name = @Name WHERE Id = @Id";
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, blog);
            }

            public async Task<int> DeleteAsync(int id)
            {
                const string query = "DELETE FROM Blogs WHERE Id = @Id";
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, new { Id = id });
            }
        }
   
}
