using ActiveDirectory.Common;
using ADService.Mapping;
using Common;
using MicrosoftGraph.Service;

namespace ADService
{
    public class ADService : IADService
    {
        private readonly IGraphService _graphService;

        public ADService(IGraphService graphService) 
        {
            _graphService = graphService;
        }

        private string? GetNextPageToken(string? nextLink)
        {
            if(string.IsNullOrEmpty(nextLink)) 
                return null;

            var queryString = new Uri(nextLink).Query;
            var queryDict = System.Web.HttpUtility.ParseQueryString(queryString);
            var skipToken = queryDict[$"skiptoken"].ToString();

            return skipToken;
        }


        public async Task<ResponseWithPagedContent<List<User>>> GetAllUsersPagedAsync(int? pageCount = null, string? nextPageToken = null)
        {
            List<User> users = null;
            var response = new ResponseWithPagedContent<List<User>> { IsSuccess = false };

            var usersResponse = await _graphService.GetAllUsersPagedAsync(null, null, pageCount, nextPageToken);
            nextPageToken = null;

            if (usersResponse == null)
            {
                response.Content = null;
                response.ResponseMessage = "Success";
                return response;
            }

            if(usersResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                response.Content = null;
                response.ResponseMessage = usersResponse.ToHttpResponseMessage().ToString();
                return response;                
            }

            var userPage = await usersResponse.GetResponseObjectAsync();

            if(userPage != null && userPage.Value.CurrentPage != null && userPage.Value.CurrentPage.Count > 0)
            {
                users = userPage.Value.CurrentPage.Select(GraphUser.MapGraphUserToUser).ToList();
                nextPageToken = GetNextPageToken(userPage.NextLink);
                response.NextPageToken = nextPageToken;
            }

            response.IsSuccess = true;
            
            return response;
        }

        public async Task<ResponseWithContent<User>> GetUserByGuidAsync(string userGuid)
        {
            User user = null;
            var response = new ResponseWithContent<User> { IsSuccess = false };

            var userResponse = await _graphService.GetUserByGuidAsync(userGuid, null, null);

            if(userResponse == null)
            {
                response.Content = null;
                response.ResponseMessage = "Success";
                return response;
            }

            if(userResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                response.ResponseMessage = userResponse.ToHttpResponseMessage().ToString();
                response.Content = null;
                return response;
            }

            var graphUser = await userResponse.GetResponseObjectAsync();
            if (graphUser != null)
                user = GraphUser.MapGraphUserToUser(graphUser);

            response.IsSuccess = true;
            response.Content = user;
            response.ResponseMessage = "Success";

            return response;

        }
    }
}
