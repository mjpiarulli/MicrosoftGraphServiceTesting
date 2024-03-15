using ActiveDirectory.Common;
using Common;

namespace ADService
{
    public interface IADService
    {
        Task<ResponseWithPagedContent<List<User>>> GetAllUsersPagedAsync(int? pageCount = null, string? nextPageToken = null);
        Task<ResponseWithContent<User>> GetUserByGuidAsync(string userGuid);
        
    }
}
