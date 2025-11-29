using game.Models;

namespace game.DataAccess
{
    internal interface IBatimentRefRepository
    {
        BatimentRef? GetById(int batimentId);
        BatimentRef? GetByCode(string code);
        List<BatimentRef> GetAll();
        void Create(BatimentRef batRef);
        void Update(BatimentRef batRef);
        void Delete(int batimentId);
    }
}