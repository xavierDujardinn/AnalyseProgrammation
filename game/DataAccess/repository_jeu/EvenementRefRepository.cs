using System.Data.SQLite;
using DataAccess.Common;
using DataAccess.interface_jeu;

namespace DataAccess.repository_jeu
{
    public class EvenementRefRepository : IEvenementRefRepository
    {
        private readonly SQLiteConnection? _testConnection;

        // Constructeur production
        public EvenementRefRepository() { }

        // Constructeur test : injection de connexion
        public EvenementRefRepository(SQLiteConnection testConn)
        {
            _testConnection = testConn;
        }

        public EvenementRef? GetById(int eventId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT event_id, code, libelle, type, effets, triggers FROM evenements_ref WHERE event_id = @id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", eventId);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new EvenementRef
                {
                    EventId = reader.GetInt32(0),
                    Code = reader.IsDBNull(1) ? null : reader.GetString(1),
                    Libelle = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Type = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Effets = reader.IsDBNull(4) ? null : reader.GetString(4),
                    Triggers = reader.IsDBNull(5) ? null : reader.GetString(5)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[EvenementRefRepository.GetById] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[EvenementRefRepository.GetById] General error: {ex.Message}");
                throw;
            }
        }

        public EvenementRef? GetByCode(string code)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT event_id, code, libelle, type, effets, triggers FROM evenements_ref WHERE code = @code";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@code", code);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new EvenementRef
                {
                    EventId = reader.GetInt32(0),
                    Code = reader.IsDBNull(1) ? null : reader.GetString(1),
                    Libelle = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Type = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Effets = reader.IsDBNull(4) ? null : reader.GetString(4),
                    Triggers = reader.IsDBNull(5) ? null : reader.GetString(5)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[EvenementRefRepository.GetByCode] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[EvenementRefRepository.GetByCode] General error: {ex.Message}");
                throw;
            }
        }

        public List<EvenementRef> GetAll()
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT event_id, code, libelle, type, effets, triggers FROM evenements_ref";
                using var cmd = new SQLiteCommand(sql, conn);
                using var reader = cmd.ExecuteReader();
                var list = new List<EvenementRef>();
                while (reader.Read())
                {
                    list.Add(new EvenementRef
                    {
                        EventId = reader.GetInt32(0),
                        Code = reader.IsDBNull(1) ? null : reader.GetString(1),
                        Libelle = reader.IsDBNull(2) ? null : reader.GetString(2),
                        Type = reader.IsDBNull(3) ? null : reader.GetString(3),
                        Effets = reader.IsDBNull(4) ? null : reader.GetString(4),
                        Triggers = reader.IsDBNull(5) ? null : reader.GetString(5)
                    });
                }
                return list;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[EvenementRefRepository.GetAll] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[EvenementRefRepository.GetAll] General error: {ex.Message}");
                throw;
            }
        }

        public void Create(EvenementRef evenementRef)
        {
            try {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "INSERT INTO evenements_ref (code, libelle, type, effets, triggers) VALUES (@code, @libelle, @type, @effets, @triggers)";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@code", evenementRef.Code);
                cmd.Parameters.AddWithValue("@libelle", (object?)evenementRef.Libelle ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@type", (object?)evenementRef.Type ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@effets", (object?)evenementRef.Effets ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@triggers", (object?)evenementRef.Triggers ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[EvenementRefRepository.Create] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[EvenementRefRepository.Create] General error: {ex.Message}");
                throw;
            }
        }

        public void Update(EvenementRef evenementRef)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = @"UPDATE evenements_ref SET code=@code, libelle=@libelle, type=@type, effets=@effets, triggers=@triggers WHERE event_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", evenementRef.EventId);
                cmd.Parameters.AddWithValue("@code", evenementRef.Code);
                cmd.Parameters.AddWithValue("@libelle", (object?)evenementRef.Libelle ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@type", (object?)evenementRef.Type ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@effets", (object?)evenementRef.Effets ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@triggers", (object?)evenementRef.Triggers ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[EvenementRefRepository.Update] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[EvenementRefRepository.Update] General error: {ex.Message}");
                throw;
            }
        }

        public void Delete(int eventId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "DELETE FROM evenements_ref WHERE event_id = @id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", eventId);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[EvenementRefRepository.Delete] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[EvenementRefRepository.Delete] General error: {ex.Message}");
                throw;
            }
        }
    }
}
