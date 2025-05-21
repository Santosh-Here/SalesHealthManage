using SalesHealth.Models;
using SalesHealth.Models.Dtos;

namespace SalesHealth.SalesHeathRepository.Interface
{
    public interface ISalesHealthRespository
    {
        Task<ResponseDto?> GetAllSaleAsync();
        Task<ResponseDto?> GetSaleByIdAsync(int id);
        Task<ResponseDto?> CreateSaleAsync(SaleDto saleDto);
        Task<ResponseDto?> EditSaleAsync(SaleDto saleDto);
        Task<ResponseDto?> DeleteSaleAsync(int id);
    }
}
