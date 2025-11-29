namespace DataAccess.interface_jeu
{
    public interface IExplorationRepository
    {
        Exploration? GetById(int explorationId);
        List<Exploration> GetAllByJoueur(int joueurId, int partieId);
        List<Exploration> GetAllByZone(int zoneId, int partieId);
        void Create(Exploration exploration);
        void Update(Exploration exploration);
        void Delete(int explorationId);
    }
}