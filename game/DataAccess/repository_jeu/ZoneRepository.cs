using System.Data.SQLite;
using DataAccess.Common;
using DataAccess.interface_jeu;

namespace DataAccess.repository_jeu
{
    public class ZoneRepository : IZoneRepository
    {
        private readonly SQLiteConnection? _testConnection;

        // Constructeur production
        public ZoneRepository() { }

        // Constructeur test : injection de connexion
        public ZoneRepository(SQLiteConnection testConn)
        {
            _testConnection = testConn;
        }

        public Zone? GetById(int zoneId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT zone_id, partie_id, coord, type, danger, decouvert_par FROM zones WHERE zone_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", zoneId);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new Zone
                {
                    ZoneId = reader.GetInt32(0),
                    PartieId = reader.GetInt32(1),
                    Coord = reader.GetString(2),
                    Type = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Danger = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                    DecouvertPar = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[ZoneRepository.GetById] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[ZoneRepository.GetById] General error: {ex.Message}");
                throw;
            }
        }

        public Zone? GetByCoord(int partieId, string coord)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT zone_id, partie_id, coord, type, danger, decouvert_par FROM zones WHERE partie_id=@partieId AND coord=@coord";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", partieId);
                cmd.Parameters.AddWithValue("@coord", coord);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new Zone
                {
                    ZoneId = reader.GetInt32(0),
                    PartieId = reader.GetInt32(1),
                    Coord = reader.GetString(2),
                    Type = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Danger = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                    DecouvertPar = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[ZoneRepository.GetByCoord] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[ZoneRepository.GetByCoord] General error: {ex.Message}");
                throw;
            }
        }

        public List<Zone> GetAllByPartie(int partieId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT zone_id, partie_id, coord, type, danger, decouvert_par FROM zones WHERE partie_id=@partieId";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", partieId);
                using var reader = cmd.ExecuteReader();
                var list = new List<Zone>();
                while (reader.Read())
                {
                    list.Add(new Zone
                    {
                        ZoneId = reader.GetInt32(0),
                        PartieId = reader.GetInt32(1),
                        Coord = reader.GetString(2),
                        Type = reader.IsDBNull(3) ? null : reader.GetString(3),
                        Danger = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                        DecouvertPar = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5)
                    });
                }
                return list;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[ZoneRepository.GetAllByPartie] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[ZoneRepository.GetAllByPartie] General error: {ex.Message}");
                throw;
            }
        }

        public void Create(Zone zone)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "INSERT INTO zones (partie_id, coord, type, danger, decouvert_par) VALUES (@partieId, @coord, @type, @danger, @decouvertPar)";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", zone.PartieId);
                cmd.Parameters.AddWithValue("@coord", zone.Coord);
                cmd.Parameters.AddWithValue("@type", (object?)zone.Type ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@danger", (object?)zone.Danger ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@decouvertPar", (object?)zone.DecouvertPar ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[ZoneRepository.Create] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[ZoneRepository.Create] General error: {ex.Message}");
                throw;
            }
        }

        public void Update(Zone zone)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = @"UPDATE zones SET partie_id=@partieId, coord=@coord, type=@type, danger=@danger, decouvert_par=@decouvertPar WHERE zone_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", zone.ZoneId);
                cmd.Parameters.AddWithValue("@partieId", zone.PartieId);
                cmd.Parameters.AddWithValue("@coord", zone.Coord);
                cmd.Parameters.AddWithValue("@type", (object?)zone.Type ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@danger", (object?)zone.Danger ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@decouvertPar", (object?)zone.DecouvertPar ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[ZoneRepository.Update] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[ZoneRepository.Update] General error: {ex.Message}");
                throw;
            }
        }

        public void Delete(int zoneId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "DELETE FROM zones WHERE zone_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", zoneId);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[ZoneRepository.Delete] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[ZoneRepository.Delete] General error: {ex.Message}");
                throw;
            }
        }
    }
}
