using Xunit;
using System.Data.SQLite;
using DataAccess.repository_jeu;

public class SauvegardeRepositoryTests
{
    private SQLiteConnection GetInMemoryConnection()
    {
        var conn = new SQLiteConnection("Data Source=:memory:");
        conn.Open();
        using var cmd = new SQLiteCommand(
            @"CREATE TABLE sauvegardes (
                sauvegarde_id INTEGER PRIMARY KEY AUTOINCREMENT,
                partie_id INTEGER NOT NULL,
                slot INTEGER NOT NULL,
                nom TEXT,
                tour INTEGER NOT NULL,
                snapshot TEXT NOT NULL,
                created_at TEXT,
                temps INTEGER
            );", conn);
        cmd.ExecuteNonQuery();
        return conn;
    }

    [Fact]
    public void Create_And_GetById_Works()
    {
        using var conn = GetInMemoryConnection();
        var repo = new SauvegardeRepository(conn);

        var sauvegarde = new Sauvegarde
        {
            PartieId = 1,
            Slot = 0,
            Nom = "testSave",
            Tour = 7,
            Snapshot = "some state",
            CreatedAt = "2025-11-25 14:54:00",
            Temps = 12
        };
        repo.Create(sauvegarde);

        // récupère la sauvegarde nouvellement insérée via rowid
        var cmd = new SQLiteCommand("SELECT last_insert_rowid()", conn);
        var lastId = (long)cmd.ExecuteScalar();

        var fetched = repo.GetById((int)lastId);
        Assert.NotNull(fetched);
        Assert.Equal(1, fetched.PartieId);
        Assert.Equal(0, fetched.Slot);
        Assert.Equal("testSave", fetched.Nom);
        Assert.Equal(7, fetched.Tour);
        Assert.Equal("some state", fetched.Snapshot);
        Assert.Equal("2025-11-25 14:54:00", fetched.CreatedAt);
        Assert.Equal(12, fetched.Temps);
    }

    [Fact]
    public void GetAllByPartie_ReturnsCorrectSaves()
    {
        using var conn = GetInMemoryConnection();
        var repo = new SauvegardeRepository(conn);

        repo.Create(new Sauvegarde { PartieId = 2, Slot = 1, Nom = "A", Tour = 3, Snapshot = "sA", CreatedAt = null, Temps = 5 });
        repo.Create(new Sauvegarde { PartieId = 2, Slot = 2, Nom = "B", Tour = 5, Snapshot = "sB", CreatedAt = null, Temps = 10 });
        repo.Create(new Sauvegarde { PartieId = 3, Slot = 1, Nom = "C", Tour = 7, Snapshot = "sC", CreatedAt = null, Temps = 3 });

        var saves = repo.GetAllByPartie(2);
        Assert.Equal(2, saves.Count);
        Assert.Contains(saves, s => s.Nom == "A");
        Assert.Contains(saves, s => s.Nom == "B");
    }

    [Fact]
    public void GetBySlot_ReturnsCorrectSave()
    {
        using var conn = GetInMemoryConnection();
        var repo = new SauvegardeRepository(conn);

        repo.Create(new Sauvegarde { PartieId = 2, Slot = 1, Nom = "SlotA", Tour = 3, Snapshot = "sA", CreatedAt = null, Temps = 5 });
        repo.Create(new Sauvegarde { PartieId = 2, Slot = 2, Nom = "SlotB", Tour = 5, Snapshot = "sB", CreatedAt = null, Temps = 10 });

        var save = repo.GetBySlot(2, 2);
        Assert.NotNull(save);
        Assert.Equal("SlotB", save.Nom);
        Assert.Equal(10, save.Temps);
    }

    [Fact]
    public void Update_ChangesValues()
    {
        using var conn = GetInMemoryConnection();
        var repo = new SauvegardeRepository(conn);

        var sauvegarde = new Sauvegarde
        {
            PartieId = 4, Slot = 1, Nom = "init", Tour = 1, Snapshot = "state", CreatedAt = null, Temps = 7
        };
        repo.Create(sauvegarde);

        // récupère l'id de la sauvegarde créée
        var cmd = new SQLiteCommand("SELECT last_insert_rowid()", conn);
        var id = (long)cmd.ExecuteScalar();

        var toUpdate = repo.GetById((int)id)!;
        toUpdate.Nom = "updated";
        toUpdate.Tour = 9;
        toUpdate.Snapshot = "newstate";
        toUpdate.Temps = 33;
        repo.Update(toUpdate);

        var updated = repo.GetById((int)id);
        Assert.NotNull(updated);
        Assert.Equal("updated", updated.Nom);
        Assert.Equal(9, updated.Tour);
        Assert.Equal("newstate", updated.Snapshot);
        Assert.Equal(33, updated.Temps);
    }

    [Fact]
    public void Delete_RemovesSave()
    {
        using var conn = GetInMemoryConnection();
        var repo = new SauvegardeRepository(conn);

        repo.Create(new Sauvegarde { PartieId = 5, Slot = 1, Nom = "toDelete", Tour = 3, Snapshot = "sd", CreatedAt = null, Temps = 2 });

        // récupère l'id de la sauvegarde créée
        var cmd = new SQLiteCommand("SELECT last_insert_rowid()", conn);
        var id = (long)cmd.ExecuteScalar();

        repo.Delete((int)id);
        var res = repo.GetById((int)id);
        Assert.Null(res);
    }
}
