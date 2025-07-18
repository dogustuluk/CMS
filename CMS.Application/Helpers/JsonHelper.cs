using System.Text.Json;

namespace CMS.Application.Helpers;
public class JsonHelper
{
    public string? GetValue(string json, string key)
    {
        using var document = JsonDocument.Parse(json);

        if (document.RootElement.TryGetProperty(key, out var property))
            return property.GetString();

        return null;

    }

    //int - string karışık gelebilirse
    public object? GetRawValue(string json, string key)
    {
        using var document = JsonDocument.Parse(json);
        if (document.RootElement.TryGetProperty(key, out var value))
        {
            return value.ValueKind switch
            {
                JsonValueKind.String => value.GetString(),
                JsonValueKind.Number => value.GetInt32(), // veya GetDouble()
                JsonValueKind.True => true,
                JsonValueKind.False => false,
                _ => null
            };
        }
        return null;
    }

}