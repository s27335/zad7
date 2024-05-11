namespace WebApp3.Repositories;

public interface IWarehouseRepository
{
    public Task<int> CreateWarehouseProduct(int idProduct, int idWarehouse, int amount);
    public Task UpdateFulfilledAt(int idProduct, int amount);
}