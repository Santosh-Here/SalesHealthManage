using Microsoft.EntityFrameworkCore;
using SalesHealth.DbContexts;
using SalesHealth.Models;
using SalesHealth.SalesHeathRepository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesHealth.SalesHeathRepository.Implementation
{
    public class SalesHealthRespository : ISalesHealthRespository
    {
        private readonly SalesHealthDbContext _dbContext;

        public SalesHealthRespository(SalesHealthDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Sale> CreateSale(Sale sale)
        {
            if (sale == null)
            {
                throw new ArgumentNullException(nameof(sale));
            }

            await _dbContext.Sales.AddAsync(sale);
            await _dbContext.SaveChangesAsync();
            return sale;
        }

        public async Task<int> DeleteSale(int id)
        {
            var saleFromDb = await _dbContext.Sales.FindAsync(id);
            if (saleFromDb == null)
            {
                throw new KeyNotFoundException($"Sale with ID {id} not found.");
            }

            _dbContext.Sales.Remove(saleFromDb);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Sale>> GetAllSalesAsync()
        {
            return await _dbContext.Sales.ToListAsync();
        }

        public async Task<Sale> GetSaleAsync(int id)
        {
            var sale = await _dbContext.Sales.FindAsync(id);
            if (sale == null)
            {
                throw new KeyNotFoundException($"Sale with ID {id} not found.");
            }

            return sale;
        }

        public async Task<Sale> UpdateSale(Sale sale)
        {
            if (sale == null)
            {
                throw new ArgumentNullException(nameof(sale));
            }

            var existingSale = await _dbContext.Sales.FindAsync(sale.Id);
            if (existingSale == null)
            {
                throw new KeyNotFoundException($"Sale with ID {sale.Id} not found.");
            }

            _dbContext.Entry(existingSale).CurrentValues.SetValues(sale);
            await _dbContext.SaveChangesAsync();
            return existingSale;
        }
    }
}
