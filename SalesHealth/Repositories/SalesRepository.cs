using SalesHealth.Adapters.Interfaces;
using SalesHealth.Repositories.Interfaces;

namespace SalesHealth.Repositories
{
    public class SalesRepository<T>(ISalesApiAdapter<T> apiAdapter) : ISalesRepository<T>
	{
		private readonly ISalesApiAdapter<T> _apiAdapter = apiAdapter;

        public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _apiAdapter.FetchAllAsync();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _apiAdapter.FetchByIdAsync(id);
		}

		public async Task AddAsync(T entity)
		{
			await _apiAdapter.CreateAsync(entity);
		}

		public async Task UpdateAsync(T entity)
		{
			await _apiAdapter.ModifyAsync(entity);
		}

		public async Task DeleteAsync(int id)
		{
			await _apiAdapter.RemoveAsync(id);
		}
	}
}
