using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Items.Models;

namespace Items.DAL
{
    public class ItemsRepository : IItemsRepository
    {
        private readonly SqlConnection _sqlConnection;

        public ItemsRepository(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
        }

        public async Task Create(Item obj)
        {
            const string sql = "INSERT INTO Items (Name) VALUES (@Name)";
            await _sqlConnection.QueryAsync(sql, new
            {
                Name = obj.Name
            });
        }

        public async Task<Item> Get(int id)
        {
            const string sql = "SELECT * FROM Items WHERE id=@Id";
            var item = await _sqlConnection.QueryFirstOrDefaultAsync<Item>(sql, new
            {
                Id = id
            });
            return item;
        }

        public async Task Update(Item obj)
        {
            const string sql = "UPDATE Items SET Name=@Name WHERE Id=@Id";
            await _sqlConnection.QueryAsync(sql, new
            {
                Name = obj.Name,
                Id = obj.Id
            });
        }

        public async Task Delete(int id)
        {
            const string sql = "DELETE FROM Items WHERE Id=@Id";
            await _sqlConnection.QueryAsync(sql, new
            {
                Id = id
            });
        }

        public async Task<IEnumerable<Item>> GetAll()
        {
            const string sql = "SELECT * FROM Items";
            var items = await _sqlConnection.QueryAsync<Item>(sql);
            return items;
        }
    }
}
