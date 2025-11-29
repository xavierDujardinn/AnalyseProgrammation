using game.Models;

namespace game.DataAccess
{
    internal interface IJoueurRechercheRepository
    {
        JoueurRecherche? Get(int jrId);
        List<JoueurRecherche> GetAllByJoueur(int joueurId, int partieId);
        void Create(JoueurRecherche jr);
        void Update(JoueurRecherche jr);
        void Delete(int jrId);
    }
}