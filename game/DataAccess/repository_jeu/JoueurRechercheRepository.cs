using System.Data.SQLite;
using DataAccess.Common;
using DataAccess.interface_jeu;

namespace DataAccess.repository_jeu
{
    public class JoueurRechercheRepository : IJoueurRechercheRepository
    {
        private readonly SQLiteConnection? _testConnection;

        // Constructeur production
        public JoueurRechercheRepository() { }

        // Constructeur test : injection de connexion
        public JoueurRechercheRepository(SQLiteConnection testConn)
        {
            _testConnection = testConn;
        }

        public JoueurRecherche? Get(int jrId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT jr_id, partie_id, joueur_id, recherche_id, etat, progression FROM joueurs_recherches WHERE jr_id = @id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", jrId);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new JoueurRecherche
                {
                    JrId = reader.GetInt32(0),
                    PartieId = reader.GetInt32(1),
                    JoueurId = reader.GetInt32(2),
                    RechercheId = reader.GetInt32(3),
                    Etat = reader.IsDBNull(4) ? null : reader.GetString(4),
                    Progression = reader.IsDBNull(5) ? (double?)null : reader.GetDouble(5)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[JoueurRechercheRepository.Get] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[JoueurRechercheRepository.Get] General error: {ex.Message}");
                throw;
            }
        }

        public List<JoueurRecherche> GetAllByJoueur(int joueurId, int partieId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT jr_id, partie_id, joueur_id, recherche_id, etat, progression FROM joueurs_recherches WHERE joueur_id = @joueurId AND partie_id = @partieId";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@joueurId", joueurId);
                cmd.Parameters.AddWithValue("@partieId", partieId);
                using var reader = cmd.ExecuteReader();
                var list = new List<JoueurRecherche>();
                while (reader.Read())
                {
                    list.Add(new JoueurRecherche
                    {
                        JrId = reader.GetInt32(0),
                        PartieId = reader.GetInt32(1),
                        JoueurId = reader.GetInt32(2),
                        RechercheId = reader.GetInt32(3),
                        Etat = reader.IsDBNull(4) ? null : reader.GetString(4),
                        Progression = reader.IsDBNull(5) ? (double?)null : reader.GetDouble(5)
                    });
                }
                return list;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[JoueurRechercheRepository.GetAllByJoueur] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[JoueurRechercheRepository.GetAllByJoueur] General error: {ex.Message}");
                throw;
            }
        }

        public void Create(JoueurRecherche jr)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "INSERT INTO joueurs_recherches (partie_id, joueur_id, recherche_id, etat, progression) VALUES (@partieId, @joueurId, @rechercheId, @etat, @progression)";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", jr.PartieId);
                cmd.Parameters.AddWithValue("@joueurId", jr.JoueurId);
                cmd.Parameters.AddWithValue("@rechercheId", jr.RechercheId);
                cmd.Parameters.AddWithValue("@etat", (object?)jr.Etat ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@progression", (object?)jr.Progression ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[JoueurRechercheRepository.Create] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[JoueurRechercheRepository.Create] General error: {ex.Message}");
                throw;
            }
        }

        public void Update(JoueurRecherche jr)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = @"UPDATE joueurs_recherches SET partie_id=@partieId, joueur_id=@joueurId, recherche_id=@rechercheId, etat=@etat, progression=@progression WHERE jr_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", jr.JrId);
                cmd.Parameters.AddWithValue("@partieId", jr.PartieId);
                cmd.Parameters.AddWithValue("@joueurId", jr.JoueurId);
                cmd.Parameters.AddWithValue("@rechercheId", jr.RechercheId);
                cmd.Parameters.AddWithValue("@etat", (object?)jr.Etat ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@progression", (object?)jr.Progression ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[JoueurRechercheRepository.Update] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[JoueurRechercheRepository.Update] General error: {ex.Message}");
                throw;
            }
        }

        public void Delete(int jrId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "DELETE FROM joueurs_recherches WHERE jr_id = @id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", jrId);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[JoueurRechercheRepository.Delete] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[JoueurRechercheRepository.Delete] General error: {ex.Message}");
                throw;
            }
        }
    }
}
