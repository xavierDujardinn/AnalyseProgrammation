using System.Data.SQLite;
using game.Models;

namespace game.DataAccess
{
    internal class SauvegardeRepository : ISauvegardeRepository
    {
        private readonly SQLiteConnection? _testConnection;

        // Constructeur production
        public SauvegardeRepository() { }

        // Constructeur test : injection de connexion
        public SauvegardeRepository(SQLiteConnection testConn)
        {
            _testConnection = testConn;
        }

        public Sauvegarde? GetById(int sauvegardeId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT sauvegarde_id, partie_id, slot, nom, tour, snapshot, created_at, temps FROM sauvegardes WHERE sauvegarde_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", sauvegardeId);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new Sauvegarde
                {
                    SauvegardeId = reader.GetInt32(0),
                    PartieId = reader.GetInt32(1),
                    Slot = reader.GetInt32(2),
                    Nom = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Tour = reader.GetInt32(4),
                    Snapshot = reader.GetString(5),
                    CreatedAt = reader.IsDBNull(6) ? null : reader.GetString(6),
                    Temps = reader.IsDBNull(7) ? 0 : reader.GetInt32(7)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[SauvegardeRepository.GetById] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[SauvegardeRepository.GetById] General error: {ex.Message}");
                throw;
            }
        }

        public List<Sauvegarde> GetAllByPartie(int partieId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT sauvegarde_id, partie_id, slot, nom, tour, snapshot, created_at, temps FROM sauvegardes WHERE partie_id=@partieId";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", partieId);
                using var reader = cmd.ExecuteReader();
                var list = new List<Sauvegarde>();
                while (reader.Read())
                {
                    list.Add(new Sauvegarde
                    {
                        SauvegardeId = reader.GetInt32(0),
                        PartieId = reader.GetInt32(1),
                        Slot = reader.GetInt32(2),
                        Nom = reader.IsDBNull(3) ? null : reader.GetString(3),
                        Tour = reader.GetInt32(4),
                        Snapshot = reader.GetString(5),
                        CreatedAt = reader.IsDBNull(6) ? null : reader.GetString(6),
                        Temps = reader.IsDBNull(7) ? 0 : reader.GetInt32(7)
                    });
                }
                return list;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[SauvegardeRepository.GetAllByPartie] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[SauvegardeRepository.GetAllByPartie] General error: {ex.Message}");
                throw;
            }
        }

        public Sauvegarde? GetBySlot(int partieId, int slot)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT sauvegarde_id, partie_id, slot, nom, tour, snapshot, created_at, temps FROM sauvegardes WHERE partie_id=@partieId AND slot=@slot";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", partieId);
                cmd.Parameters.AddWithValue("@slot", slot);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new Sauvegarde
                {
                    SauvegardeId = reader.GetInt32(0),
                    PartieId = reader.GetInt32(1),
                    Slot = reader.GetInt32(2),
                    Nom = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Tour = reader.GetInt32(4),
                    Snapshot = reader.GetString(5),
                    CreatedAt = reader.IsDBNull(6) ? null : reader.GetString(6),
                    Temps = reader.IsDBNull(7) ? 0 : reader.GetInt32(7)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[SauvegardeRepository.GetBySlot] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[SauvegardeRepository.GetBySlot] General error: {ex.Message}");
                throw;
            }
        }

        public void Create(Sauvegarde sauvegarde)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "INSERT INTO sauvegardes (partie_id, slot, nom, tour, snapshot, created_at, temps) VALUES (@partieId, @slot, @nom, @tour, @snapshot, @createdAt, @temps)";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", sauvegarde.PartieId);
                cmd.Parameters.AddWithValue("@slot", sauvegarde.Slot);
                cmd.Parameters.AddWithValue("@nom", (object?)sauvegarde.Nom ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@tour", sauvegarde.Tour);
                cmd.Parameters.AddWithValue("@snapshot", sauvegarde.Snapshot);
                cmd.Parameters.AddWithValue("@createdAt", (object?)sauvegarde.CreatedAt ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@temps", sauvegarde.Temps);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[SauvegardeRepository.Create] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[SauvegardeRepository.Create] General error: {ex.Message}");
                throw;
            }
        }

        public void Update(Sauvegarde sauvegarde)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = @"UPDATE sauvegardes SET partie_id=@partieId, slot=@slot, nom=@nom, tour=@tour, snapshot=@snapshot, created_at=@createdAt, temps=@temps WHERE sauvegarde_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", sauvegarde.SauvegardeId);
                cmd.Parameters.AddWithValue("@partieId", sauvegarde.PartieId);
                cmd.Parameters.AddWithValue("@slot", sauvegarde.Slot);
                cmd.Parameters.AddWithValue("@nom", (object?)sauvegarde.Nom ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@tour", sauvegarde.Tour);
                cmd.Parameters.AddWithValue("@snapshot", sauvegarde.Snapshot);
                cmd.Parameters.AddWithValue("@createdAt", (object?)sauvegarde.CreatedAt ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@temps", sauvegarde.Temps);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[SauvegardeRepository.Update] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[SauvegardeRepository.Update] General error: {ex.Message}");
                throw;
            }
        }

        public void Delete(int sauvegardeId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "DELETE FROM sauvegardes WHERE sauvegarde_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", sauvegardeId);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[SauvegardeRepository.Delete] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[SauvegardeRepository.Delete] General error: {ex.Message}");
                throw;
            }
        }
    }
}
