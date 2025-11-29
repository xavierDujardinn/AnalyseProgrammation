using System.Data.SQLite;
using DataAccess.Common;
using DataAccess.interface_jeu;

namespace DataAccess.repository_jeu
{
    public class PopulationRepository : IPopulationRepository
    {
        private readonly SQLiteConnection? _testConnection;

        // Constructeur production
        public PopulationRepository() { }

        // Constructeur test : injection de connexion
        public PopulationRepository(SQLiteConnection testConn)
        {
            _testConnection = testConn;
        }

        public Population? GetById(int populationId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT population_id, partie_id, joueur_id, total, moral, besoins, specialisation FROM populations WHERE population_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", populationId);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new Population
                {
                    PopulationId = reader.GetInt32(0),
                    PartieId = reader.GetInt32(1),
                    JoueurId = reader.GetInt32(2),
                    Total = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                    Moral = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                    Besoins = reader.IsDBNull(5) ? null : reader.GetString(5),
                    Specialisation = reader.IsDBNull(6) ? null : reader.GetString(6)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[PopulationRepository.GetById] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[PopulationRepository.GetById] General error: {ex.Message}");
                throw;
            }
        }

        public Population? GetByPartieAndJoueur(int partieId, int joueurId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT population_id, partie_id, joueur_id, total, moral, besoins, specialisation FROM populations WHERE partie_id=@partieId AND joueur_id=@joueurId";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", partieId);
                cmd.Parameters.AddWithValue("@joueurId", joueurId);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new Population
                {
                    PopulationId = reader.GetInt32(0),
                    PartieId = reader.GetInt32(1),
                    JoueurId = reader.GetInt32(2),
                    Total = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                    Moral = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                    Besoins = reader.IsDBNull(5) ? null : reader.GetString(5),
                    Specialisation = reader.IsDBNull(6) ? null : reader.GetString(6)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[PopulationRepository.GetByPartieAndJoueur] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[PopulationRepository.GetByPartieAndJoueur] General error: {ex.Message}");
                throw;
            }
        }

        public List<Population> GetAllByPartie(int partieId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT population_id, partie_id, joueur_id, total, moral, besoins, specialisation FROM populations WHERE partie_id=@partieId";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", partieId);
                using var reader = cmd.ExecuteReader();
                var list = new List<Population>();
                while (reader.Read())
                {
                    list.Add(new Population
                    {
                        PopulationId = reader.GetInt32(0),
                        PartieId = reader.GetInt32(1),
                        JoueurId = reader.GetInt32(2),
                        Total = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                        Moral = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                        Besoins = reader.IsDBNull(5) ? null : reader.GetString(5),
                        Specialisation = reader.IsDBNull(6) ? null : reader.GetString(6)
                    });
                }
                return list;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[PopulationRepository.GetAllByPartie] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[PopulationRepository.GetAllByPartie] General error: {ex.Message}");
                throw;
            }
        }

        public void Create(Population population)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "INSERT INTO populations (partie_id, joueur_id, total, moral, besoins, specialisation) VALUES (@partieId, @joueurId, @total, @moral, @besoins, @specialisation)";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", population.PartieId);
                cmd.Parameters.AddWithValue("@joueurId", population.JoueurId);
                cmd.Parameters.AddWithValue("@total", (object?)population.Total ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@moral", (object?)population.Moral ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@besoins", (object?)population.Besoins ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@specialisation", (object?)population.Specialisation ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[PopulationRepository.Create] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[PopulationRepository.Create] General error: {ex.Message}");
                throw;
            }
        }

        public void Update(Population population)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = @"UPDATE populations SET partie_id=@partieId, joueur_id=@joueurId, total=@total, moral=@moral, besoins=@besoins, specialisation=@specialisation WHERE population_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", population.PopulationId);
                cmd.Parameters.AddWithValue("@partieId", population.PartieId);
                cmd.Parameters.AddWithValue("@joueurId", population.JoueurId);
                cmd.Parameters.AddWithValue("@total", (object?)population.Total ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@moral", (object?)population.Moral ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@besoins", (object?)population.Besoins ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@specialisation", (object?)population.Specialisation ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[PopulationRepository.Update] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[PopulationRepository.Update] General error: {ex.Message}");
                throw;
            }
        }

        public void Delete(int populationId)
        {
            try
            {
            var conn = SQLjeu.GetConnection(_testConnection);
            const string sql = "DELETE FROM populations WHERE population_id=@id";
            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", populationId);
            cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[PopulationRepository.Delete] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[PopulationRepository.Delete] General error: {ex.Message}");
                throw;
            }
        }
    }
}
