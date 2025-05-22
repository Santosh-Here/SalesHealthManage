using Newtonsoft.Json;
using SalesHealth.Adapters.Interfaces;

namespace SalesHealth.Adapters
{
    public class SalesApiAdapter<T>(HttpClient httpClient, IConfiguration configuration) : ISalesApiAdapter<T>
	{
		private readonly HttpClient _httpClient = httpClient;
        private readonly string _baseUrl = configuration["ServiceUrls:SaleAPI"] ?? "https://localhost:7048/saleshealthdata";

        public async Task<IEnumerable<T>> FetchAllAsync()
		{
			var response = await _httpClient.GetAsync($"{_baseUrl}");
			response.EnsureSuccessStatusCode();
			var responseString = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<IEnumerable<T>>(responseString) ?? default!;
		}

		public async Task<T> FetchByIdAsync(int id)
		{
			var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
			response.EnsureSuccessStatusCode();
			return await response.Content.ReadFromJsonAsync<T>() ?? default!;
		}

		public async Task CreateAsync(T entity)
		{
			var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}", entity);
			response.EnsureSuccessStatusCode();
		}

		public async Task ModifyAsync(T entity)
		{
			var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}", entity);
			response.EnsureSuccessStatusCode();
		}

		public async Task RemoveAsync(int id)
		{
			var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
			response.EnsureSuccessStatusCode();
		}
	}
}
