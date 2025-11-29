using game.Models;

namespace game.DataAccess
{
    internal interface IEvenementRefRepository
    {
        EvenementRef? GetById(int eventId);
        EvenementRef? GetByCode(string code);
        List<EvenementRef> GetAll();
        void Create(EvenementRef evenementRef);
        void Update(EvenementRef evenementRef);
        void Delete(int eventId);
    }
}