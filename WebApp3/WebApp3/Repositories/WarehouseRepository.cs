using System.Data.SqlClient;

namespace WebApp3.Repositories;

public class WarehouseRepository : IWarehouseRepository
{
    private IConfiguration _configuration;
    public WarehouseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task UpdateFulfilledAt(int idProduct, int amount)
    {
        await using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();

        await using var command = new SqlCommand();
        command.Connection = con;
        command.CommandText = "UPDATE [Order] SET fulFilledAt = GETDATE() WHERE idProduct = @idProduct AND amount = @amount;";
        command.Parameters.AddWithValue("@idProduct", idProduct);
        command.Parameters.AddWithValue("amount", amount);
        await command.ExecuteNonQueryAsync();
    }
    
    public async Task<int> CreateWarehouseProduct(int idProduct, int idWarehouse, int amount)
    {
        await using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();

        await using var command = new SqlCommand();
        command.Connection = con;
        command.CommandText =
            "INSERT INTO Product_Warehouse (idWarehouse, idProduct, idOrder,amount, price, createdAt)" +
            " VALUES (@idWarehouse, @idProduct,(SELECT idOrder FROM [Order] WHERE idProduct = @idProduct AND amount = @amount)," +
            "@amount,((SELECT price FROM Product WHERE idProduct = @idProduct) * @amount),GETDATE());" +
            "SELECT SCOPE_IDENTITY();";
        command.Parameters.AddWithValue("@idWarehouse", idWarehouse);
        command.Parameters.AddWithValue("@idProduct", idProduct);
        command.Parameters.AddWithValue("@amount", amount);

        return Convert.ToInt32(await command.ExecuteScalarAsync());
    }

    
}