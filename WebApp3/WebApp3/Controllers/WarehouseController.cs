using Microsoft.AspNetCore.Mvc;
using WebApp3.Services;

namespace WebApp3.Controllers;

[Route("api/warehouse")]
[ApiController]
public class WarehouseController : ControllerBase
{
    private IWarehouseService _warehouseService;

    public WarehouseController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    //Ogarnac async !!!

    [HttpPost]
    public IActionResult CreateWarehouseProduct(int idProduct, int idWarehouse, int amount, String createdAt)
    {
        if (_warehouseService.ProductExists(idProduct))
        {
            var primaryKey = _warehouseService.CreateWarehouseProduct(idProduct, idWarehouse, amount, createdAt);
            return Ok(primaryKey);
        }

        return NotFound();
    }
}