using System.Data.SQLite;
using DataAccess.Common;
using DataAccess.interface_jeu;

namespace DataAccess.repository_jeu
{
    public class PartieRepository : IPartieRepository
    {
        private readonly SQLiteConnection? _testConnection;

        // Constructeur production
        public PartieRepository() { }

        // Constructeur test : injection de connexion
        public PartieRepository(SQLiteConnection testConn)
        {
            _testConnection = testConn;
        }

        public Partie? GetById(int partieId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT partie_id, nom, seed, statut, created_at, updated_at FROM parties WHERE partie_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", partieId);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new Partie
                {
                    PartieId = reader.GetInt32(0),
                    Nom = reader.IsDBNull(1) ? null : reader.GetString(1),
                    Seed = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                    Statut = reader.IsDBNull(3) ? null : reader.GetString(3),
                    CreatedAt = reader.IsDBNull(4) ? null : reader.GetString(4),
                    UpdatedAt = reader.IsDBNull(5) ? null : reader.GetString(5)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[PartieRepository.GetById] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[PartieRepository.GetById] General error: {ex.Message}");
                throw;
            }
        }

        public List<Partie> GetAll()
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT partie_id, nom, seed, statut, created_at, updated_at FROM parties";
                using var cmd = new SQLiteCommand(sql, conn);
                using var reader = cmd.ExecuteReader();
                var list = new List<Partie>();
                while (reader.Read())
                {
                    list.Add(new Partie
                    {
                        PartieId = reader.GetInt32(0),
                        Nom = reader.IsDBNull(1) ? null : reader.GetString(1),
                        Seed = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                        Statut = reader.IsDBNull(3) ? null : reader.GetString(3),
                        CreatedAt = reader.IsDBNull(4) ? null : reader.GetString(4),
                        UpdatedAt = reader.IsDBNull(5) ? null : reader.GetString(5)
                    });
                }
                return list;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[PartieRepository.GetAll] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[PartieRepository.GetAll] General error: {ex.Message}");
                throw;
            }
        }

        public void Create(Partie partie)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "INSERT INTO parties (nom, seed, statut, created_at, updated_at) VALUES (@nom, @seed, @statut, @createdAt, @updatedAt)";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nom", (object?)partie.Nom ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@seed", (object?)partie.Seed ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@statut", (object?)partie.Statut ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@createdAt", (object?)partie.CreatedAt ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@updatedAt", (object?)partie.UpdatedAt ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[PartieRepository.Create] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[PartieRepository.Create] General error: {ex.Message}");
                throw;
            }
        }

        public void Update(Partie partie)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = @"UPDATE parties SET nom=@nom, seed=@seed, statut=@statut, created_at=@createdAt, updated_at=@updatedAt WHERE partie_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", partie.PartieId);
                cmd.Parameters.AddWithValue("@nom", (object?)partie.Nom ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@seed", (object?)partie.Seed ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@statut", (object?)partie.Statut ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@createdAt", (object?)partie.CreatedAt ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@updatedAt", (object?)partie.UpdatedAt ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[PartieRepository.Update] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[PartieRepository.Update] General error: {ex.Message}");
                throw;
            }
        }

        public void Delete(int partieId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "DELETE FROM parties WHERE partie_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", partieId);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[PartieRepository.Delete] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[PartieRepository.Delete] General error: {ex.Message}");
                throw;
            }
        }

        public List<Partie> GetByName(string nom)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT partie_id, nom, seed, statut, created_at, updated_at FROM parties WHERE nom=@nom";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nom", nom);
                using var reader = cmd.ExecuteReader();
                var list = new List<Partie>();
                while (reader.Read())
                {
                    list.Add(new Partie
                    {
                        PartieId = reader.GetInt32(0),
                        Nom = reader.IsDBNull(1) ? null : reader.GetString(1),
                        Seed = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                        Statut = reader.IsDBNull(3) ? null : reader.GetString(3),
                        CreatedAt = reader.IsDBNull(4) ? null : reader.GetString(4),
                        UpdatedAt = reader.IsDBNull(5) ? null : reader.GetString(5)
                    });
                }
                return list;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[PartieRepository.GetByName] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[PartieRepository.GetByName] General error: {ex.Message}");
                throw;
            }
        }

        public List<Partie> GetByStatus(string statut)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT partie_id, nom, seed, statut, created_at, updated_at FROM parties WHERE statut=@statut";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@statut", statut);
                using var reader = cmd.ExecuteReader();
                var list = new List<Partie>();
                while (reader.Read())
                {
                    list.Add(new Partie
                    {
                        PartieId = reader.GetInt32(0),
                        Nom = reader.IsDBNull(1) ? null : reader.GetString(1),
                        Seed = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                        Statut = reader.IsDBNull(3) ? null : reader.GetString(3),
                        CreatedAt = reader.IsDBNull(4) ? null : reader.GetString(4),
                        UpdatedAt = reader.IsDBNull(5) ? null : reader.GetString(5)
                    });
                }
                return list;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[PartieRepository.GetByStatus] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[PartieRepository.GetByStatus] General error: {ex.Message}");
                throw;
            }
        }
    }
}
