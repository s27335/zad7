namespace WebApp3.Services;

public interface IWarehouseService
{
    
    public bool ProductExists(int idProduct);
    public bool WarehouseExists(int idWarehouse);
    public bool OrderExists(int idProduct, int amount);
    public bool ValidDate(int idProduct, int amount, DateTime createdAt);
    public bool CompletedOrder(int idProduct, int amount);
    public void UpdateFulfilledAt(int idProduct, int amount);
    public int CreateWarehouseProduct(int idProduct, int idWarehouse, int amount);

}