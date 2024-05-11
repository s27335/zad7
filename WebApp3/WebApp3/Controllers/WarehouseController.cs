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
            if (_warehouseService.OrderExists(data.idProduct,data.amount) && _warehouseService.ValidDate(data.idProduct,data.amount,data.createdAt))
            {
                if (!_warehouseService.CompletedOrder(data.idProduct,data.amount))
                {
                    _warehouseService.UpdateFulfilledAt(data.idProduct,data.amount);
                    var primaryKey = _warehouseService.CreateWarehouseProduct(data.idProduct, data.idWarehouse, data.amount);
                    return Created("","Record added with ID: " + primaryKey);
                }

                return BadRequest("Order has been already completed");
            }

            return BadRequest("There isn't any order with this parameters");
        }

        return BadRequest("Product or warehouse with given id doesn't exists");
    }
}