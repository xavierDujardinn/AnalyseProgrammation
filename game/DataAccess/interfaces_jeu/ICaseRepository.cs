namespace DataAccess.interface_jeu
{
    public interface ICaseRepository
    {
        Case? GetById(int caseId);
        List<Case> GetAllByPartie(int partieId);
        Case? GetByCoord(int partieId, int x, int y);
        void Create(Case c);
        void Update(Case c);
        void Delete(int caseId);
    }
}