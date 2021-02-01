using AutoMapper;

namespace AdventureWork.Infra.Data.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DtoToDomainMappingProfile>();
            });
        }
    }
}
