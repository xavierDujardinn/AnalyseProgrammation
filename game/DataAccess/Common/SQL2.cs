using System.Data.SQLite;

namespace DataAccess.Common
{
    public static class SQLjeu
    {
        public static SQLiteConnection GetConnection(SQLiteConnection?testConnection)
        {
            return testConnection ?? JeuDBConnection.GetInstance().Connection;
        }
    }
}