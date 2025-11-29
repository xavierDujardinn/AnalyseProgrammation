using System.Data.SQLite;
using game.Models;

namespace game.DataAccess
{
    internal class BatimentNiveauRepository : IBatimentNiveauRepository
    {
        private readonly SQLiteConnection? _testConnection;

        public BatimentNiveauRepository() { }

        public BatimentNiveauRepository(SQLiteConnection testConn)
        {
            _testConnection = testConn;
        }

        public BatimentNiveau? GetById(int bnId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT bn_id, batiment_id, niveau, couts, prod, prerequis FROM batiments_niveaux WHERE bn_id = @id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", bnId);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new BatimentNiveau
                {
                    BnId = reader.GetInt32(0),
                    BatimentId = reader.GetInt32(1),
                    Niveau = reader.GetInt32(2),
                    Couts = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Prod = reader.IsDBNull(4) ? null : reader.GetString(4),
                    Prerequis = reader.IsDBNull(5) ? null : reader.GetString(5)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[BatimentNiveauRepository.GetById] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[BatimentNiveauRepository.GetById] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public List<BatimentNiveau> GetAllByBatiment(int batimentId)
        {
            try {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT bn_id, batiment_id, niveau, couts, prod, prerequis FROM batiments_niveaux WHERE batiment_id = @batimentId";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@batimentId", batimentId);
                using var reader = cmd.ExecuteReader();
                var list = new List<BatimentNiveau>();
                while (reader.Read())
                {
                    list.Add(new BatimentNiveau
                    {
                        BnId = reader.GetInt32(0),
                        BatimentId = reader.GetInt32(1),
                        Niveau = reader.GetInt32(2),
                        Couts = reader.IsDBNull(3) ? null : reader.GetString(3),
                        Prod = reader.IsDBNull(4) ? null : reader.GetString(4),
                        Prerequis = reader.IsDBNull(5) ? null : reader.GetString(5)
                    });
                }
                return list;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[BatimentNiveauRepository.GetAllByBatiment] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[BatimentNiveauRepository.GetAllByBatiment] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public BatimentNiveau? GetByBatimentAndNiveau(int batimentId, int niveau)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT bn_id, batiment_id, niveau, couts, prod, prerequis FROM batiments_niveaux WHERE batiment_id = @batimentId AND niveau = @niveau";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@batimentId", batimentId);
                cmd.Parameters.AddWithValue("@niveau", niveau);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new BatimentNiveau
                {
                    BnId = reader.GetInt32(0),
                    BatimentId = reader.GetInt32(1),
                    Niveau = reader.GetInt32(2),
                    Couts = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Prod = reader.IsDBNull(4) ? null : reader.GetString(4),
                    Prerequis = reader.IsDBNull(5) ? null : reader.GetString(5)
                }
                : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[BatimentNiveauRepository.GetByBatimentAndNiveau] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[BatimentNiveauRepository.GetByBatimentAndNiveau] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public void Create(BatimentNiveau niveau)
        {
            try{
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "INSERT INTO batiments_niveaux (batiment_id, niveau, couts, prod, prerequis) VALUES (@batimentId, @niveau, @couts, @prod, @prerequis)";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@batimentId", niveau.BatimentId);
                cmd.Parameters.AddWithValue("@niveau", niveau.Niveau);
                cmd.Parameters.AddWithValue("@couts", (object?)niveau.Couts ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@prod", (object?)niveau.Prod ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@prerequis", (object?)niveau.Prerequis ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[BatimentNiveauRepository.Create] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[BatimentNiveauRepository.Create] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public void Update(BatimentNiveau niveau)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = @"UPDATE batiments_niveaux SET batiment_id=@batimentId, niveau=@niveau, couts=@couts, prod=@prod, prerequis=@prerequis WHERE bn_id=@bnId";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@bnId", niveau.BnId);
                cmd.Parameters.AddWithValue("@batimentId", niveau.BatimentId);
                cmd.Parameters.AddWithValue("@niveau", niveau.Niveau);
                cmd.Parameters.AddWithValue("@couts", (object?)niveau.Couts ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@prod", (object?)niveau.Prod ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@prerequis", (object?)niveau.Prerequis ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[BatimentNiveauRepository.Update] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[BatimentNiveauRepository.Update] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public void Delete(int bnId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "DELETE FROM batiments_niveaux WHERE bn_id = @id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", bnId);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[BatimentNiveauRepository.Delete] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[BatimentNiveauRepository.Delete] Unexpected error: {ex.Message}");
                throw;
            }
        }
    }
}
