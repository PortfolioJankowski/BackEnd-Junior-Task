using System.Text.Json;
using System.Text.Json.Serialization;

namespace Task.Services.DateConverter
{
    public class JsonZuluDateConverter : JsonConverter<DateTime> 
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        { 
            if (reader.TokenType == JsonTokenType.String) 
            {
                var dateStr = reader.GetString(); 
                return ZuluDateConverter.DateOffsetToDateTime(dateStr!); 
            } 
            throw new NotImplementedException($"Invalid DateTime format: {reader.GetString()}"); 
        }
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options) 
        { 
            writer.WriteStringValue(value.ToString("yyyy-MM-ddTHH:mm:ssZ")); 
        } 
    }
}

