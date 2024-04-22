using WebApp3.Repositories;

namespace WebApp3.Services;

public class WarehouseService : IWarehouseService
{
    private IWarehouseRepository _warehouseRepository;

    public WarehouseService(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }
    
    public int CreateWarehouseProduct(int idProduct, int idWarehouse, int amount, String createdAt)
    {
        return _warehouseRepository.CreateWarehouseProduct(idProduct,idWarehouse,amount,createdAt);
    }
}