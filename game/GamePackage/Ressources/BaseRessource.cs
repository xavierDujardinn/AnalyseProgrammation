using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.GamePackage.Ressources
{
    internal abstract class BaseRessource
    {
        public string Name { get; private set; } = "Unknown ressource";
        protected BaseRessource(string name) 
        {
            Name = name;
        }
    }
}
