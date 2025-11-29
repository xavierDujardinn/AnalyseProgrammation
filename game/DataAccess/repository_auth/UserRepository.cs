using System.Data.SQLite;
using DataAccess.Common;
using DataAccess.interfaces_auth;

namespace DataAccess.repository_auth
{
    public class UserRepository : IUserRepository
    {
        private readonly SQLiteConnection? _testConnection;

        public UserRepository() { }

        public UserRepository(SQLiteConnection testConn)
        {
            _testConnection = testConn;
        }

        public User? GetByUsername(string username)
        {
            try{
                var conn = SQLauth.GetConnection(_testConnection);
                const string sql = "SELECT username, role, status FROM users WHERE username = @u";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@u", username);
                using var reader = cmd.ExecuteReader();
                return reader.Read()
                ? new User
                    {
                        Username = reader.GetString(0),
                        Role = reader.GetString(1),
                        Status = reader.IsDBNull(2) ? null : reader.GetString(2)
                    }
                : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[UserRepository.GetByUsername] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[UserRepository.GetByUsername] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public List<User> GetAllByRole(string roleName)
        {
            try {
                var conn = SQLauth.GetConnection(_testConnection);
                const string sql = "SELECT username, role, status FROM users WHERE role = @r";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@r", roleName);
                using var reader = cmd.ExecuteReader();
                var list = new List<User>();
                while (reader.Read())
                    list.Add(new User
                    {
                        Username = reader.GetString(0),
                        Role = reader.GetString(1),
                        Status = reader.IsDBNull(2) ? null : reader.GetString(2)
                    });
                return list;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[UserRepository.GetAllByRole] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[UserRepository.GetAllByRole] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public void Create(User user)
        {
            try {
            var conn = SQLauth.GetConnection(_testConnection);
            const string sql = "INSERT INTO users(username, role, status) VALUES(@u, @r, @s)";
            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@u", user.Username);
            cmd.Parameters.AddWithValue("@r", user.Role);
            cmd.Parameters.AddWithValue("@s", (object?)user.Status ?? DBNull.Value);
            cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[UserRepository.Create] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[UserRepository.Create] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public void UpdateStatus(string username, string? newStatus)
        {
            try {
                var conn = SQLauth.GetConnection(_testConnection);
                const string sql = "UPDATE users SET status = @s WHERE username = @u";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@s", (object?)newStatus ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[UserRepository.UpdateStatus] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[UserRepository.UpdateStatus] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public void Delete(string username)
        {
            try {
                var conn = SQLauth.GetConnection(_testConnection);
                const string sql = "DELETE FROM users WHERE username = @u";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@u", username);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[UserRepository.Delete] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[UserRepository.Delete] Unexpected error: {ex.Message}");
                throw;
            }
        }
    }
}
