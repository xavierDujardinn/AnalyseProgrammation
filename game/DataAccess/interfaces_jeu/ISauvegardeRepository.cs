namespace DataAccess.interface_jeu
{
    public interface ISauvegardeRepository
    {
        Sauvegarde? GetById(int sauvegardeId);
        List<Sauvegarde> GetAllByPartie(int partieId);
        Sauvegarde? GetBySlot(int partieId, int slot);
        void Create(Sauvegarde sauvegarde);
        void Update(Sauvegarde sauvegarde);
        void Delete(int sauvegardeId);
    }
}