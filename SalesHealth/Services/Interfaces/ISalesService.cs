using SalesHealth.Models.Dtos;

namespace SalesHealth.Services.Interfaces
{
    public interface ISalesService
    {
        Task<IEnumerable<SaleDto>> GetAllAsync();
        Task<SaleDto> GetByIdAsync(int id);
        Task AddAsync(SaleDto saleDto);
        Task UpdateAsync(SaleDto SaleDto);
        Task DeleteAsync(int id);
    }
}
