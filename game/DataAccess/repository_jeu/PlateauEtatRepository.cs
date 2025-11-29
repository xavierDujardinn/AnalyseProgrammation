using System.Data.SQLite;
using DataAccess.Common;
using DataAccess.interface_jeu;

namespace DataAccess.repository_jeu
{
    public class PlateauEtatRepository : IPlateauEtatRepository
    {
        private readonly SQLiteConnection? _testConnection;

        // Constructeur production
        public PlateauEtatRepository() { }

        // Constructeur test : injection de connexion
        public PlateauEtatRepository(SQLiteConnection testConn)
        {
            _testConnection = testConn;
        }

        public PlateauEtat? GetById(int plateauId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT plateau_id, partie_id, tour, ascii_state, checksum, created_at FROM plateau_etats WHERE plateau_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", plateauId);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new PlateauEtat
                {
                    PlateauId = reader.GetInt32(0),
                    PartieId = reader.GetInt32(1),
                    Tour = reader.GetInt32(2),
                    AsciiState = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Checksum = reader.IsDBNull(4) ? null : reader.GetString(4),
                    CreatedAt = reader.IsDBNull(5) ? null : reader.GetString(5)
                } : null;
            }
            catch(SQLiteException ex)
            {
                Console.Error.WriteLine($"[PlateauEtatRepository.GetById] SQLite error: {ex.Message}");
                throw;
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine($"[PlateauEtatRepository.GetById] General error: {ex.Message}");
                throw;
            }
        }

        public List<PlateauEtat> GetAllByPartie(int partieId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT plateau_id, partie_id, tour, ascii_state, checksum, created_at FROM plateau_etats WHERE partie_id=@partieId";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", partieId);
                using var reader = cmd.ExecuteReader();
                var list = new List<PlateauEtat>();
                while (reader.Read())
                {
                    list.Add(new PlateauEtat
                    {
                        PlateauId = reader.GetInt32(0),
                        PartieId = reader.GetInt32(1),
                        Tour = reader.GetInt32(2),
                        AsciiState = reader.IsDBNull(3) ? null : reader.GetString(3),
                        Checksum = reader.IsDBNull(4) ? null : reader.GetString(4),
                        CreatedAt = reader.IsDBNull(5) ? null : reader.GetString(5)
                    });
                }
                return list;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[PlateauEtatRepository.GetAllByPartie] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[PlateauEtatRepository.GetAllByPartie] General error: {ex.Message}");
                throw;
            }
        }

        public PlateauEtat? GetByPartieAndTour(int partieId, int tour)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT plateau_id, partie_id, tour, ascii_state, checksum, created_at FROM plateau_etats WHERE partie_id=@partieId AND tour=@tour";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", partieId);
                cmd.Parameters.AddWithValue("@tour", tour);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new PlateauEtat
                {
                    PlateauId = reader.GetInt32(0),
                    PartieId = reader.GetInt32(1),
                    Tour = reader.GetInt32(2),
                    AsciiState = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Checksum = reader.IsDBNull(4) ? null : reader.GetString(4),
                    CreatedAt = reader.IsDBNull(5) ? null : reader.GetString(5)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[PlateauEtatRepository.GetByPartieAndTour] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[PlateauEtatRepository.GetByPartieAndTour] General error: {ex.Message}");
                throw;
            }
        }

        public void Create(PlateauEtat etat)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "INSERT INTO plateau_etats (partie_id, tour, ascii_state, checksum, created_at) VALUES (@partieId, @tour, @asciiState, @checksum, @createdAt)";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", etat.PartieId);
                cmd.Parameters.AddWithValue("@tour", etat.Tour);
                cmd.Parameters.AddWithValue("@asciiState", (object?)etat.AsciiState ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@checksum", (object?)etat.Checksum ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@createdAt", (object?)etat.CreatedAt ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[PlateauEtatRepository.Create] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[PlateauEtatRepository.Create] General error: {ex.Message}");
                throw;
            }
        }

        public void Update(PlateauEtat etat)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = @"UPDATE plateau_etats SET partie_id=@partieId, tour=@tour, ascii_state=@asciiState, checksum=@checksum, created_at=@createdAt WHERE plateau_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", etat.PlateauId);
                cmd.Parameters.AddWithValue("@partieId", etat.PartieId);
                cmd.Parameters.AddWithValue("@tour", etat.Tour);
                cmd.Parameters.AddWithValue("@asciiState", (object?)etat.AsciiState ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@checksum", (object?)etat.Checksum ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@createdAt", (object?)etat.CreatedAt ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[PlateauEtatRepository.Update] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[PlateauEtatRepository.Update] General error: {ex.Message}");
                throw;
            }
        }

        public void Delete(int plateauId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "DELETE FROM plateau_etats WHERE plateau_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", plateauId);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[PlateauEtatRepository.Delete] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[PlateauEtatRepository.Delete] General error: {ex.Message}");
                throw;
            }
        }
    }
}
