namespace FeiniuBus.Converter
{
    public  interface IOperationConverter
    {
        bool CanConvert(ClientTypes clientType, string field, QueryOperations operation, object value);

        OperationConvertResult Convert(ClientTypes clientType, string field, QueryOperations operation, object value, string parameterName);
    }

    public sealed class OperationConvertResult
    {
        public OperationConvertResult(string clause, object value)
        {
            Clause = clause;
            Value = value;
        }
        public string Clause { get; }
        public object Value { get; }
    }
}
