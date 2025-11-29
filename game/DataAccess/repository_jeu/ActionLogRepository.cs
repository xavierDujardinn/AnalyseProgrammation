using System.Data.SQLite;
using DataAccess.Common;
using DataAccess.interface_jeu;

namespace DataAccess.repository_jeu
{
    public class ActionLogRepository : IActionLogRepository
    {
        private readonly SQLiteConnection? _testConnection;

        public ActionLogRepository() { }

        public ActionLogRepository(SQLiteConnection testConn)
        {
            _testConnection = testConn;
        }

        public ActionLog? GetById(int logId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT log_id, partie_id, joueur_id, type, payload, ts FROM actions_log WHERE log_id = @id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", logId);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new ActionLog
                {
                    LogId = reader.GetInt32(0),
                    PartieId = reader.GetInt32(1),
                    JoueurId = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                    Type = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Payload = reader.IsDBNull(4) ? null : reader.GetString(4),
                    Ts = reader.IsDBNull(5) ? null : reader.GetString(5)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[ActionLogRepository.GetById] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[ActionLogRepository.GetById] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public List<ActionLog> GetAllByPartie(int partieId)
        {
            try {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT log_id, partie_id, joueur_id, type, payload, ts FROM actions_log WHERE partie_id = @partieId";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", partieId);
                using var reader = cmd.ExecuteReader();
                var list = new List<ActionLog>();
                while (reader.Read())
                {
                    list.Add(new ActionLog
                    {
                        LogId = reader.GetInt32(0),
                        PartieId = reader.GetInt32(1),
                        JoueurId = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                        Type = reader.IsDBNull(3) ? null : reader.GetString(3),
                        Payload = reader.IsDBNull(4) ? null : reader.GetString(4),
                        Ts = reader.IsDBNull(5) ? null : reader.GetString(5)
                    });
                }
                return list;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[ActionLogRepository.GetAllByPartie] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[ActionLogRepository.GetAllByPartie] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public List<ActionLog> GetAllByJoueur(int joueurId, int partieId)
        {
            try {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT log_id, partie_id, joueur_id, type, payload, ts FROM actions_log WHERE joueur_id = @joueurId AND partie_id = @partieId";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@joueurId", joueurId);
                cmd.Parameters.AddWithValue("@partieId", partieId);
                using var reader = cmd.ExecuteReader();
                var list = new List<ActionLog>();
                while (reader.Read())
                {
                    list.Add(new ActionLog
                    {
                        LogId = reader.GetInt32(0),
                        PartieId = reader.GetInt32(1),
                        JoueurId = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                        Type = reader.IsDBNull(3) ? null : reader.GetString(3),
                        Payload = reader.IsDBNull(4) ? null : reader.GetString(4),
                        Ts = reader.IsDBNull(5) ? null : reader.GetString(5)
                    });
                }
                return list;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[ActionLogRepository.GetAllByJoueur] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[ActionLogRepository.GetAllByJoueur] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public void Create(ActionLog log)
        {
            try 
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "INSERT INTO actions_log (partie_id, joueur_id, type, payload, ts) VALUES (@partieId, @joueurId, @type, @payload, @ts)";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", log.PartieId);
                cmd.Parameters.AddWithValue("@joueurId", (object?)log.JoueurId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@type", (object?)log.Type ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@payload", (object?)log.Payload ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ts", (object?)log.Ts ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[ActionLogRepository.Create] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[ActionLogRepository.Create] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public void Delete(int logId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "DELETE FROM actions_log WHERE log_id = @id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", logId);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[ActionLogRepository.Delete] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[ActionLogRepository.Delete] Unexpected error: {ex.Message}");
                throw;
            }
        }
    }
}
