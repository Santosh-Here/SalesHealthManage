using SalesHealth.Models.Dtos;

namespace SalesHealth.Services.IService
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto request, bool withBearer = true);
    }
}
