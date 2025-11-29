using System.Data.SQLite;
using DataAccess.Common;
using DataAccess.interfaces_auth;

namespace DataAccess.repository_auth
{
    public class RolePermissionRepository : IRolePermissionRepository
    {
        private readonly SQLiteConnection? _testConnection;

        public RolePermissionRepository() { }

        public RolePermissionRepository(SQLiteConnection testConn)
        {
            _testConnection = testConn;
        }

        public RolePermission? Get(string role, string permission)
        {
            try
            {
                var conn = SQLauth.GetConnection(_testConnection); // PAS de "using"
                const string sql = "SELECT role, permission FROM roles_permissions WHERE role = @r AND permission = @p";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@r", role);
                cmd.Parameters.AddWithValue("@p", permission);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new RolePermission { Role = reader.GetString(0), Permission = reader.GetString(1) } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[RolePermissionRepository.Get] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[RolePermissionRepository.Get] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public List<RolePermission> GetAllByRole(string role)
        {
            try
            {
                var conn = SQLauth.GetConnection(_testConnection);
                const string sql = "SELECT role, permission FROM roles_permissions WHERE role = @r";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@r", role);
                using var reader = cmd.ExecuteReader();
                var list = new List<RolePermission>();
                while (reader.Read())
                    list.Add(new RolePermission { Role = reader.GetString(0), Permission = reader.GetString(1) });
                return list;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[RolePermissionRepository.GetAllByRole] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[RolePermissionRepository.GetAllByRole] Unexpected error: {ex.Message}");
                throw;
            }
        }


        public void Create(RolePermission rp)
        {
            try
            {
                var conn = SQLauth.GetConnection(_testConnection);
                const string sql = "INSERT INTO roles_permissions (role, permission) VALUES (@r, @p)";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@r", rp.Role);
                cmd.Parameters.AddWithValue("@p", rp.Permission);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[RolePermissionRepository.Create] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[RolePermissionRepository.Create] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public void Delete(string role, string permission)
        {
            try
            {
                var conn = SQLauth.GetConnection(_testConnection);
                const string sql = "DELETE FROM roles_permissions WHERE role = @r AND permission = @p";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@r", role);
                cmd.Parameters.AddWithValue("@p", permission);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[RolePermissionRepository.Delete] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[RolePermissionRepository.Delete] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public void Delete(RolePermission rp)
        {
            Delete(rp.Role, rp.Permission);
        }
    }
}
