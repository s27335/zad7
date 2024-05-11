using System.Data.SqlClient;
using WebApp3.Repositories;

namespace WebApp3.Services;

public class WarehouseService : IWarehouseService
{
    private IWarehouseRepository _warehouseRepository;
    private IConfiguration _configuration;
    //Sql Connect
    //"Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True"
    //"Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=test;Integrated Security=True"

    public WarehouseService(IWarehouseRepository warehouseRepository, IConfiguration configuration)
    {
        _warehouseRepository = warehouseRepository;
        _configuration = configuration;
    }

    public bool ProductExists(int idProduct)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();

        using var command = new SqlCommand();
        command.Connection = con;
        command.CommandText = "SELECT COUNT(1) FROM Product WHERE idProduct = @idProduct;";
        command.Parameters.AddWithValue("@idProduct", idProduct);
        var exists = (int)command.ExecuteScalar();

        return exists > 0;
    }

    public bool WarehouseExists(int idWarehouse)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();

        using var command = new SqlCommand();
        command.Connection = con;
        command.CommandText = "SELECT COUNT(1) From Warehouse WHERE idWarehouse = @idWarehouse;";
        command.Parameters.AddWithValue("@idWarehouse", idWarehouse);
        var exists = (int)command.ExecuteScalar();

        return exists > 0;
    }

    public bool OrderExists(int idProduct,int amount)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();

        using var command = new SqlCommand();
        command.Connection = con;
        command.CommandText = "SELECT COUNT(1) From Order WHERE idProduct = @idProduct AND amount = @amount;";
        command.Parameters.AddWithValue("@idProduct", idProduct);
        command.Parameters.AddWithValue("@amount", amount);
        var exists = (int)command.ExecuteScalar();

        return exists > 0;
    }

    public bool ValidDate(int idProduct,int amount,DateTime createdAt)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();

        using var command = new SqlCommand();
        command.Connection = con;
        command.CommandText = "SELECT createdAt FROM [Order] WHERE idProduct = @idProduct AND amount = @amount;";
        command.Parameters.AddWithValue("@idProduct", idProduct);
        command.Parameters.AddWithValue("@amount", amount);
        var date = (DateTime)command.ExecuteScalar();

        return date < createdAt;
    }

    public bool CompletedOrder(int idProduct, int amount)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();

        using var command = new SqlCommand();
        command.Connection = con;
        command.CommandText = "SELECT COUNT(1) FROM Product_Warehouse WHERE idProduct = @idProduct AND amount = @amount;";
        command.Parameters.AddWithValue("@idProduct", idProduct);
        command.Parameters.AddWithValue("amount", amount);
        var exists = (int)command.ExecuteScalar();

        return exists > 0;
    }

    public void UpdateFullfilledAt(int idProduct,int amount)
    {
        _warehouseRepository.UpdateFullfilledAt(idProduct,amount);
    }
    
    public int CreateWarehouseProduct(int idProduct, int idWarehouse, int amount, DateTime createdAt)
    { 
        return _warehouseRepository.CreateWarehouseProduct(idProduct,idWarehouse,amount,createdAt);
    }
}