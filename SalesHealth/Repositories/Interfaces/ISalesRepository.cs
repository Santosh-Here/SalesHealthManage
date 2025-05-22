namespace SalesHealth.Repositories.Interfaces
{
    public interface ISalesRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetByIdAsync(int id);
		Task AddAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(int id);
    }
}
