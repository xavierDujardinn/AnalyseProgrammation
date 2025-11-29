using System.Data.SQLite;

namespace DataAccess.Common
{
    public static class SQLauth
    {
        public static SQLiteConnection GetConnection(SQLiteConnection? testConnection)
        {
            return testConnection ?? AuthDBConnection.GetInstance().Connection;
        }
    }
}