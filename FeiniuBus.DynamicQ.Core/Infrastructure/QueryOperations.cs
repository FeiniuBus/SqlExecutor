namespace FeiniuBus.DynamicQ.Infrastructure
{
    public partial class QueryOperations
    {
        private readonly string _value;

        internal QueryOperations(string value)
        {
            _value = value;
        }

        public static implicit operator QueryOperations(string s)
        {
            return new QueryOperations(s);
        }

        public static implicit operator string(QueryOperations operation)
        {
            return operation._value;
        }

        public override bool Equals(object obj)
        {
            return obj.ToString() == ToString();
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return _value;
        }

        public static bool operator ==(QueryOperations left, QueryOperations right)
        {
            // ReSharper disable once PossibleNullReferenceException
            return left.Equals(right);
        }

        public static bool operator !=(QueryOperations left, QueryOperations right)
        {
            // ReSharper disable once PossibleNullReferenceException
            return !left.Equals(right);
        }
    }
}