using System.Text.Json;

namespace Rsp.IrasService.Infrastructure.Helpers;

public static class JsonHelper
{
    public static List<TEntity> Parse<TEntity>(string fileName)
    {
        var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var fullPath = Path.Combine(currentDirectory, "Data", fileName);

        var result = new List<TEntity>();
        using (var reader = new StreamReader(fullPath))
        {
            string json = reader.ReadToEnd();
            result = JsonSerializer.Deserialize<List<TEntity>>(json);
        }

        return result!;
    }
}