using System.Data.SQLite;
using game.Models;

namespace game.DataAccess
{
    internal class ExplorationRepository : IExplorationRepository
    {
        private readonly SQLiteConnection? _testConnection;

        // Constructeur production
        public ExplorationRepository() { }

        // Constructeur test : injection de connexion
        public ExplorationRepository(SQLiteConnection testConn)
        {
            _testConnection = testConn;
        }

        public Exploration? GetById(int explorationId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT exploration_id, partie_id, joueur_id, zone_id, status, resultats, created_at FROM explorations WHERE exploration_id = @id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", explorationId);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new Exploration
                {
                    ExplorationId = reader.GetInt32(0),
                    PartieId = reader.GetInt32(1),
                    JoueurId = reader.GetInt32(2),
                    ZoneId = reader.GetInt32(3),
                    Status = reader.IsDBNull(4) ? null : reader.GetString(4),
                    Resultats = reader.IsDBNull(5) ? null : reader.GetString(5),
                    CreatedAt = reader.IsDBNull(6) ? null : reader.GetString(6)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[ExplorationRepository.GetById] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[ExplorationRepository.GetById] General error: {ex.Message}");
                throw;
            }
        }

        public List<Exploration> GetAllByJoueur(int joueurId, int partieId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT exploration_id, partie_id, joueur_id, zone_id, status, resultats, created_at FROM explorations WHERE joueur_id = @joueurId AND partie_id = @partieId";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@joueurId", joueurId);
                cmd.Parameters.AddWithValue("@partieId", partieId);
                using var reader = cmd.ExecuteReader();
                var list = new List<Exploration>();
                while (reader.Read())
                {
                    list.Add(new Exploration
                    {
                        ExplorationId = reader.GetInt32(0),
                        PartieId = reader.GetInt32(1),
                        JoueurId = reader.GetInt32(2),
                        ZoneId = reader.GetInt32(3),
                        Status = reader.IsDBNull(4) ? null : reader.GetString(4),
                        Resultats = reader.IsDBNull(5) ? null : reader.GetString(5),
                        CreatedAt = reader.IsDBNull(6) ? null : reader.GetString(6)
                    });
                }
                return list;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[ExplorationRepository.GetAllByJoueur] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[ExplorationRepository.GetAllByJoueur] General error: {ex.Message}");
                throw;
            }
        }

        public List<Exploration> GetAllByZone(int zoneId, int partieId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT exploration_id, partie_id, joueur_id, zone_id, status, resultats, created_at FROM explorations WHERE zone_id = @zoneId AND partie_id = @partieId";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@zoneId", zoneId);
                cmd.Parameters.AddWithValue("@partieId", partieId);
                using var reader = cmd.ExecuteReader();
                var list = new List<Exploration>();
                while (reader.Read())
                {
                    list.Add(new Exploration
                    {
                        ExplorationId = reader.GetInt32(0),
                        PartieId = reader.GetInt32(1),
                        JoueurId = reader.GetInt32(2),
                        ZoneId = reader.GetInt32(3),
                        Status = reader.IsDBNull(4) ? null : reader.GetString(4),
                        Resultats = reader.IsDBNull(5) ? null : reader.GetString(5),
                        CreatedAt = reader.IsDBNull(6) ? null : reader.GetString(6)
                    });
                }
                return list;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[ExplorationRepository.GetAllByZone] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[ExplorationRepository.GetAllByZone] General error: {ex.Message}");
                throw;
            }
        }

        public void Create(Exploration exploration)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "INSERT INTO explorations (partie_id, joueur_id, zone_id, status, resultats, created_at) VALUES (@partieId, @joueurId, @zoneId, @status, @resultats, @createdAt)";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", exploration.PartieId);
                cmd.Parameters.AddWithValue("@joueurId", exploration.JoueurId);
                cmd.Parameters.AddWithValue("@zoneId", exploration.ZoneId);
                cmd.Parameters.AddWithValue("@status", (object?)exploration.Status ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@resultats", (object?)exploration.Resultats ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@createdAt", (object?)exploration.CreatedAt ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[ExplorationRepository.Create] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[ExplorationRepository.Create] General error: {ex.Message}");
                throw;
            }
        }

        public void Update(Exploration exploration)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = @"UPDATE explorations SET partie_id=@partieId, joueur_id=@joueurId, zone_id=@zoneId, status=@status, resultats=@resultats, created_at=@createdAt WHERE exploration_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", exploration.ExplorationId);
                cmd.Parameters.AddWithValue("@partieId", exploration.PartieId);
                cmd.Parameters.AddWithValue("@joueurId", exploration.JoueurId);
                cmd.Parameters.AddWithValue("@zoneId", exploration.ZoneId);
                cmd.Parameters.AddWithValue("@status", (object?)exploration.Status ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@resultats", (object?)exploration.Resultats ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@createdAt", (object?)exploration.CreatedAt ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[ExplorationRepository.Update] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[ExplorationRepository.Update] General error: {ex.Message}");
                throw;
            }
        }

        public void Delete(int explorationId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "DELETE FROM explorations WHERE exploration_id = @id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", explorationId);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[ExplorationRepository.Delete] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[ExplorationRepository.Delete] General error: {ex.Message}");
                throw;
            }
        }
    }
}
