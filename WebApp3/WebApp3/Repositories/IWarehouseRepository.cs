namespace WebApp3.Repositories;

public interface IWarehouseRepository
{
    public int CreateWarehouseProduct(int idProduct, int idWarehouse, int amount, String createdAt);
}