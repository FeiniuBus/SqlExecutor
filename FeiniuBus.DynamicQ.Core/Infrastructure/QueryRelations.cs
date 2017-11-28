namespace FeiniuBus.DynamicQ.Infrastructure
{
    public sealed partial class QueryRelations
    {
        private readonly string _value;

        private QueryRelations(string value)
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

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case QueryRelations operations:
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

        public static bool operator ==(QueryRelations left, QueryRelations right)
        {
            return left != null && left.Equals(right);
        }

        public static bool operator !=(QueryRelations left, QueryRelations right)
        {
            if (left == null && right == null) return false;
            if (right != null && left != null && left._value == right._value) return false;
            return true;
        }
    }
}