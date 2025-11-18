using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using game.GamePackage;

namespace game.Controller
{
    internal class GameController
    {
        private List<Round> rounds;
        private int currentRoundNumber;
        public bool Playing { get; set; } = false;

        public event Action<int>? OnNewRoundStarted;

        public GameController()
        {
            rounds = new List<Round>();
        }
        public void StartGame()
        {
            Console.WriteLine("The game has started !");
            Playing = true;
            RunGame();
        }

        public void RunGame()
        {
            while (Playing)
            {
                Round newRound = new Round();
                currentRoundNumber = newRound.GetNumber();
                rounds.Add(newRound);

                OnNewRoundStarted?.Invoke(currentRoundNumber);

                if (currentRoundNumber >= 10)
                {
                    EndGame();
                }
            }
        }

        public void EndGame()
        {
            Playing = false;
            rounds.Clear();
            Round.SetNbRounds(0);
        }
    }
}
