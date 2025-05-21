using SalesHealth.Models;

namespace SalesHealth.SalesHeathRepository.Interface
{
    public interface ISalesHealthRespository
    {
        Task<IEnumerable<Sale>> GetAllSalesAsync();
        Task<Sale> GetSaleAsync(int id);
        Task<Sale> CreateSale(Sale sale);
        Task<Sale> UpdateSale(Sale sale);
        Task<int> DeleteSale(int id);
    }
}
