using AutoMapper;
using SalesHealth.Models;
using SalesHealth.Models.Dtos;

namespace SalesHealth.Cores
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Sale, SaleDto>().ReverseMap();
                config.CreateMap<Sale, CreateSaleRequestDto>().ReverseMap();
                config.CreateMap<SaleDto, CreateSaleRequestDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
