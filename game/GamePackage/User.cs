using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.GamePackage
{
    internal class User
    {
        // Default value for debugging is "Unknown"
        public string Name { get; set; } = "Unknown";

        public User(string name)
        {
            Name = name;
        }

        public override string ToString() 
        {
            return Name;
        }
    }
}
