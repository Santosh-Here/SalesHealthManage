using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalesHealth.Models;
using SalesHealth.Models.Dtos;
using SalesHealth.SalesHeathRepository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesHealth.Controllers
{
    [Route("/api/saleshealth/v1/sales")]
    [ApiController]
    public class SalesHealthController : ControllerBase
    {
        private readonly ISalesHealthRespository _salesHealthRespository;
        private readonly IMapper _mapper;

        public SalesHealthController(ISalesHealthRespository salesHealthRespository, IMapper mapper)
        {
            _salesHealthRespository = salesHealthRespository;
            _mapper = mapper;
        }

        /// <summary>
        /// Endpoint to create a sale
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequestDto requestDto)
        {
            var response = new ResponseDto();
            if (string.IsNullOrEmpty(requestDto.Name) || string.IsNullOrEmpty(requestDto.Description))
            {
                return BadRequest("Name and Description cannot be empty.");
            }

            try
            {
                var sale = _mapper.Map<SaleDto>(requestDto);
                var createdSale = await _salesHealthRespository.CreateSaleAsync(sale);

                response.Result = createdSale != null ? _mapper.Map<SaleDto>(createdSale) : null;
                response.IsSuccess = createdSale != null;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        /// <summary>
        /// Endpoint to update a sale
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateSale([FromBody] SaleDto request)
        {
            var response = new ResponseDto();
            try
            {
                var sale = _mapper.Map<SaleDto>(request);
                var updatedSale = await _salesHealthRespository.EditSaleAsync(sale);

                response.Result = updatedSale != null ? _mapper.Map<SaleDto>(updatedSale) : null;
                response.IsSuccess = updatedSale != null;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        /// <summary>
        /// Endpoint to delete a sale
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            var response = new ResponseDto();
            try
            {
                var result = await _salesHealthRespository.DeleteSaleAsync(id);
                response.Result = result?.IsSuccess == true ? "Success! Sale deleted successfully..." : null;
                response.IsSuccess = result.IsSuccess;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        /// <summary>
        /// Endpoint to get all sales details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetSales()
        {
            var response = new ResponseDto();
            try
            {
                var salesList = await _salesHealthRespository.GetAllSaleAsync();
                response.Result = salesList != null ? _mapper.Map<IEnumerable<SaleDto>>(salesList) : null;
                response.IsSuccess = salesList != null;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        /// <summary>
        /// Endpoint to get the details of a sale by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSale([FromRoute] int id)
        {
            var response = new ResponseDto();
            try
            {
                var sale = await _salesHealthRespository.GetSaleByIdAsync(id);
                response.Result = sale != null ? _mapper.Map<SaleDto>(sale) : null;
                response.IsSuccess = sale != null;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response?.IsSuccess == true ? Ok(response) : NotFound(response);
        }
    }
}
