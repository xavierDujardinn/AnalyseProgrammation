using System;
using System.Data.SQLite;

public sealed class JeuDBConnection : IDisposable
{
    private static JeuDBConnection? _instance;
    private static readonly object _lock = new();
    private SQLiteConnection? _connection;
    private bool _disposed;

    private JeuDBConnection()
    {
        try{
            _connection = new SQLiteConnection($"Data Source=database/jeu.db;Version=3;");
            _connection.Open();
        }
        catch (SQLiteException ex)
        {
            Console.WriteLine($"Impossible d'ouvrir la DB auth: {ex.Message}");
            throw;
        }
    }

    public static JeuDBConnection GetInstance()
    {
        lock (_lock)
        {
            if (_instance == null)
            {
                try
                {
                    _instance = new JeuDBConnection();
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine($"[JeuDBConnection] Impossible de créer l'instance : {ex.Message}");
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
            Console.WriteLine($"[JeuDBConnection] Erreur fermeture DB: {ex.Message}");
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