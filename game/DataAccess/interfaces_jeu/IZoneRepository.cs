namespace DataAccess.interface_jeu
{
    public interface IZoneRepository
    {
        Zone? GetById(int zoneId);
        Zone? GetByCoord(int partieId, string coord);
        List<Zone> GetAllByPartie(int partieId);
        void Create(Zone zone);
        void Update(Zone zone);
        void Delete(int zoneId);
    }
}