namespace DataAccess.interface_jeu
{
    public interface IParametreGlobalRepository
    {
        ParametreGlobal? GetById(int paramId);
        ParametreGlobal? GetByPartie(int partieId);
        void Create(ParametreGlobal param);
        void Update(ParametreGlobal param);
        void Delete(int paramId);
    }
}