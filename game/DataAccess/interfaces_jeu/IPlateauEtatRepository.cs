namespace DataAccess.interface_jeu
{
    public interface IPlateauEtatRepository
    {
        PlateauEtat? GetById(int plateauId);
        List<PlateauEtat> GetAllByPartie(int partieId);
        PlateauEtat? GetByPartieAndTour(int partieId, int tour);
        void Create(PlateauEtat etat);
        void Update(PlateauEtat etat);
        void Delete(int plateauId);
    }
}