namespace DataAccess.interfaces_auth
{
    public interface IRoleRepository
    {
        Role? GetByName (string RoleName);
        List<Role> GetAll();
        void Create (Role role);
        void Delete (string RoleName);
    }
}