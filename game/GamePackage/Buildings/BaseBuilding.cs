using game.GamePackage.Ressources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.GamePackage.Buildings
{
    internal abstract class BaseBuilding : CellContent
    {
        public string Name { get; protected set; }

        public BaseRessource Ressource { get; private set; }
        public int RessourceQuantity { get; protected set; } = 0;

        protected BaseBuilding(string name, char icon, BaseRessource ressource)
        {
            Name = name;
            Icon = icon;
            Ressource = ressource;
        }

        public override string ToString()
        {
            return Icon.ToString();
        }
    }
}
