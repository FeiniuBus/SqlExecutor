using FeiniuBus.DynamicQ.Infrastructure;
using Newtonsoft.Json;
using System;

namespace FeiniuBus.DynamicQ.Json
{
    public class QueryRelationsJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                throw new ArgumentNullException($"Cannot convert null value to {nameof(QueryRelations)}.");
            }
            return new QueryRelations(reader.Value.ToString());
        } 

        public override bool CanConvert(Type objectType) =>
            objectType == typeof(QueryRelations) || objectType == typeof(string);
    }
}
