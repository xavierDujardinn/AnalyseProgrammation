namespace DataAccess.interface_jeu
{
    public interface IPopulationRepository
    {
        Population? GetById(int populationId);
        Population? GetByPartieAndJoueur(int partieId, int joueurId);
        List<Population> GetAllByPartie(int partieId);
        void Create(Population population);
        void Update(Population population);
        void Delete(int populationId);
    }
}