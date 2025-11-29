using game.Models;

namespace game.DataAccess
{
    internal interface IRechercheRefRepository
    {
        RechercheRef? GetById(int rechercheId);
        RechercheRef? GetByCode(string code);
        List<RechercheRef> GetAll();
        void Create(RechercheRef recherche);
        void Update(RechercheRef recherche);
        void Delete(int rechercheId);
    }
}