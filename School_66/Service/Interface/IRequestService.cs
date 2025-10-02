public interface IRequestService
{
    Task<List<Request>> GetUserRequestsAsync(string userId);
}