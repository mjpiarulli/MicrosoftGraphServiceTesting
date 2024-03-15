using Microsoft.Graph;

namespace ActiveDirectoryService.UnitTest.Mocks
{
    public class MockHttpProvider : IHttpProvider
    {
        private readonly HttpResponseMessage _httpResponseMessage;
        public ISerializer Serializer { get; set; }        
        public TimeSpan OverallTimeout { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public MockHttpProvider(HttpResponseMessage httpResponseMessage, ISerializer serializer = null)
        {
            _httpResponseMessage = httpResponseMessage;
            Serializer = serializer;
        }

        public void Dispose()
        {
            _httpResponseMessage.Dispose();
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return Task.FromResult(_httpResponseMessage);
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
