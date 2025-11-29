namespace DataAccess.interface_jeu
{
    public interface IJoueurRechercheRepository
    {
        JoueurRecherche? Get(int jrId);
        List<JoueurRecherche> GetAllByJoueur(int joueurId, int partieId);
        void Create(JoueurRecherche jr);
        void Update(JoueurRecherche jr);
        void Delete(int jrId);
    }
}