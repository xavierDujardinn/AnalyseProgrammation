using System.Data.SQLite;
using game.Models;

namespace game.DataAccess
{
    internal class JoueurRepository : IJoueurRepository
    {
        private readonly SQLiteConnection? _testConnection;

        public JoueurRepository() { }

        public JoueurRepository(SQLiteConnection testConn)
        {
            _testConnection = testConn;
        }

        public Joueur? GetById(int joueurId)
        {
            try {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT joueur_id, partie_id, pseudo, couleur, ordre_tour, actif, created_at FROM joueurs WHERE joueur_id = @id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", joueurId);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new Joueur
                {
                    JoueurId = reader.GetInt32(0),
                    PartieId = reader.GetInt32(1),
                    Pseudo = reader.IsDBNull(2) ? null : reader.GetString(2),
                    couleur = reader.IsDBNull(3) ? null : reader.GetString(3),
                    OrdreTour = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                    Actif = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                    CreatedAt = reader.IsDBNull(6) ? null : reader.GetString(6)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[JoueurRepository.GetById] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[JoueurRepository.GetById] General error: {ex.Message}");
                throw;
            }
        }

        public List<Joueur> GetAllByPartie(int partieId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT joueur_id, partie_id, pseudo, couleur, ordre_tour, actif, created_at FROM joueurs WHERE partie_id = @partieId";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", partieId);
                using var reader = cmd.ExecuteReader();
                var list = new List<Joueur>();
                while (reader.Read())
                {
                    list.Add(new Joueur
                    {
                        JoueurId = reader.GetInt32(0),
                        PartieId = reader.GetInt32(1),
                        Pseudo = reader.IsDBNull(2) ? null : reader.GetString(2),
                        couleur = reader.IsDBNull(3) ? null : reader.GetString(3),
                        OrdreTour = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                        Actif = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                        CreatedAt = reader.IsDBNull(6) ? null : reader.GetString(6)
                    });
                }
                return list;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[JoueurRepository.GetAllByPartie] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[JoueurRepository.GetAllByPartie] General error: {ex.Message}");
                throw;
            }
        }

        public List<Joueur> GetAll()
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT joueur_id, partie_id, pseudo, couleur, ordre_tour, actif, created_at FROM joueurs";
                using var cmd = new SQLiteCommand(sql, conn);
                using var reader = cmd.ExecuteReader();
                var list = new List<Joueur>();
                while (reader.Read())
                {
                    list.Add(new Joueur
                    {
                        JoueurId = reader.GetInt32(0),
                        PartieId = reader.GetInt32(1),
                        Pseudo = reader.IsDBNull(2) ? null : reader.GetString(2),
                        couleur = reader.IsDBNull(3) ? null : reader.GetString(3),
                        OrdreTour = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                        Actif = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                        CreatedAt = reader.IsDBNull(6) ? null : reader.GetString(6)
                    });
                }
                return list;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[JoueurRepository.GetAll] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[JoueurRepository.GetAll] General error: {ex.Message}");
                throw;
            }
        }

        public void Create(Joueur joueur)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "INSERT INTO joueurs (partie_id, pseudo, couleur, ordre_tour, actif, created_at) VALUES (@partieId, @pseudo, @couleur, @ordreTour, @actif, @createdAt)";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", joueur.PartieId);
                cmd.Parameters.AddWithValue("@pseudo", (object?)joueur.Pseudo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@couleur", (object?)joueur.couleur ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ordreTour", (object?)joueur.OrdreTour ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@actif", (object?)joueur.Actif ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@createdAt", (object?)joueur.CreatedAt ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[JoueurRepository.Create] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[JoueurRepository.Create] General error: {ex.Message}");
                throw;
            }
        }

        public void Update(Joueur joueur)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = @"UPDATE joueurs SET partie_id=@partieId, pseudo=@pseudo, couleur=@couleur, ordre_tour=@ordreTour, actif=@actif, created_at=@createdAt WHERE joueur_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", joueur.JoueurId);
                cmd.Parameters.AddWithValue("@partieId", joueur.PartieId);
                cmd.Parameters.AddWithValue("@pseudo", (object?)joueur.Pseudo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@couleur", (object?)joueur.couleur ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ordreTour", (object?)joueur.OrdreTour ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@actif", (object?)joueur.Actif ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@createdAt", (object?)joueur.CreatedAt ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[JoueurRepository.Update] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[JoueurRepository.Update] General error: {ex.Message}");
                throw;
            }
        }

        public void Delete(int joueurId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "DELETE FROM joueurs WHERE joueur_id = @id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", joueurId);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[JoueurRepository.Delete] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[JoueurRepository.Delete] General error: {ex.Message}");
                throw;
            }
        }

        public Joueur? GetByPseudo(int partieId, string pseudo)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT joueur_id, partie_id, pseudo, couleur, ordre_tour, actif, created_at FROM joueurs WHERE partie_id=@partieId AND pseudo=@pseudo";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", partieId);
                cmd.Parameters.AddWithValue("@pseudo", pseudo);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new Joueur
                {
                    JoueurId = reader.GetInt32(0),
                    PartieId = reader.GetInt32(1),
                    Pseudo = reader.IsDBNull(2) ? null : reader.GetString(2),
                    couleur = reader.IsDBNull(3) ? null : reader.GetString(3),
                    OrdreTour = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                    Actif = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                    CreatedAt = reader.IsDBNull(6) ? null : reader.GetString(6)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[JoueurRepository.GetByPseudo] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[JoueurRepository.GetByPseudo] General error: {ex.Message}");
                throw;
            }
        }

        public Joueur? GetByUserId(int partieId, int userId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT joueur_id, partie_id, pseudo, couleur, ordre_tour, actif, created_at FROM joueurs WHERE partie_id=@partieId AND joueur_id=@userId";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", partieId);
                cmd.Parameters.AddWithValue("@userId", userId);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new Joueur
                {
                    JoueurId = reader.GetInt32(0),
                    PartieId = reader.GetInt32(1),
                    Pseudo = reader.IsDBNull(2) ? null : reader.GetString(2),
                    couleur = reader.IsDBNull(3) ? null : reader.GetString(3),
                    OrdreTour = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                    Actif = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                    CreatedAt = reader.IsDBNull(6) ? null : reader.GetString(6)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[JoueurRepository.GetByUserId] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[JoueurRepository.GetByUserId] General error: {ex.Message}");
                throw;
            }
        }
    }
}
