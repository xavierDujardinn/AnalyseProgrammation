namespace DataAccess.interfaces_auth
{
    public interface IUserRepository
    {
        User? GetByUsername(string username);
        List<User> GetAllByRole(string roleName);
        void Create(User user);
        void UpdateStatus(string username, string? newStatus);
        void Delete (string username);
    }
}