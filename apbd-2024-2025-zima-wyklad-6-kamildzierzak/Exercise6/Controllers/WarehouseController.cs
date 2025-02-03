using Exercise6.Model;
using Exercise6.Services;
using Microsoft.AspNetCore.Mvc;

namespace Exercise6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseController : ControllerBase
    {

        private IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProductToWarehouse([FromBody] AddProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input");
            }

            try
            {
                var productWarehouseId = await _warehouseService.UpdateWarehouse(request.IdProduct, request.IdWarehouse, request.Amount, request.CreatedAt);

                return StatusCode(StatusCodes.Status201Created, new { ProductWarehouseId = productWarehouseId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}
