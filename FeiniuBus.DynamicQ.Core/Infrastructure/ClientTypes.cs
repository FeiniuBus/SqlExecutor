namespace FeiniuBus.DynamicQ.Infrastructure
{
    public sealed partial class ClientTypes
    {
        private readonly string _value;

        private ClientTypes(string value)
        {
            _value = value;
        }

        public static implicit operator ClientTypes(string s)
        {
            return new ClientTypes(s);
        }

        public static implicit operator string(ClientTypes operation)
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
                case ClientTypes operations:
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

        public static bool operator ==(ClientTypes left, ClientTypes right)
        {
            return left != null && left.Equals(right);
        }

        public static bool operator !=(ClientTypes left, ClientTypes right)
        {
            if (left == null && right == null) return false;
            if (right != null && left != null && left._value == right._value) return false;
            return true;
        }
    }
}