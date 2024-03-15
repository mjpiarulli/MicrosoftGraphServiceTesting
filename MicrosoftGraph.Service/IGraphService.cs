using Microsoft.Graph;
using System.Linq.Expressions;

namespace MicrosoftGraph.Service
{
    public interface IGraphService
    {
        Task<GraphResponse<GraphServiceUsersCollectionResponse>> GetAllUsersPagedAsync(Expression<Func<User, User>> propertiesExpression = null, string customExtensionProperties = null, int? pageCount = null, string skipToken = null);
        Task<GraphResponse<User>> GetUserByGuidAsync(string userGuid, Expression<Func<User, User>> propertiesExpression = null, string customExtensionProperties = null, int? pageCount = null, string skipToken = null);
    }
}
