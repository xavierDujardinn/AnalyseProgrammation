using System.Data.SQLite;
using DataAccess.Common;
using DataAccess.interface_jeu;

namespace DataAccess.repository_jeu
{
    public class ParametreGlobalRepository : IParametreGlobalRepository
    {
        private readonly SQLiteConnection? _testConnection;

        // Constructeur production
        public ParametreGlobalRepository() { }

        // Constructeur test : injection de connexion
        public ParametreGlobalRepository(SQLiteConnection testConn)
        {
            _testConnection = testConn;
        }

        public ParametreGlobal? GetById(int paramId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT param_id, partie_id, map_width, map_height, world_seed, max_turns, starting_ressources, max_joueurs FROM parametres_global WHERE param_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", paramId);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new ParametreGlobal
                {
                    ParamId = reader.GetInt32(0),
                    PartieId = reader.GetInt32(1),
                    MapWidth = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                    MapHeight = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                    WorldSeed = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                    MaxTurns = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                    StartingRessources = reader.IsDBNull(6) ? null : reader.GetString(6),
                    MaxJoueurs = reader.GetInt32(7)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[ParametreGlobalRepository.GetById] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[ParametreGlobalRepository.GetById] General error: {ex.Message}");
                throw;
            }
        }

        public ParametreGlobal? GetByPartie(int partieId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "SELECT param_id, partie_id, map_width, map_height, world_seed, max_turns, starting_ressources, max_joueurs FROM parametres_global WHERE partie_id=@partieId";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", partieId);
                using var reader = cmd.ExecuteReader();
                return reader.Read() ? new ParametreGlobal
                {
                    ParamId = reader.GetInt32(0),
                    PartieId = reader.GetInt32(1),
                    MapWidth = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                    MapHeight = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                    WorldSeed = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                    MaxTurns = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                    StartingRessources = reader.IsDBNull(6) ? null : reader.GetString(6),
                    MaxJoueurs = reader.GetInt32(7)
                } : null;
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[ParametreGlobalRepository.GetByPartie] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[ParametreGlobalRepository.GetByPartie] General error: {ex.Message}");
                throw;
            }
        }

        public void Create(ParametreGlobal param)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "INSERT INTO parametres_global (partie_id, map_width, map_height, world_seed, max_turns, starting_ressources, max_joueurs) VALUES (@partieId, @mapWidth, @mapHeight, @worldSeed, @maxTurns, @startingRessources, @maxJoueurs)";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partieId", param.PartieId);
                cmd.Parameters.AddWithValue("@mapWidth", (object?)param.MapWidth ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@mapHeight", (object?)param.MapHeight ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@worldSeed", (object?)param.WorldSeed ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@maxTurns", (object?)param.MaxTurns ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@startingRessources", (object?)param.StartingRessources ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@maxJoueurs", param.MaxJoueurs);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[ParametreGlobalRepository.Create] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[ParametreGlobalRepository.Create] General error: {ex.Message}");
                throw;
            }
        }

        public void Update(ParametreGlobal param)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = @"UPDATE parametres_global SET partie_id=@partieId, map_width=@mapWidth, map_height=@mapHeight, world_seed=@worldSeed, max_turns=@maxTurns, starting_ressources=@startingRessources, max_joueurs=@maxJoueurs WHERE param_id=@id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", param.ParamId);
                cmd.Parameters.AddWithValue("@partieId", param.PartieId);
                cmd.Parameters.AddWithValue("@mapWidth", (object?)param.MapWidth ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@mapHeight", (object?)param.MapHeight ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@worldSeed", (object?)param.WorldSeed ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@maxTurns", (object?)param.MaxTurns ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@startingRessources", (object?)param.StartingRessources ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@maxJoueurs", param.MaxJoueurs);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[ParametreGlobalRepository.Update] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[ParametreGlobalRepository.Update] General error: {ex.Message}");
                throw;
            }
        }

        public void Delete(int paramId)
        {
            try
            {
                var conn = SQLjeu.GetConnection(_testConnection);
                const string sql = "DELETE FROM parametres_global WHERE param_id = @id";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", paramId);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.Error.WriteLine($"[ParametreGlobalRepository.Delete] SQLite error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[ParametreGlobalRepository.Delete] General error: {ex.Message}");
                throw;
            }
        }
    }
}
