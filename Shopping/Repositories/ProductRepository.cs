using Dapper;
using Shopping.Models;
using Shopping.Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Shopping.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DapperContext _context;

        public ProductRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            using var connection = _context.CreateConnection();
            string query = "SELECT * FROM Products";
            return await connection.QueryAsync<Product>(query);
        }

        public async Task<Product> GetProductById(int id)
        {
            using var connection = _context.CreateConnection();
            string query = "SELECT * FROM Products WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Product>(query, new { Id = id });
        }

        public async Task<int> AddProduct(Product product)
        {
            using var connection = _context.CreateConnection();
            string query = "INSERT INTO Products (Name, Category, Price, Description) VALUES (@Name, @Category, @Price, @Description)";
            return await connection.ExecuteAsync(query, product);
        }

        public async Task<int> UpdateProduct(Product product)
        {
            using var connection = _context.CreateConnection();
            string query = "UPDATE Products SET Name = @Name, Category = @Category, Price = @Price, Description = @Description WHERE Id = @Id";
            return await connection.ExecuteAsync(query, product);
        }

        public async Task<int> DeleteProduct(int id)
        {
            using var connection = _context.CreateConnection();
            string query = "DELETE FROM Products WHERE Id = @Id";
            return await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}
