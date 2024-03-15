using Microsoft.Graph;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace ActiveDirectoryService.UnitTest.Mocks
{
    public class MockSerializer : ISerializer
    {
        public T DeserializeObject<T>(Stream stream)
        {
            if (stream.GetType().ToString() == "System.Net.Http.EmptyReadStream")
                return default!;

            return JsonSerializer.Deserialize<T>(stream)!;
        }

        public T DeserializeObject<T>(string inputString)
        {
            if (inputString.IsNullOrEmpty())
                return default!;

            return JsonSerializer.Deserialize<T>(inputString)!;
        }

        public string SerializeObject(object serializeableObject)
        {
            return "{\"key\": \"value\"}";
        }
    }
}
