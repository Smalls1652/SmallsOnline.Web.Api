namespace SmallsOnline.Web.Lib.Models.Json;

public class JsonDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
{
    private readonly TimeZoneInfo _dateTimeTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        DateTime inputTime = DateTime.Parse($"{reader.GetString()} 17:00");
        return new(inputTime, _dateTimeTimeZoneInfo.GetUtcOffset(inputTime));
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
    }
}