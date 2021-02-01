using AdventureWork.Domain.Entities;
using AdventureWork.Infra.Data.Dto;
using AutoMapper;

namespace AdventureWork.Infra.Data.AutoMapper
{
    internal class DtoToDomainMappingProfile : Profile
    {
        public DtoToDomainMappingProfile()
        {
            CreateMap<WorkOrdersProductDto, WorkOrder>()
                .ForPath(s => s.Product.Name, o => o.MapFrom(x => x.Name))
                .ForPath(s => s.Product.Color, o => o.MapFrom(x => x.Color))
                .ForPath(s => s.Product.ProductNumber, o => o.MapFrom(x => x.ProductNumber));
        }
    }
}
