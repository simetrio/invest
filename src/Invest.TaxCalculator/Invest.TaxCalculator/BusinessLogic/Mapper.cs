using AutoMapper;
using Invest.TaxCalculator.BusinessLogic.Operations;
using Invest.TaxCalculator.BusinessLogic.Operations.Dto;

namespace Invest.TaxCalculator.BusinessLogic
{
    public class Mapper
    {
        private static IMapper Current { get; } = CreateMapper();

        public TResult Map<TSource, TResult>(TSource source)
        {
            return Current.Map<TSource, TResult>(source);
        }

        private static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(ConfigureMapper);
            return config.CreateMapper();
        }

        private static void ConfigureMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Operation, OperationDto>();
        }
    }
}