using System.Data.SQLite;
using game.Models;

namespace game.DataAccess
{
    internal class RessourceRefRepository : IRessourceRefRepository
    {
        private readonly SQLiteConnection? _testConnection;

        // Constructeur production
        public RessourceRefRepository() { }

        // Constructeur test : injection de connexion
        public RessourceRefRepository(SQLiteConnection testConn)
        {
            _testConnection = testConn;
        }

        public RessourceRef? GetById(int ressourceId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT ressource_id, code, libelle, unite FROM ressources_ref WHERE ressource_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", ressourceId);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new RessourceRef
                {
                    RessourceId = reader.GetInt32(0),
                    Code = reader.GetString(1),
                    Libelle = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Unite = reader.IsDBNull(3) ? null : reader.GetString(3)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[RessourceRefRepository.GetById] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[RessourceRefRepository.GetById] General error: {ex.Message}");
                throw;
            }
        }

        public RessourceRef? GetByCode(string code)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT ressource_id, code, libelle, unite FROM ressources_ref WHERE code=@code";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@code", code);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new RessourceRef
                {
                    RessourceId = reader.GetInt32(0),
                    Code = reader.GetString(1),
                    Libelle = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Unite = reader.IsDBNull(3) ? null : reader.GetString(3)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[RessourceRefRepository.GetByCode] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[RessourceRefRepository.GetByCode] General error: {ex.Message}");
                throw;
            }
        }

        public List<RessourceRef> GetAll()
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT ressource_id, code, libelle, unite FROM ressources_ref";
                using var cmd = new SQLiteCommand(sql, conn);
                using var reader = cmd.ExecuteReader();
                var list = new List<RessourceRef>();
                while (reader.Read())
                {
                    list.Add(new RessourceRef
                    {
                        RessourceId = reader.GetInt32(0),
                        Code = reader.GetString(1),
                        Libelle = reader.IsDBNull(2) ? null : reader.GetString(2),
                        Unite = reader.IsDBNull(3) ? null : reader.GetString(3)
                    });
                }
                return list;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[RessourceRefRepository.GetAll] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[RessourceRefRepository.GetAll] General error: {ex.Message}");
                throw;
            }
        }

        public void Create(RessourceRef ressource)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "INSERT INTO ressources_ref (code, libelle, unite) VALUES (@code, @libelle, @unite)";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@code", ressource.Code);
                cmd.Parameters.AddWithValue("@libelle", (object?)ressource.Libelle ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@unite", (object?)ressource.Unite ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[RessourceRefRepository.Create] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[RessourceRefRepository.Create] General error: {ex.Message}");
                throw;
            }
        }

        public void Update(RessourceRef ressource)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = @"UPDATE ressources_ref SET code=@code, libelle=@libelle, unite=@unite WHERE ressource_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", ressource.RessourceId);
                cmd.Parameters.AddWithValue("@code", ressource.Code);
                cmd.Parameters.AddWithValue("@libelle", (object?)ressource.Libelle ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@unite", (object?)ressource.Unite ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[RessourceRefRepository.Update] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[RessourceRefRepository.Update] General error: {ex.Message}");
                throw;
            }
        }

        public void Delete(int ressourceId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "DELETE FROM ressources_ref WHERE ressource_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", ressourceId);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[RessourceRefRepository.Delete] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[RessourceRefRepository.Delete] General error: {ex.Message}");
                throw;
            }
        }
    }
}
