using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.GamePackage
{
    internal class Round
    {
        private static int nbRounds = 1;
        private int number;
        public Round()
        {
            number = nbRounds;
            nbRounds++;
        }

        public int GetNumber() { return number; }

        public static void SetNbRounds(int number)
        {
            nbRounds = number;
        }
    }
}
