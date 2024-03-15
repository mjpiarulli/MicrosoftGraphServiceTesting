using Microsoft.Graph;
using ActiveDirectoryService.UnitTest.Mocks;
using NSubstitute;

namespace ActiveDirectoryService.UnitTest.Helpers
{
    internal static class MicrosoftGraphResponseHelper
    {
        
        internal static GraphResponse<T> CreateGraphResponse<T>(string baseUrl, HttpResponseMessage responseMessage)
        {
            var authProvider = Substitute.For<IAuthenticationProvider>();
            var serializer = Substitute.For<MockSerializer>();
            var httpProvider = Substitute.For<MockHttpProvider>(responseMessage, serializer);

            var baseClient = Substitute.For<BaseClient>(baseUrl, authProvider, httpProvider);
            var baseRequest = Substitute.For<BaseRequest>(baseUrl, baseClient, null);

            var response = new GraphResponse<T>(baseRequest, responseMessage);

            return response;
        }
    }
}
