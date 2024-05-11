namespace WebApp3.Services;

public interface IWarehouseService
{
    
    public Task<bool> ProductExists(int idProduct);
    public Task<bool> WarehouseExists(int idWarehouse);
    public Task<bool> OrderExists(int idProduct, int amount);
    public Task<bool> ValidDate(int idProduct, int amount, DateTime createdAt);
    public Task<bool> CompletedOrder(int idProduct, int amount);
    public Task UpdateFulfilledAt(int idProduct, int amount);
    public Task<int> CreateWarehouseProduct(int idProduct, int idWarehouse, int amount);

}