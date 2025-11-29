using System.Data.SQLite;
using game.Models;

namespace game.DataAccess
{
    internal class RechercheRefRepository : IRechercheRefRepository
    {
        private readonly SQLiteConnection? _testConnection;

        // Constructeur production
        public RechercheRefRepository() { }

        // Constructeur test : injection de connexion
        public RechercheRefRepository(SQLiteConnection testConn)
        {
            _testConnection = testConn;
        }

        public RechercheRef? GetById(int rechercheId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT recherche_id, code, libelle, prerequis, effets FROM recherches_ref WHERE recherche_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", rechercheId);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new RechercheRef
                {
                    RechercheId = reader.GetInt32(0),
                    Code = reader.GetString(1),
                    Libelle = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Prerequis = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Effets = reader.IsDBNull(4) ? null : reader.GetString(4)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[RechercheRefRepository.GetById] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[RechercheRefRepository.GetById] General error: {ex.Message}");
                throw;
            }
        }

        public RechercheRef? GetByCode(string code)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT recherche_id, code, libelle, prerequis, effets FROM recherches_ref WHERE code=@code";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@code", code);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new RechercheRef
                {
                    RechercheId = reader.GetInt32(0),
                    Code = reader.GetString(1),
                    Libelle = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Prerequis = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Effets = reader.IsDBNull(4) ? null : reader.GetString(4)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[RechercheRefRepository.GetByCode] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[RechercheRefRepository.GetByCode] General error: {ex.Message}");
                throw;
            }
        }

        public List<RechercheRef> GetAll()
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT recherche_id, code, libelle, prerequis, effets FROM recherches_ref";
                using var cmd = new SQLiteCommand(sql, conn);
                using var reader = cmd.ExecuteReader();
                var list = new List<RechercheRef>();
                while (reader.Read())
                {
                    list.Add(new RechercheRef
                    {
                        RechercheId = reader.GetInt32(0),
                        Code = reader.GetString(1),
                        Libelle = reader.IsDBNull(2) ? null : reader.GetString(2),
                        Prerequis = reader.IsDBNull(3) ? null : reader.GetString(3),
                        Effets = reader.IsDBNull(4) ? null : reader.GetString(4)
                    });
                }
                return list;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[RechercheRefRepository.GetAll] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[RechercheRefRepository.GetAll] General error: {ex.Message}");
                throw;
            }
        }

        public void Create(RechercheRef recherche)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "INSERT INTO recherches_ref (code, libelle, prerequis, effets) VALUES (@code, @libelle, @prerequis, @effets)";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@code", recherche.Code);
                cmd.Parameters.AddWithValue("@libelle", (object?)recherche.Libelle ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@prerequis", (object?)recherche.Prerequis ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@effets", (object?)recherche.Effets ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[RechercheRefRepository.Create] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[RechercheRefRepository.Create] General error: {ex.Message}");
                throw;
            }
        }

        public void Update(RechercheRef recherche)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = @"UPDATE recherches_ref SET code=@code, libelle=@libelle, prerequis=@prerequis, effets=@effets WHERE recherche_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", recherche.RechercheId);
                cmd.Parameters.AddWithValue("@code", recherche.Code);
                cmd.Parameters.AddWithValue("@libelle", (object?)recherche.Libelle ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@prerequis", (object?)recherche.Prerequis ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@effets", (object?)recherche.Effets ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[RechercheRefRepository.Update] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[RechercheRefRepository.Update] General error: {ex.Message}");
                throw;
            }
        }

        public void Delete(int rechercheId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "DELETE FROM recherches_ref WHERE recherche_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", rechercheId);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[RechercheRefRepository.Delete] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[RechercheRefRepository.Delete] General error: {ex.Message}");
                throw;
            }
        }
    }
}
