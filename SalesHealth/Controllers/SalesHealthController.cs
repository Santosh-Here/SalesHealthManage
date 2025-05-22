using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalesHealth.Models.Dtos;
using SalesHealth.Services.Interfaces;

namespace SalesHealth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesHealthController(IMapper mapper,
        ISalesService salesService,
        ILogger<SalesHealthController> logger) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly ISalesService _salesService = salesService;
        private readonly ILogger<SalesHealthController> _logger = logger;

        /// <summary>
        /// Endpoint to create a sale
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequestDto requestDto)
        {
            if (string.IsNullOrEmpty(requestDto.Name) || string.IsNullOrEmpty(requestDto.Description))
            {
                return BadRequest("Name or Description cannot be empty.");
            }

            try
            {
                var sale = _mapper.Map<SaleDto>(requestDto);

                await _salesService.AddAsync(sale);

                return Created();
            }
            catch (HttpRequestException requestException)
            {
                _logger.LogError($"Non success response code received from the underlying service. Response: {requestException.StatusCode} and message: {requestException.Message}.");

                return StatusCode(500, $"Non success response code received from the underlying service. Response: {requestException.StatusCode}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error while creating sale. Reason: {ex.Message}");
            }
        }

        /// <summary>
        /// Endpoint to update a sale
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateSale([FromBody] SaleDto requestDto)
        {
            try
            {
                if (string.IsNullOrEmpty(requestDto.Name) || string.IsNullOrEmpty(requestDto.Description))
                {
                    return BadRequest("Name or Description cannot be empty.");
                }

                await _salesService.UpdateAsync(requestDto);

                return Accepted();
            }
            catch (HttpRequestException requestException)
            {
                if (requestException.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return NotFound();
                }

                _logger.LogError($"Non success response code received from the underlying service. Response: {requestException.StatusCode} and message: {requestException.Message}.");

                return StatusCode(500, $"Non success response code received from the underlying service. Response: {requestException.StatusCode}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error while updating sale. Reason: {ex.Message}");
            }
        }

        /// <summary>
        /// Endpoint to delete a sale
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            try
            {
                await _salesService.DeleteAsync(id);

                return Accepted();
            }
            catch (HttpRequestException requestException)
            {
                if (requestException.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return NotFound();
                }

                _logger.LogError($"Non success response code received from the underlying service. Response: {requestException.StatusCode} and message: {requestException.Message}.");

                return StatusCode(500, $"Non success response code received from the underlying service. Response: {requestException.StatusCode}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error while deleting sale. Reason: {ex.Message}");
            }
        }

        /// <summary>
        /// Endpoint to get all sales details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetSales()
        {
            try
            {
                var saleDataList = await _salesService.GetAllAsync();

                return Ok(saleDataList);
            }
            catch (HttpRequestException requestException)
            {
                _logger.LogError($"Non success response code received from the underlying service. Response: {requestException.StatusCode} and message: {requestException.Message}.");

                return StatusCode(500, $"non success response code received from the underlying service. Response: {requestException.StatusCode}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error while fetching sales data. Reason: {ex.Message}");
            }
        }

        /// <summary>
        /// Endpoint to get the details of a sale by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSale(int id)
        {
            try
            {
                var saleData = await _salesService.GetByIdAsync(id);

                return Ok(saleData);
            }
            catch (HttpRequestException requestException)
            {
                if(requestException.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return NotFound();
                }

                _logger.LogError($"Non success response code received from the underlying service. Response: {requestException.StatusCode} and message: {requestException.Message}.");

                return StatusCode(500, $"Non success response code received from the underlying service. Response: {requestException.StatusCode}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error while fetching sales data. Reason: {ex.Message}");
            }
        }
    }
}
