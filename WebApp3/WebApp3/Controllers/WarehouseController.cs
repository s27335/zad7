using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApp3.Services;

namespace WebApp3.Controllers;

[Route("api/warehouse")]
[ApiController]
public class WarehouseController
{
    private IWarehouseService _warehouseService;

    public WarehouseController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }


    [HttpPost]
    public IActionResult CreateWarehouseProduct(int idProduct, int idWarehouse, int amount, String createdAt)
    {
        var primaryKey = _warehouseService.CreateWarehouseProduct(idProduct, idWarehouse, amount, createdAt);
        return Ok(primaryKey);
    }
}