using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MomProduct.Model;


namespace MomProductApi.Repositories
{
    public interface IBlogTypeRepository
    {
        Task<IEnumerable<BlogType>> GetAllAsync();
        Task<BlogType?> GetByIdAsync(int id);
        Task<int> AddAsync(BlogType BlogType);
        Task<int> UpdateAsync(BlogType BlogType);
        Task<int> DeleteAsync(int id);
    }

    public class BlogTypeRepository : IBlogTypeRepository
    {
        private readonly string _connectionString;

        public BlogTypeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task<IEnumerable<BlogType>> GetAllAsync()
        {
            const string query = "SELECT * FROM BlogType";
            using var connection = CreateConnection();
            return await connection.QueryAsync<BlogType>(query);
        }

        public async Task<BlogType?> GetByIdAsync(int id)
        {
            const string query = "SELECT * FROM BlogType WHERE Id = @Id";
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<BlogType>(query, new { Id = id });
        }

        public async Task<int> AddAsync(BlogType blogType)
        {
            const string query = "INSERT INTO BlogType (BlogName, Description) VALUES (@BlogName, @Descriptione)";
            using var connection = CreateConnection();
            return await connection.ExecuteAsync(query, blogType);
        }

        public async Task<int> UpdateAsync(BlogType blogType)
        {
            const string query = "UPDATE BlogType SET BlogName = @BlogName AND Description = @Description WHERE Id = @Id";
            using var connection = CreateConnection();
            return await connection.ExecuteAsync(query, blogType);
        }

        public async Task<int> DeleteAsync(int id)
        {
            const string query = "DELETE FROM BlogType WHERE Id = @Id";
            using var connection = CreateConnection();
            return await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}
