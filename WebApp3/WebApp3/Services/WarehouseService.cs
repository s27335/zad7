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

    public async Task<bool> ProductExists(int idProduct)
    {
        await using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();

        await using var command = new SqlCommand();
        command.Connection = con;
        command.CommandText = "SELECT COUNT(1) FROM Product WHERE idProduct = @idProduct;";
        command.Parameters.AddWithValue("@idProduct", idProduct);
        var exists = (int)await command.ExecuteScalarAsync();

        return exists > 0;
    }

    public async Task<bool> WarehouseExists(int idWarehouse)
    {
        await using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();

        await using var command = new SqlCommand();
        command.Connection = con;
        command.CommandText = "SELECT COUNT(1) From Warehouse WHERE idWarehouse = @idWarehouse;";
        command.Parameters.AddWithValue("@idWarehouse", idWarehouse);
        var exists = (int)await command.ExecuteScalarAsync();

        return exists > 0;
    }

    public async Task<bool> OrderExists(int idProduct,int amount)
    {
        await using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();

        await using var command = new SqlCommand();
        command.Connection = con;
        command.CommandText = "SELECT COUNT(1) From [Order] WHERE idProduct = @idProduct AND amount = @amount;";
        command.Parameters.AddWithValue("@idProduct", idProduct);
        command.Parameters.AddWithValue("@amount", amount);
        var exists = (int)await command.ExecuteScalarAsync();

        return exists > 0;
    }

    public async Task<bool> ValidDate(int idProduct,int amount,DateTime createdAt)
    {
        await using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();

        await using var command = new SqlCommand();
        command.Connection = con;
        command.CommandText = "SELECT createdAt FROM [Order] WHERE idProduct = @idProduct AND amount = @amount;";
        command.Parameters.AddWithValue("@idProduct", idProduct);
        command.Parameters.AddWithValue("@amount", amount);
        var date = (DateTime)await command.ExecuteScalarAsync();

        return date < createdAt;
    }

    public async Task<bool> CompletedOrder(int idProduct, int amount)
    {
        await using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();

        await using var command = new SqlCommand();
        command.Connection = con;
        command.CommandText = "SELECT COUNT(1) FROM Product_Warehouse WHERE idProduct = @idProduct AND amount = @amount;";
        command.Parameters.AddWithValue("@idProduct", idProduct);
        command.Parameters.AddWithValue("amount", amount);
        var exists = (int)await command.ExecuteScalarAsync();
        
        return exists > 0;
    }

    public async Task UpdateFulfilledAt(int idProduct,int amount)
    {
        await _warehouseRepository.UpdateFulfilledAt(idProduct,amount);
    }
    
    public Task<int> CreateWarehouseProduct(int idProduct, int idWarehouse, int amount)
    { 
        return _warehouseRepository.CreateWarehouseProduct(idProduct,idWarehouse,amount);
    }
}