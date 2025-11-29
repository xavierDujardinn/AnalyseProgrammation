using game.Models;

namespace game.DataAccess
{
    internal interface IJoueurRepository
    {
        Joueur? GetById(int joueurId);
        List<Joueur> GetAllByPartie(int partieId);
        List<Joueur> GetAll();
        void Create(Joueur joueur);
        void Update(Joueur joueur);
        void Delete(int joueurId);
        Joueur? GetByPseudo(int partieId, string pseudo);
        Joueur? GetByUserId(int partieId, int userId);
    }
}