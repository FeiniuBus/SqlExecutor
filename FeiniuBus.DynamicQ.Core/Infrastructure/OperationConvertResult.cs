namespace FeiniuBus.DynamicQ.Infrastructure
{
    public class OperationConvertResult
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