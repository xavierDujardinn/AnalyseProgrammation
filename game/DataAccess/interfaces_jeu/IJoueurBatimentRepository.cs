namespace DataAccess.interface_jeu
{
    public interface IJoueurBatimentRepository
    {
        JoueurBatiment? Get(int jbId);
        List <JoueurBatiment> GetAllByJoueur(int joueurId, int partieId);
        void Create (JoueurBatiment jb);
        void Update (JoueurBatiment jb);
        void Delete (int jbId);
    }
}