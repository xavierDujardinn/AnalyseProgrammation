using System.Data.SQLite;
using DataAccess.Common;
using DataAccess.interface_jeu;

namespace DataAccess.repository_jeu
{
    public class EvenementInstanRepository : IEvenementInstanRepository
    {
        private readonly SQLiteConnection? _testConnection;

        // Constructeur production
        public EvenementInstanRepository() { }

        // Constructeur test : injection de connexion
        public EvenementInstanRepository(SQLiteConnection testConn)
        {
            _testConnection = testConn;
        }

        public EvenementInstan? GetById(int eveninstId)
        {
            try{
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT eveninst_id, partie_id, tour, event_id, cible_type, cible_id, decision, resolu, created_at FROM evenements_instan WHERE eveninst_id = @id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", eveninstId);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new EvenementInstan
                {
                    EveninstId = reader.GetInt32(0),
                    PartieId = reader.GetInt32(1),
                    Tour = reader.GetInt32(2),
                    EventId = reader.GetInt32(3),
                    CibleType = reader.IsDBNull(4) ? null : reader.GetString(4),
                    CibleId = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                    Decision = reader.IsDBNull(6) ? null : reader.GetString(6),
                    Resolu = reader.IsDBNull(7) ? (int?)null : reader.GetInt32(7),
                    CreatedAt = reader.IsDBNull(8) ? null : reader.GetString(8)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[EvenementInstanRepository.GetById] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[EvenementInstanRepository.GetById] General error: {ex.Message}");
                throw;
            }
        }

        public List<EvenementInstan> GetAllByPartie(int partieId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT eveninst_id, partie_id, tour, event_id, cible_type, cible_id, decision, resolu, created_at FROM evenements_instan WHERE partie_id = @partieId";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", partieId);
                using var reader = cmd.ExecuteReader();
                var list = new List<EvenementInstan>();
                while (reader.Read())
                {
                    list.Add(new EvenementInstan
                    {
                        EveninstId = reader.GetInt32(0),
                        PartieId = reader.GetInt32(1),
                        Tour = reader.GetInt32(2),
                        EventId = reader.GetInt32(3),
                        CibleType = reader.IsDBNull(4) ? null : reader.GetString(4),
                        CibleId = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                        Decision = reader.IsDBNull(6) ? null : reader.GetString(6),
                        Resolu = reader.IsDBNull(7) ? (int?)null : reader.GetInt32(7),
                        CreatedAt = reader.IsDBNull(8) ? null : reader.GetString(8)
                    });
                }
                return list;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[EvenementInstanRepository.GetAllByPartie] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[EvenementInstanRepository.GetAllByPartie] General error: {ex.Message}");
                throw;
            }
        }

        public List<EvenementInstan> GetAllByTour(int partieId, int tour)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT eveninst_id, partie_id, tour, event_id, cible_type, cible_id, decision, resolu, created_at FROM evenements_instan WHERE partie_id = @partieId AND tour = @tour";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", partieId);
                cmd.Parameters.AddWithValue("@tour", tour);
                using var reader = cmd.ExecuteReader();
                var list = new List<EvenementInstan>();
                while (reader.Read())
                {
                    list.Add(new EvenementInstan
                    {
                        EveninstId = reader.GetInt32(0),
                        PartieId = reader.GetInt32(1),
                        Tour = reader.GetInt32(2),
                        EventId = reader.GetInt32(3),
                        CibleType = reader.IsDBNull(4) ? null : reader.GetString(4),
                        CibleId = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                        Decision = reader.IsDBNull(6) ? null : reader.GetString(6),
                        Resolu = reader.IsDBNull(7) ? (int?)null : reader.GetInt32(7),
                        CreatedAt = reader.IsDBNull(8) ? null : reader.GetString(8)
                    });
                }
                return list;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[EvenementInstanRepository.GetAllByTour] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[EvenementInstanRepository.GetAllByTour] General error: {ex.Message}");
                throw;
            }
        }

        public void Create(EvenementInstan evenementInstan)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "INSERT INTO evenements_instan (partie_id, tour, event_id, cible_type, cible_id, decision, resolu, created_at) VALUES (@partieId, @tour, @eventId, @cibleType, @cibleId, @decision, @resolu, @createdAt)";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", evenementInstan.PartieId);
                cmd.Parameters.AddWithValue("@tour", evenementInstan.Tour);
                cmd.Parameters.AddWithValue("@eventId", evenementInstan.EventId);
                cmd.Parameters.AddWithValue("@cibleType", (object?)evenementInstan.CibleType ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@cibleId", (object?)evenementInstan.CibleId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@decision", (object?)evenementInstan.Decision ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@resolu", (object?)evenementInstan.Resolu ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@createdAt", (object?)evenementInstan.CreatedAt ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[EvenementInstanRepository.Create] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[EvenementInstanRepository.Create] General error: {ex.Message}");
                throw;
            }
        }

        public void Update(EvenementInstan evenementInstan)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = @"UPDATE evenements_instan SET partie_id = @partieId, tour = @tour, event_id = @eventId, cible_type = @cibleType, cible_id = @cibleId, decision = @decision, resolu = @resolu, created_at = @createdAt WHERE eveninst_id = @id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", evenementInstan.EveninstId);
                cmd.Parameters.AddWithValue("@partieId", evenementInstan.PartieId);
                cmd.Parameters.AddWithValue("@tour", evenementInstan.Tour);
                cmd.Parameters.AddWithValue("@eventId", evenementInstan.EventId);
                cmd.Parameters.AddWithValue("@cibleType", (object?)evenementInstan.CibleType ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@cibleId", (object?)evenementInstan.CibleId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@decision", (object?)evenementInstan.Decision ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@resolu", (object?)evenementInstan.Resolu ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@createdAt", (object?)evenementInstan.CreatedAt ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[EvenementInstanRepository.Update] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[EvenementInstanRepository.Update] General error: {ex.Message}");
                throw;
            }
        }

        public void Delete(int eveninstId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "DELETE FROM evenements_instan WHERE eveninst_id = @id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", eveninstId);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[EvenementInstanRepository.Delete] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[EvenementInstanRepository.Delete] General error: {ex.Message}");
                throw;
            }
        }
    }
}
