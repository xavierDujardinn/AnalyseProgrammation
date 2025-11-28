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
        public List<User> Users;
        public int CurrentRoundNumber { get; private set; }
        public Tab GameTab { get; private set; }

        public bool Playing { get; set; } = false;

        public event Action OnNewRoundStarted;

        // J'ignore le warning pour le moment
        public GameController()
        {
            rounds = new List<Round>();
            Users = new List<User>();
        }

        #region base methods
        public void CreateGame(int tabSize)
        {
            GameTab = new Tab(tabSize);
            StartGame();
        }

        public void EndGame()
        {
            Playing = false;
            rounds.Clear();
            Round.SetNbRounds(0);
        }

        public void StartGame()
        {
            Playing = true;
            RunGame();
        }
        #endregion

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
    }
}
