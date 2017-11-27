namespace FeiniuBus.Mapper
{
    public interface IEntityMapper
    {
        TDestination Mapper<TSource, TDestination>(TSource model);
        TDestination Mapper<TSource, TDestination>(TSource model, TDestination destination);
    }
}