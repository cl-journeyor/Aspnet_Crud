using System.Text.Json;

namespace Aspnet_Crud;

[Obsolete]
public static class Json
{
    static Json()
    {
        opts = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.KebabCaseLower
        };
    }

    private static readonly JsonSerializerOptions opts;

    public static string Serialize(object? obj) =>
        JsonSerializer.Serialize(obj, opts);
}
