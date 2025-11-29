using game.Models;

namespace game.DataAccess
{
    internal interface IRoleRepository
    {
        Role? GetByName (string RoleName);
        List<Role> GetAll();
        void Create (Role role);
        void Delete (string RoleName);
    }
}