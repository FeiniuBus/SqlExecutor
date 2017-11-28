namespace FeiniuBus.DynamicQ.Infrastructure
{
    public sealed partial class QueryOperations
    {
        private readonly string _value;

        private QueryOperations(string value)
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

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case QueryOperations operations:
                    return operations._value == _value;
                case string s:
                    return _value == s;
            }
            return false;
        }

        public override string ToString()
        {
            return _value;
        }

        public static bool operator ==(QueryOperations left, QueryOperations right)
        {
            return left != null && left.Equals(right);
        }

        public static bool operator !=(QueryOperations left, QueryOperations right)
        {
            if (left == null && right == null) return false;
            if (right != null && left != null && left._value == right._value) return false;
            return true;
        }
    }
}