using game.Models;

namespace game.DataAccess
{
    internal interface IExplorationRepository
    {
        Exploration? GetById(int explorationId);
        List<Exploration> GetAllByJoueur(int joueurId, int partieId);
        List<Exploration> GetAllByZone(int zoneId, int partieId);
        void Create(Exploration exploration);
        void Update(Exploration exploration);
        void Delete(int explorationId);
    }
}