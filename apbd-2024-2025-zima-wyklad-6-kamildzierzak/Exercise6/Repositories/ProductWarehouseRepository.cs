using Microsoft.Data.SqlClient;

namespace Exercise6.Repositories;

public interface IProductWarehouseRepository
{
    Task<bool> OrderExists(int idOrder);
    Task<int> AddProductToWarehouse(int idWarehouse, int idProduct, int idOrder, int amount, decimal price);
}

public class ProductWarehouseRepository : IProductWarehouseRepository
{
    private readonly string _connectionString;

    public ProductWarehouseRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<bool> OrderExists(int idOrder)
    {
        using var connection = new SqlConnection(_connectionString);
        var query = "SELECT COUNT(1) FROM Product_Warehouse WHERE IdOrder = @IdOrder";
        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("IdOrder", idOrder);

        await connection.OpenAsync();

        return (int)await command.ExecuteScalarAsync() > 0;
    }

    public async Task<int> AddProductToWarehouse(int idWarehouse, int idProduct, int idOrder, int amount, decimal price)
    {
        using var connection = new SqlConnection(_connectionString);
        var query = "INSERT INTO Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) VALUES (@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, @CreatedAt); SELECT SCOPE_IDENTITY();";

        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@IdWarehouse", idWarehouse);
        command.Parameters.AddWithValue("@IdProduct", idProduct);
        command.Parameters.AddWithValue("IdOrder", idOrder);
        command.Parameters.AddWithValue("Amount", amount);
        command.Parameters.AddWithValue("Price", price);
        command.Parameters.AddWithValue("CreatedAt", DateTime.UtcNow);

        await connection.OpenAsync();

        var result = await command.ExecuteScalarAsync();
        return Convert.ToInt32(result);
    }

}
