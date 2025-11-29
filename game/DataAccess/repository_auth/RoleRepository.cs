using System.Data.SQLite;
using DataAccess.Common;
using DataAccess.interfaces_auth;

namespace DataAccess.repository_auth
{
    public class RoleRepository : IRoleRepository
    {
        private readonly SQLiteConnection? _testConnection;

        public RoleRepository() { }

        public RoleRepository(SQLiteConnection testConn)
        {
            _testConnection = testConn;
        }

        public Role? GetByName(string RoleName)
        {
            try
            {
                var conn = SQLauth.GetConnection(_testConnection);
                const string sql = "SELECT role FROM roles WHERE role = @r";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@r", RoleName);
                using var reader = cmd.ExecuteReader();
                return reader.Read()
                    ? new Role { RoleName = reader.GetString(0) }
                    : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[RoleRepository.GetByName] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[RoleRepository.GetByName] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public List<Role> GetAll()
        {
            try {
                var conn = SQLauth.GetConnection(_testConnection);
                const string sql = "SELECT role FROM roles";
                using var cmd = new SQLiteCommand(sql, conn);
                using var reader = cmd.ExecuteReader();
                var list = new List<Role>();
                while (reader.Read())
                    list.Add(new Role { RoleName = reader.GetString(0) });
                return list;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[RoleRepository.GetAll] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[RoleRepository.GetAll] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public void Create(Role role)
        {
            try {
                var conn = SQLauth.GetConnection(_testConnection);
                const string sql = "INSERT INTO roles (role) VALUES (@r)";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@r", role.RoleName);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[RoleRepository.Create] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[RoleRepository.Create] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public void Delete(string RoleName)
        {
            try {
                var conn = SQLauth.GetConnection(_testConnection);
                const string sql = "DELETE FROM roles WHERE role = @r";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@r", RoleName);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[RoleRepository.Delete] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[RoleRepository.Delete] Unexpected error: {ex.Message}");
                throw;
            }
        }
    }
}
