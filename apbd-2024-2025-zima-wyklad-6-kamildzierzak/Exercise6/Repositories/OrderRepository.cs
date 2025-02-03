using Microsoft.Data.SqlClient;

public interface IOrderRepository
{
    Task<(int IdOrder, DateTime CreatedAt)> GetOrder(int idProduct, int amount);
    Task FulfillOrder(int idOrder);
}

public class OrderRepository : IOrderRepository
{
    private readonly string _connectionString;

    public OrderRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<(int IdOrder, DateTime CreatedAt)> GetOrder(int idProduct, int amount)
    {
        using var connection = new SqlConnection(_connectionString);
        var query = "SELECT IdOrder, CreatedAt From [Order] Where IdProduct = @IdProduct AND Amount = @Amount AND FulfilledAt IS NULL";
        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@IdProduct", idProduct);
        command.Parameters.AddWithValue("@Amount", amount);

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        if (!reader.HasRows)
        {
            throw new Exception("No matching order found.");
        }

        await reader.ReadAsync();

        return (reader.GetInt32(0), reader.GetDateTime(1));
    }

    public async Task FulfillOrder(int idOrder)
    {
        using var connection = new SqlConnection(_connectionString);
        var query = "UPDATE [Order] SET FulfilledAt = GETDATE() WHERE IdOrder = @IdOrder";
        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@IdOrder", idOrder);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();
    }

}