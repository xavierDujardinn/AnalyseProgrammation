using System.Data.SQLite;
using game.Models;

namespace game.DataAccess
{
    internal class JoueurRessourceRepository : IJoueurRessourceRepository
    {
        private readonly SQLiteConnection? _testConnection;

        // Constructeur production
        public JoueurRessourceRepository() { }

        // Constructeur test : injection de connexion
        public JoueurRessourceRepository(SQLiteConnection testConn)
        {
            _testConnection = testConn;
        }

        public JoueurRessource? Get(int partieId, int joueurId, int ressourceId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT jr_id, partie_id, joueur_id, ressource_id, qty FROM joueurs_ressources WHERE partie_id=@partieId AND joueur_id=@joueurId AND ressource_id=@ressourceId";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", partieId);
                cmd.Parameters.AddWithValue("@joueurId", joueurId);
                cmd.Parameters.AddWithValue("@ressourceId", ressourceId);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new JoueurRessource
                {
                    JrId = reader.GetInt32(0),
                    PartieId = reader.GetInt32(1),
                    JoueurId = reader.GetInt32(2),
                    RessourceId = reader.GetInt32(3),
                    Qty = reader.IsDBNull(4) ? (double?)null : reader.GetDouble(4)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[JoueurRessourceRepository.Get] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[JoueurRessourceRepository.Get] General error: {ex.Message}");
                throw;
            }
        }

        public List<JoueurRessource> GetAllbyJoueur(int joueurId, int partieId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT jr_id, partie_id, joueur_id, ressource_id, qty FROM joueurs_ressources WHERE joueur_id=@joueurId AND partie_id=@partieId";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@joueurId", joueurId);
                cmd.Parameters.AddWithValue("@partieId", partieId);
                using var reader = cmd.ExecuteReader();
                var list = new List<JoueurRessource>();
                while (reader.Read())
                {
                    list.Add(new JoueurRessource
                    {
                        JrId = reader.GetInt32(0),
                        PartieId = reader.GetInt32(1),
                        JoueurId = reader.GetInt32(2),
                        RessourceId = reader.GetInt32(3),
                        Qty = reader.IsDBNull(4) ? (double?)null : reader.GetDouble(4)
                    });
                }
                return list;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[JoueurRessourceRepository.GetAllbyJoueur] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[JoueurRessourceRepository.GetAllbyJoueur] General error: {ex.Message}");
                throw;
            }
        }

        public void Create(JoueurRessource jr)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "INSERT INTO joueurs_ressources (partie_id, joueur_id, ressource_id, qty) VALUES (@partieId, @joueurId, @ressourceId, @qty)";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", jr.PartieId);
                cmd.Parameters.AddWithValue("@joueurId", jr.JoueurId);
                cmd.Parameters.AddWithValue("@ressourceId", jr.RessourceId);
                cmd.Parameters.AddWithValue("@qty", (object?)jr.Qty ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[JoueurRessourceRepository.Create] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[JoueurRessourceRepository.Create] General error: {ex.Message}");
                throw;
            }
        }

        public void Update(JoueurRessource jr)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = @"UPDATE joueurs_ressources SET partie_id=@partieId, joueur_id=@joueurId, ressource_id=@ressourceId, qty=@qty WHERE jr_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", jr.JrId);
                cmd.Parameters.AddWithValue("@partieId", jr.PartieId);
                cmd.Parameters.AddWithValue("@joueurId", jr.JoueurId);
                cmd.Parameters.AddWithValue("@ressourceId", jr.RessourceId);
                cmd.Parameters.AddWithValue("@qty", (object?)jr.Qty ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[JoueurRessourceRepository.Update] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[JoueurRessourceRepository.Update] General error: {ex.Message}");
                throw;
            }
        }

        public void Delete(int jrId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "DELETE FROM joueurs_ressources WHERE jr_id = @id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", jrId);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[JoueurRessourceRepository.Delete] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[JoueurRessourceRepository.Delete] General error: {ex.Message}");
                throw;
            }
        }
    }
}
