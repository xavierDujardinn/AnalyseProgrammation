namespace DataAccess.interface_jeu
{
    public interface IBatimentNiveauRepository
    {
        BatimentNiveau? GetById(int bnId);
        List<BatimentNiveau> GetAllByBatiment(int batimentId);
        BatimentNiveau? GetByBatimentAndNiveau(int batimentId, int niveau);
        void Create(BatimentNiveau niveau);
        void Update(BatimentNiveau niveau);
        void Delete(int bnId);
    }
}