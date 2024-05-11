using Microsoft.AspNetCore.Mvc;
using WebApp3.Models;
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
    public IActionResult CreateWarehouseProduct(InputData data)
    {
        if (_warehouseService.ProductExists(data.idProduct) && _warehouseService.WarehouseExists(data.idWarehouse) && data.amount>0)
        {
            var primaryKey = _warehouseService.CreateWarehouseProduct(data.idProduct, data.idWarehouse, data.amount, data.createdAt);
            return Ok(primaryKey);
        }

        return NotFound("Product or warehouse with given id doesn't exists");
    }
}