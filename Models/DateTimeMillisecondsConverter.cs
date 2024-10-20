using Newtonsoft.Json;
using System;

public class DateTimeMillisecondsConverter : JsonConverter<DateTime>
{
    public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        // Lire la valeur comme long (timestamp en millisecondes)
        long milliseconds = (long)reader.Value;

        // Convertir en DateTime
        return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).DateTime;
    }

    public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
    {
        // Convertir DateTime en timestamp (millisecondes depuis l'époque Unix)
        long milliseconds = new DateTimeOffset(value).ToUnixTimeMilliseconds();
        writer.WriteValue(milliseconds);
    }
}
