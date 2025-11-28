using game.GamePackage.Ressources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.GamePackage.Buildings
{
    internal class GoldMine : BaseBuilding
    {
        public GoldMine() : base("Batiment", '$', new Gold())
        {
            RessourceQuantity = 5;
        }
    }
}
