namespace FeiniuBus.Mapper
{
    public class FeiniuBusEntityMapper : IEntityMapper
    {
        public TDestination Mapper<TSource, TDestination>(TSource model)
        {
            return AutoMapper.Mapper.Map<TSource, TDestination>(model);
        }


        public TDestination Mapper<TSource, TDestination>(TSource model, TDestination destination)
        {
            return AutoMapper.Mapper.Map(model, destination);
        }
    }
}