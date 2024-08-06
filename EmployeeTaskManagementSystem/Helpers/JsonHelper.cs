using System.Text.Json;
using System.Text.Json.Serialization;

namespace EmployeeTaskManagementSystem.Helpers
{
    public static class JsonHelper
    {
        public static T Deserialize<T>(string json)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                ReferenceHandler = ReferenceHandler.Preserve
            };
            return JsonSerializer.Deserialize<T>(json, options);
        }
    }
}
