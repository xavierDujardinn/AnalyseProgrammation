using System.Data.SQLite;
using DataAccess.Common;
using DataAccess.interface_jeu;

namespace DataAccess.repository_jeu
{
    public class CaseRepository : ICaseRepository
    {
        private readonly SQLiteConnection? _testConnection;

        // Constructeur production
        public CaseRepository() { }

        // Constructeur test : injection de connexion
        public CaseRepository(SQLiteConnection testConn)
        {
            _testConnection = testConn;
        }

        public Case? GetById(int caseId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT case_id, partie_id, x, y, biome, decouverte FROM cases WHERE case_id = @id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", caseId);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new Case
                {
                    CaseId = reader.GetInt32(0),
                    PartieId = reader.GetInt32(1),
                    X = reader.GetInt32(2),
                    Y = reader.GetInt32(3),
                    Biome = reader.IsDBNull(4) ? null : reader.GetString(4),
                    Decouverte = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[CaseRepository.GetById] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[CaseRepository.GetById] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public List<Case> GetAllByPartie(int partieId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT case_id, partie_id, x, y, biome, decouverte FROM cases WHERE partie_id = @partieId";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", partieId);
                using var reader = cmd.ExecuteReader();
                var list = new List<Case>();
                while (reader.Read())
                {
                    list.Add(new Case
                    {
                        CaseId = reader.GetInt32(0),
                        PartieId = reader.GetInt32(1),
                        X = reader.GetInt32(2),
                        Y = reader.GetInt32(3),
                        Biome = reader.IsDBNull(4) ? null : reader.GetString(4),
                        Decouverte = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5)
                    });
                }
                return list;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[CaseRepository.GetAllByPartie] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[CaseRepository.GetAllByPartie] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public Case? GetByCoord(int partieId, int x, int y)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT case_id, partie_id, x, y, biome, decouverte FROM cases WHERE partie_id = @partieId AND x = @x AND y = @y";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", partieId);
                cmd.Parameters.AddWithValue("@x", x);
                cmd.Parameters.AddWithValue("@y", y);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new Case
                {
                    CaseId = reader.GetInt32(0),
                    PartieId = reader.GetInt32(1),
                    X = reader.GetInt32(2),
                    Y = reader.GetInt32(3),
                    Biome = reader.IsDBNull(4) ? null : reader.GetString(4),
                    Decouverte = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[CaseRepository.GetByCoord] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[CaseRepository.GetByCoord] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public void Create(Case c)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "INSERT INTO cases (partie_id, x, y, biome, decouverte) VALUES (@partieId, @x, @y, @biome, @decouverte)";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", c.PartieId);
                cmd.Parameters.AddWithValue("@x", c.X);
                cmd.Parameters.AddWithValue("@y", c.Y);
                cmd.Parameters.AddWithValue("@biome", (object?)c.Biome ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@decouverte", (object?)c.Decouverte ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[CaseRepository.Create] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[CaseRepository.Create] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public void Update(Case c)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = @"UPDATE cases SET partie_id=@partieId, x=@x, y=@y, biome=@biome, decouverte=@decouverte WHERE case_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", c.CaseId);
                cmd.Parameters.AddWithValue("@partieId", c.PartieId);
                cmd.Parameters.AddWithValue("@x", c.X);
                cmd.Parameters.AddWithValue("@y", c.Y);
                cmd.Parameters.AddWithValue("@biome", (object?)c.Biome ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@decouverte", (object?)c.Decouverte ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[CaseRepository.Update] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[CaseRepository.Update] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public void Delete(int caseId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "DELETE FROM cases WHERE case_id = @id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", caseId);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[CaseRepository.Delete] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[CaseRepository.Delete] Unexpected error: {ex.Message}");
                throw;
            }
        }
    }
}
