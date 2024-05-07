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

        command.CommandText = "SELECT COUNT(1) FROM Product WHERE idProduct = @idProduct";
        command.Parameters.AddWithValue("@idProduct", idProduct);
        int exists = (int) command.ExecuteScalar();

        return exists > 0;
    }
    
    
    
    public int CreateWarehouseProduct(int idProduct, int idWarehouse, int amount, String createdAt)
    {
        return _warehouseRepository.CreateWarehouseProduct(idProduct,idWarehouse,amount,createdAt);
    }
}