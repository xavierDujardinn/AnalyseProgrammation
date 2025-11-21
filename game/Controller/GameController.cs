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
        public int CurrentRoundNumber { get; private set; }
        public Tab GameTab { get; private set; }

        public bool Playing { get; set; } = false;

        public event Action OnNewRoundStarted;

        // J'ignore le warning pour le moment
        public GameController()
        {
            rounds = new List<Round>();
        }

        public void CreateGame()
        {
            GameTab = new Tab(20);
            StartGame();
        }
        public void StartGame()
        {
            Playing = true;
            RunGame();
        }

        public void RunGame()
        {
            while (Playing)
            {
                Round newRound = new Round();
                CurrentRoundNumber = newRound.GetNumber();
                rounds.Add(newRound);

                OnNewRoundStarted?.Invoke();

                if (CurrentRoundNumber >= 10)
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
