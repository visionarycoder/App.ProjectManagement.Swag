using System.Text.Json;

namespace Swag.Framework;

public static class GlobalShared
{

    public static readonly JsonSerializerOptions JsonSerializationOptions = new()
    { 
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };

    public static readonly string DaprApiToken = Guid.NewGuid().ToString();

}