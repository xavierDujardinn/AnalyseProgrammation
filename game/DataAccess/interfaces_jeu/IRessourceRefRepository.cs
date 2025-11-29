using game.Models;

namespace game.DataAccess
{
    internal interface IRessourceRefRepository
    {
        RessourceRef? GetById(int ressourceId);
        RessourceRef? GetByCode(string code);
        List<RessourceRef> GetAll();
        void Create(RessourceRef ressource);
        void Update(RessourceRef ressource);
        void Delete(int ressourceId);
    }
}