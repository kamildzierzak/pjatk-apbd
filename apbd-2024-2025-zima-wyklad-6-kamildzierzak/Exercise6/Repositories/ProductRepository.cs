using Microsoft.Data.SqlClient;

public interface IProductRepository
{
    Task<decimal> GetProductPrice(int idProduct);
    Task<bool> ProductExists(int idProduct);
}

public class ProductRepository : IProductRepository
{
    private readonly string _connectionString;

    public ProductRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<decimal> GetProductPrice(int idProduct)
    {
        using var connection = new SqlConnection(_connectionString);
        var query = "SELECT Price From Product WHERE IdProduct = @IdProduct";
        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@IdProduct", idProduct);

        await connection.OpenAsync();

        var result = await command.ExecuteScalarAsync();

        if (result == null)
        {
            throw new Exception($"Product with id {idProduct} does not exist");
        }

        return Convert.ToDecimal(result);
    }

    public async Task<bool> ProductExists(int idProduct)
    {
        using var connection = new SqlConnection(_connectionString);
        var query = "SELECT COUNT(1) FROM Product WHERE IdProduct = @IdProduct";
        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@IdProduct", idProduct);

        await connection.OpenAsync();

        return (int)await command.ExecuteScalarAsync() > 0;
    }

}