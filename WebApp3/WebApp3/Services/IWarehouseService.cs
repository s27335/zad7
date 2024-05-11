namespace WebApp3.Services;

public interface IWarehouseService
{
    public int CreateWarehouseProduct(int idProduct, int idWarehouse, int amount, DateTime createdAt);
    public bool ProductExists(int idProduct);
    public bool WarehouseExists(int idWarehouse);
    public bool OrderExists(int idProduct, int amount);
}