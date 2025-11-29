using game.Models;

namespace game.DataAccess
{
    internal interface IRolePermissionRepository
    {
        RolePermission? Get(string role, string permission);
        List<RolePermission> GetAllByRole(string role);
        void Create(RolePermission rolePermission);
        void Delete(RolePermission rolePermission);
    }
}