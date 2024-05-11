using System.Data.SqlClient;

namespace WebApp3.Repositories;

public class WarehouseRepository : IWarehouseRepository
{
    private IConfiguration _configuration;
    public WarehouseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void UpdateFulfilledAt(int idProduct, int amount)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();

        using var command = new SqlCommand();
        command.Connection = con;
        command.CommandText = "UPDATE [Order] SET fulFilledAt = GETDATE() WHERE idProduct = @idProduct AND amount = @amount;";
        command.Parameters.AddWithValue("@idProduct", idProduct);
        command.Parameters.AddWithValue("amount", amount);
        command.ExecuteNonQuery();
    }
    
    public int CreateWarehouseProduct(int idProduct, int idWarehouse, int amount)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();

        using var command = new SqlCommand();
        command.Connection = con;
        command.CommandText =
            "INSERT INTO Product_Warehouse (idWarehouse, idProduct, idOrder,amount, price, createdAt)" +
            " VALUES (@idWarehouse, @idProduct,(SELECT idOrder FROM [Order] WHERE idProduct = @idProduct AND amount = @amount)," +
            "@amount,((SELECT price FROM Product WHERE idProduct = @idProduct) * @amount),GETDATE());" +
            "SELECT SCOPE_IDENTITY();";
        command.Parameters.AddWithValue("@idWarehouse", idWarehouse);
        command.Parameters.AddWithValue("@idProduct", idProduct);
        command.Parameters.AddWithValue("@amount", amount);

        return Convert.ToInt32(command.ExecuteScalar());
    }

    
}