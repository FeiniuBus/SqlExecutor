using System;
using System.Collections.Generic;
using System.Text;
using FeiniuBus.DynamicQ.Infrastructure;
using Newtonsoft.Json;

namespace FeiniuBus.DynamicQ.Json
{
    public class QueryOperationsJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                throw new ArgumentNullException($"Cannot convert null value to {nameof(QueryOperations)}.");
            }
            return new QueryOperations( reader.Value.ToString());
        }

        public override bool CanConvert(Type objectType) =>
            objectType == typeof(QueryOperations) || objectType == typeof(string);
    }
}
