using Microsoft.EntityFrameworkCore;
using SalesHealth.Cores;
using SalesHealth.DbContexts;
using SalesHealth.Models;
using SalesHealth.Models.Dtos;
using SalesHealth.SalesHeathRepository.Interface;
using SalesHealth.Services.IService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesHealth.SalesHeathRepository.Implementation
{
    public class SalesHealthRespository : ISalesHealthRespository
    {
        private readonly IBaseService _baseService;
        public SalesHealthRespository(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateSaleAsync(SaleDto saleDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = saleDto,
                Url = SD.SalesApiBase + "/api/sale",
                ContentType = SD.ContentType.Json
            });
        }

        public async Task<ResponseDto?> DeleteSaleAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.SalesApiBase + "/api/sale/" + id
            });
        }


        public async Task<ResponseDto?> GetAllSaleAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.SalesApiBase + "/api/sale"
            });
        }

        public async Task<ResponseDto?> GetSaleByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.SalesApiBase + "/api/sale/" + id
            });
        }

        public async Task<ResponseDto?> EditSaleAsync(SaleDto saleDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = saleDto,
                Url = SD.SalesApiBase + "/api/sale",
                ContentType = SD.ContentType.Json
            });
        }
    }
}
