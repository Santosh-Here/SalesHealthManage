using SalesHealth.Models.Dtos;
using SalesHealth.Repositories.Interfaces;
using SalesHealth.Services.Interfaces;

namespace SalesHealth.Services
{
    public class SalesService(ILogger<SalesService> logger, 
        ISalesRepository<SaleDto> salesRepository) : ISalesService
    {
        private readonly ILogger<SalesService> _logger = logger;
        private readonly ISalesRepository<SaleDto> _salesRepository = salesRepository;

        public async Task<IEnumerable<SaleDto>> GetAllAsync()
        {
            return await _salesRepository.GetAllAsync();
        }

        public async Task<SaleDto> GetByIdAsync(int id)
        {
            return await _salesRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(SaleDto saleDto)
        {
            await _salesRepository.AddAsync(saleDto);
        }

        public async Task UpdateAsync(SaleDto SaleDto)
        {
            await _salesRepository.UpdateAsync(SaleDto);
        }

        public async Task DeleteAsync(int id)
        {
            await _salesRepository.DeleteAsync(id);
        }
    }
}
