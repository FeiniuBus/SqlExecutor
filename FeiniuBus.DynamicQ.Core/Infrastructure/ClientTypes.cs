namespace FeiniuBus.DynamicQ.Infrastructure
{
    public  partial class ClientTypes
    {
        private readonly string _value;

        internal ClientTypes(string value)
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

        public override string ToString()
        {
            return _value;
        }

        public override bool Equals(object obj)
        {
            return ToString() == obj.ToString();
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public static bool operator ==(ClientTypes left, ClientTypes right)
        {
            // ReSharper disable once PossibleNullReferenceException
            return left.Equals(right);
        }

        public static bool operator !=(ClientTypes left, ClientTypes right)
        {
            // ReSharper disable once PossibleNullReferenceException
            return !left.Equals(right);
        }
    }
}