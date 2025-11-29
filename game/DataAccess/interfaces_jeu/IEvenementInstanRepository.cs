using game.Models;

namespace game.DataAccess
{
    internal interface IEvenementInstanRepository
    {
        EvenementInstan? GetById(int eveninstId);
        List<EvenementInstan> GetAllByPartie(int partieId);
        List<EvenementInstan> GetAllByTour(int partieId, int tour);
        void Create(EvenementInstan evenementInstan);
        void Update(EvenementInstan evenementInstan);
        void Delete(int eveninstId);
    }
}