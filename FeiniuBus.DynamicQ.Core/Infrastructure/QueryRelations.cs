namespace FeiniuBus.DynamicQ.Infrastructure
{
    public partial class QueryRelations
    {
        private readonly string _value;

        internal QueryRelations(string value)
        {
            _value = value;
        }

        public static implicit operator QueryRelations(string s)
        {
            return new QueryRelations(s);
        }

        public static implicit operator string(QueryRelations operation)
        {
            return operation._value;
        }
        public override string ToString()
        {
            return _value;
        }

        public override bool Equals(object obj)
        {
            return obj.ToString() == ToString();
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public static bool operator ==(QueryRelations left, QueryRelations right)
        {
            // ReSharper disable once PossibleNullReferenceException
            return left.Equals(right);
        }

        public static bool operator !=(QueryRelations left, QueryRelations right)
        {
            // ReSharper disable once PossibleNullReferenceException
            return !left.Equals(right);
        }
    }
}