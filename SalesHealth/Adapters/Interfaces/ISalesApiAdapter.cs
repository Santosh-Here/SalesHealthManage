namespace SalesHealth.Adapters.Interfaces
{
    public interface ISalesApiAdapter<T>
	{
		Task<IEnumerable<T>> FetchAllAsync();
		Task<T> FetchByIdAsync(int id);
		Task CreateAsync(T entity);
		Task ModifyAsync(T entity);
		Task RemoveAsync(int id);
	}
}
