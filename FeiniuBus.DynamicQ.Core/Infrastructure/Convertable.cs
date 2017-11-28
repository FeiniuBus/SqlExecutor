using System;

namespace FeiniuBus.DynamicQ.Infrastructure
{
    public sealed class Convertable
    {
        public Convertable(bool canConvert, string message)
        {
            CanConvert = canConvert;
            Message = message;
        }

        public bool CanConvert { get; }
        public string Message { get; }

        public static implicit operator Convertable(bool canConvert)
        {
            return new Convertable(canConvert, null);
        }

        public static implicit operator Convertable(string message)
        {
            return new Convertable(true, message);
        }

        public bool ThrowIfCouldNotConvert()
        {
            if(CanConvert && !string.IsNullOrEmpty(Message)) throw new Exception(Message);
            return CanConvert;
        }
    }
}
