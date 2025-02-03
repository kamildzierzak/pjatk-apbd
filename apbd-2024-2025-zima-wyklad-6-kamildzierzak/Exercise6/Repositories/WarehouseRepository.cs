
using Microsoft.Data.SqlClient;

namespace Exercise6.Repositories;

public interface IWarehouseRepository
{
    Task<bool> WarehouseExists(int idWarehouse);
}


public class WarehouseRepository : IWarehouseRepository
{
    private readonly string _connectionString;

    public WarehouseRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<bool> WarehouseExists(int idWarehouse)
    {
        using var connection = new SqlConnection(_connectionString);
        var query = "SELECT COUNT(1) FROM Warehouse WHERE IdWarehouse = @IdWarehouse";
        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("IdWarehouse", idWarehouse);

        await connection.OpenAsync();

        return (int)await command.ExecuteScalarAsync() > 0;
    }
}
