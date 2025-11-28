using game.GamePackage.Ressources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.GamePackage.Buildings
{
    internal class IronMine : BaseBuilding
    {
        public IronMine() : base("Batiment", '£', new Iron())
        {
            RessourceQuantity = 15;
        }
    }
}
