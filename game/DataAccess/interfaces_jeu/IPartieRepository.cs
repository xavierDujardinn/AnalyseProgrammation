namespace DataAccess.interface_jeu
{
    public interface IPartieRepository
    {
        Partie? GetById(int partieId);
        List<Partie> GetAll();
        void Create(Partie partie);
        void Update(Partie partie);
        void Delete(int partieId);
        List<Partie> GetByName(string nom);
        List<Partie> GetByStatus(string statut);
    }
}