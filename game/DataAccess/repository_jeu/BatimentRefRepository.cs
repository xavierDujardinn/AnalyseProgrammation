using System.Data.SQLite;
using game.Models;

namespace game.DataAccess
{
    internal class BatimentRefRepository : IBatimentRefRepository
    {
        private readonly SQLiteConnection? _testConnection;

        // Constructeur production
        public BatimentRefRepository() { }

        // Constructeur test : injection de connexion
        public BatimentRefRepository(SQLiteConnection testConn)
        {
            _testConnection = testConn;
        }

        public BatimentRef? GetById(int batimentId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT batiment_id, code, libelle, type, niveau_max FROM batiments_ref WHERE batiment_id = @id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", batimentId);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new BatimentRef
                {
                    BatimentId = reader.GetInt32(0),
                    Code = reader.IsDBNull(1) ? null : reader.GetString(1),
                    Libelle = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Type = reader.IsDBNull(3) ? null : reader.GetString(3),
                    NiveauMax = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[BatimentRefRepository.GetById] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[BatimentRefRepository.GetById] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public BatimentRef? GetByCode(string code)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT batiment_id, code, libelle, type, niveau_max FROM batiments_ref WHERE code = @code";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@code", code);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new BatimentRef
                {
                    BatimentId = reader.GetInt32(0),
                    Code = reader.IsDBNull(1) ? null : reader.GetString(1),
                    Libelle = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Type = reader.IsDBNull(3) ? null : reader.GetString(3),
                    NiveauMax = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[BatimentRefRepository.GetByCode] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[BatimentRefRepository.GetByCode] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public List<BatimentRef> GetAll()
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT batiment_id, code, libelle, type, niveau_max FROM batiments_ref";
                using var cmd = new SQLiteCommand(sql, conn);
                using var reader = cmd.ExecuteReader();
                var list = new List<BatimentRef>();
                while (reader.Read())
                {
                    list.Add(new BatimentRef
                    {
                        BatimentId = reader.GetInt32(0),
                        Code = reader.IsDBNull(1) ? null : reader.GetString(1),
                        Libelle = reader.IsDBNull(2) ? null : reader.GetString(2),
                        Type = reader.IsDBNull(3) ? null : reader.GetString(3),
                        NiveauMax = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4)
                    });
                }
                return list;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[BatimentRefRepository.GetAll] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[BatimentRefRepository.GetAll] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public void Create(BatimentRef batRef)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "INSERT INTO batiments_ref (code, libelle, type, niveau_max) VALUES (@code, @libelle, @type, @niveauMax)";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@code", batRef.Code);
                cmd.Parameters.AddWithValue("@libelle", (object?)batRef.Libelle ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@type", (object?)batRef.Type ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@niveauMax", (object?)batRef.NiveauMax ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[BatimentRefRepository.Create] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[BatimentRefRepository.Create] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public void Update(BatimentRef batRef)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = @"UPDATE batiments_ref SET code=@code, libelle=@libelle, type=@type, niveau_max=@niveauMax WHERE batiment_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", batRef.BatimentId);
                cmd.Parameters.AddWithValue("@code", batRef.Code);
                cmd.Parameters.AddWithValue("@libelle", (object?)batRef.Libelle ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@type", (object?)batRef.Type ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@niveauMax", (object?)batRef.NiveauMax ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[BatimentRefRepository.Update] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[BatimentRefRepository.Update] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public void Delete(int batimentId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "DELETE FROM batiments_ref WHERE batiment_id = @id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", batimentId);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[BatimentRefRepository.Delete] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[BatimentRefRepository.Delete] Unexpected error: {ex.Message}");
                throw;
            }
        }
    }
}
