using System.Data.SQLite;

namespace game.DataAccess
{
    internal static class SQLjeu
    {
        public static SQLiteConnection GetConnection(SQLiteConnection?testConnection)
        {
            return testConnection ?? JeuDBConnection.GetInstance().Connection;
        }
    }
}