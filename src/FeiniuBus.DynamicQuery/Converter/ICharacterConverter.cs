namespace FeiniuBus
{
    public interface ICharacterConverter
    {
        string FieldConverter(string fieldName);
        DynamicQuery Converter(DynamicQuery dynamicQuery);
        DynamicQueryParamGroup ConverterDynamicQueryParamGroup(DynamicQueryParamGroup old);
    }
}