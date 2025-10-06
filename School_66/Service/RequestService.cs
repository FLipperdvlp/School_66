using Microsoft.EntityFrameworkCore;
using School_66.DataBase;

public class RequestService : IRequestService
{
    private readonly AppDbContext _db;
    public RequestService(AppDbContext db) { _db = db; }

    public async Task<List<Request>> GetUserRequestsAsync(string userId)
    {
        return await _db.Requests
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }
}