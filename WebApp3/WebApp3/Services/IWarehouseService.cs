namespace WebApp3.Services;

public interface IWarehouseService
{
    public int CreateWarehouseProduct(int idProduct, int idWarehouse, int amount, String createdAt);
    public bool ProductExists(int idProduct);
}