namespace DataAccess.interface_jeu
{
    public interface IJoueurRessourceRepository
    {
        JoueurRessource? Get(int partieId, int joueurId, int ressourceid);
        List<JoueurRessource> GetAllbyJoueur(int joueurId, int partieId);
        void Create(JoueurRessource jr);
        void Update(JoueurRessource jr);
        void Delete(int jrid);
    }
}