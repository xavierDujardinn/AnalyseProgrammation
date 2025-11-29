using game.Models;

namespace game.DataAccess
{
    internal interface IActionLogRepository
    {
        ActionLog? GetById(int logId);
        List<ActionLog> GetAllByPartie(int partieId);
        List<ActionLog> GetAllByJoueur(int joueurId, int partieId);
        void Create(ActionLog log);
        void Delete(int logId);
    }
}