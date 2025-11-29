using System;
using System.Data.SQLite;

public sealed class AuthDBConnection : IDisposable
{
    private static AuthDBConnection? _instance;
    private static readonly object _lock = new();
    private SQLiteConnection? _connection;
    private bool _disposed;

    private AuthDBConnection()
    {
        try
        {
            _connection = new SQLiteConnection("Data Source=database/auth.db;Version=3;");
            _connection.Open();
        }
        catch (SQLiteException ex)
        {
            Console.WriteLine($"Impossible d'ouvrir la DB auth: {ex.Message}");
            throw;
        }
    }
    public static AuthDBConnection GetInstance()
    {
        lock (_lock)
        {
            if (_instance == null)
            {
                try
                {
                    _instance = new AuthDBConnection();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[AuthDBConnection] Impossible de créer l'instance : {ex.Message}");
                    throw;
                }
            }
            return _instance;
        }
    }

    public SQLiteConnection Connection => _connection!;

    public void Close()
    {
        if (_disposed) return;
        try
        {
            _connection?.Close();
            _connection?.Dispose();
        }
        catch (SQLiteException ex)
        {
            Console.WriteLine($"[AuthDBConnection] Erreur fermeture DB: {ex.Message}");
        }
        finally
        {
            _connection = null;
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Close();
    }
}