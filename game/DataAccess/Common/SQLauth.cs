using System.Data.SQLite;

namespace game.DataAccess
{
    internal static class SQLauth
    {
        public static SQLiteConnection GetConnection(SQLiteConnection? testConnection)
        {
            return testConnection ?? AuthDBConnection.GetInstance().Connection;
        }
    }
}