using System.Data.SQLite;
using game.Models;

namespace game.DataAccess
{
    internal class JoueurBatimentRepository : IJoueurBatimentRepository
    {
        private readonly SQLiteConnection? _testConnection;

        // Constructeur production
        public JoueurBatimentRepository() { }

        // Constructeur test : injection de connexion
        public JoueurBatimentRepository(SQLiteConnection testConn)
        {
            _testConnection = testConn;
        }

        public JoueurBatiment? Get(int jbId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT jb_id, partie_id, joueur_id, case_id, batiment_id, niveau, etat, date_construction FROM joueurs_batiments WHERE jb_id = @id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", jbId);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new JoueurBatiment
                {
                    JbId = reader.GetInt32(0),
                    PartieId = reader.GetInt32(1),
                    JoueurId = reader.GetInt32(2),
                    CaseId = reader.GetInt32(3),
                    BatimentId = reader.GetInt32(4),
                    Niveau = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                    Etat = reader.IsDBNull(6) ? null : reader.GetString(6),
                    DateConstruction = reader.IsDBNull(7) ? null : reader.GetString(7)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[JoueurBatimentRepository.Get] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[JoueurBatimentRepository.Get] General error: {ex.Message}");
                throw;
            }
        }

        public List<JoueurBatiment> GetAllByJoueur(int joueurId, int partieId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT jb_id, partie_id, joueur_id, case_id, batiment_id, niveau, etat, date_construction FROM joueurs_batiments WHERE joueur_id = @joueurId AND partie_id = @partieId";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@joueurId", joueurId);
                cmd.Parameters.AddWithValue("@partieId", partieId);
                using var reader = cmd.ExecuteReader();
                var list = new List<JoueurBatiment>();
                while (reader.Read())
                {
                    list.Add(new JoueurBatiment
                    {
                        JbId = reader.GetInt32(0),
                        PartieId = reader.GetInt32(1),
                        JoueurId = reader.GetInt32(2),
                        CaseId = reader.GetInt32(3),
                        BatimentId = reader.GetInt32(4),
                        Niveau = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                        Etat = reader.IsDBNull(6) ? null : reader.GetString(6),
                        DateConstruction = reader.IsDBNull(7) ? null : reader.GetString(7)
                    });
                }
                return list;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[JoueurBatimentRepository.GetAllByJoueur] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[JoueurBatimentRepository.GetAllByJoueur] General error: {ex.Message}");
                throw;
            }
        }

        public void Create(JoueurBatiment jb)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "INSERT INTO joueurs_batiments (partie_id, joueur_id, case_id, batiment_id, niveau, etat, date_construction) VALUES (@partieId, @joueurId, @caseId, @batimentId, @niveau, @etat, @dateConstruction)";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", jb.PartieId);
                cmd.Parameters.AddWithValue("@joueurId", jb.JoueurId);
                cmd.Parameters.AddWithValue("@caseId", jb.CaseId);
                cmd.Parameters.AddWithValue("@batimentId", jb.BatimentId);
                cmd.Parameters.AddWithValue("@niveau", (object?)jb.Niveau ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@etat", (object?)jb.Etat ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@dateConstruction", (object?)jb.DateConstruction ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[JoueurBatimentRepository.Create] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[JoueurBatimentRepository.Create] General error: {ex.Message}");
                throw;
            }
        }

        public void Update(JoueurBatiment jb)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = @"UPDATE joueurs_batiments 
                    SET partie_id=@partieId, joueur_id=@joueurId, case_id=@caseId, batiment_id=@batimentId, niveau=@niveau, etat=@etat, date_construction=@dateConstruction 
                    WHERE jb_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", jb.JbId);
                cmd.Parameters.AddWithValue("@partieId", jb.PartieId);
                cmd.Parameters.AddWithValue("@joueurId", jb.JoueurId);
                cmd.Parameters.AddWithValue("@caseId", jb.CaseId);
                cmd.Parameters.AddWithValue("@batimentId", jb.BatimentId);
                cmd.Parameters.AddWithValue("@niveau", (object?)jb.Niveau ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@etat", (object?)jb.Etat ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@dateConstruction", (object?)jb.DateConstruction ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[JoueurBatimentRepository.Update] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[JoueurBatimentRepository.Update] General error: {ex.Message}");
                throw;
            }
        }

        public void Delete(int jbId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "DELETE FROM joueurs_batiments WHERE jb_id = @id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", jbId);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[JoueurBatimentRepository.Delete] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[JoueurBatimentRepository.Delete] General error: {ex.Message}");
                throw;
            }
        }
    }
}
