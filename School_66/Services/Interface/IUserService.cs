public interface IUserService
{
    Task<User?> GetUserByEmailAndPasswordAsync(string email, string password);
    Task<User?> GetUserByEmailAsync(string? email);
    Task<bool> IsEmailTakenAsync(string email);
    Task CreateUserAsync(User user);
}