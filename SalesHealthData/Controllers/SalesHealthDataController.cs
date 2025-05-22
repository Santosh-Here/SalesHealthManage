using Microsoft.AspNetCore.Mvc;
using SalesHealthData.Models;

namespace SalesHealthData.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesHealthDataController(ILogger<SalesHealthDataController> logger) : ControllerBase
    {
        private readonly ILogger<SalesHealthDataController> _logger = logger;
        private static List<SaleDto> SalesData =
        [
            new() { Id = 1, Name = "Door", Description = "Solid Wooden Door" },
            new() { Id = 2, Name = "Window", Description = "Solid Wooden Window" },
            new() { Id = 3, Name = "Shelf", Description = "Solid Wooden Shelf" },
            new() { Id = 4, Name = "Cupboard", Description = "Solid Wooden Cupboard" },
            new() { Id = 5, Name = "Dining Set", Description = "Solid Wooden Dining Set" }
        ];

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await Task.Run(() => Ok(SalesData.AsEnumerable()));
        }

        [Route("{id:int}")]
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var salesData = SalesData.FirstOrDefault(s => s.Id == id);

            if (salesData == null)
            {
                return await Task.Run(() => NotFound());
            }

            return await Task.Run(() => Ok(salesData)) ;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]SaleDto saleDto)
        {
            if (saleDto == null) throw new ArgumentNullException(nameof(saleDto));

            if (string.IsNullOrEmpty(saleDto.Name) || string.IsNullOrEmpty(saleDto.Description))
            {
                return await Task.Run(() => BadRequest("Name or Description cannot be empty."));
            }
            var maxId = SalesData.Max(s => s.Id);

            saleDto.Id = maxId+1;

            SalesData.Add(saleDto);

            return await Task.Run(() => Accepted());
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]SaleDto saleDto)
        {
            if (saleDto == null) throw new ArgumentNullException(nameof(saleDto));

            if (string.IsNullOrEmpty(saleDto.Name) || string.IsNullOrEmpty(saleDto.Description))
            {
                return await Task.Run(() => BadRequest("Name or Description cannot be empty."));
            }

            var salesData = SalesData.FirstOrDefault(s => s.Id == saleDto.Id);

            if (salesData == null)
            {
                return await Task.Run(() => NotFound());
            }

            salesData.Name = saleDto.Name;
            salesData.Description = saleDto.Description;

            return await Task.Run(() => Accepted());
        }

        [Route("{id:int}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var salesData = SalesData.FirstOrDefault(s => s.Id == id);

            if (salesData == null)
            {
                return await Task.Run(() => NotFound());
            }

            SalesData.Remove(salesData);

            return await Task.Run(() => Accepted());
        }
    }
}
