using game.Controller;
using game.GamePackage;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace game.View
{
    internal class GameConfigMenu
    {
        private MainMenu mainMenu;
        private GameMenu gameMenu;
        private GameController gameController;

        // Set default tabSize to default size
        private int tabSize = Tab.DEFAULT_SIZE; 

        public GameConfigMenu(MainMenu mainMenu, GameMenu gameMenu)
        {
            this.mainMenu = mainMenu;
            this.gameMenu = gameMenu;
            gameController = this.gameMenu.GameController;
        }
        
        public void ResetConfigValues()
        {
            // This function is called when the user goes back to the main menu,
            // don't forget to add the next config parameters.
            tabSize = Tab.DEFAULT_SIZE;
            gameController.Users.Clear();
        }
        public void AddPlayer()
        {
            int ctr = AnsiConsole.Prompt(new TextPrompt<int>("Combien de joueurs voulez vous ajouter ? :"));
            int cnt = 0;
            while (cnt <= ctr-1)
            {
                string playerName = AnsiConsole.Prompt(
                new TextPrompt<string>($"Ajouter un joueur, actuellement ({gameController.Users.Count}) :"));
                gameController.Users.Add(new User(playerName));
                AnsiConsole.MarkupLine(gameController.Users.Last().ToString());
                cnt ++;
            }
            ShowPlayerConfigMenu();
        }

        public void DelPlayer() 
        {
            if (gameController.Users.Count > 0)
            {

                AnsiConsole.MarkupLine("Joueurs actuels :");
                foreach (User user in gameController.Users)
                {
                    AnsiConsole.MarkupLine(user.ToString());
                }
                string playerName = AnsiConsole.Prompt(
                new TextPrompt<string>("Supprimer un joueur :"));
                gameController.Users.RemoveAll(user => user.Name == playerName);
                string option = AnsiConsole.Prompt(new TextPrompt<string>("Voulez vous supprimez un autre joueur ? (oui/non)"));
                if (option == "oui")
                {
                    DelPlayer();
                }
                else if (option == "non")
                {
                    ShowPlayerConfigMenu();
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Il n'y a pas de joueur à éditer[/]");
                ShowPlayerConfigMenu();
            }
        }

        public void EditSizeParameter()
        {
            tabSize = AnsiConsole.Prompt(
                new TextPrompt<int>($"Taille du plateau (min 4), actuellement ({tabSize}) :")
                .Validate(n => n >= 4 ? ValidationResult.Success() : ValidationResult.Error("[red]La taille doit être ≥ 4[/]"))
            );
            ShowGameConfigMenu();
        }

        public void ShowGameConfigMenu()
        {
            var configActions = new Dictionary<string, Action>
            {
                // Settings
                { $"Éditer la taille du plateau : {tabSize}", EditSizeParameter },
                { "Liste des joueurs", ShowPlayerConfigMenu },


                // Fixed elements
                { "Continuer avec ces paramètres", () =>
                    {
                        AnsiConsole.MarkupLine($"[yellow]Lancement d'une partie avec un tableau de taille {tabSize}[/]");
                        gameController.CreateGame(tabSize);
                    }
                },
                { "Retourner au menu", () =>
                    {
                        ResetConfigValues();
                        mainMenu.ShowMainMenu();
                    }
                }
            };

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Édition des paramètres")
                    .PageSize(5)
                    .AddChoices(configActions.Keys)
            );

            configActions[choice].Invoke();
        }

        public void ShowPlayerList()
        {
            AnsiConsole.Clear();
            string userList = $"[blue] Liste des joueurs ({gameController.Users.Count}) :[/]";

            if (gameController.Users.Count > 0)
            {
                foreach (User user in gameController.Users)
                {
                    userList += $"\n- {user.Name}";
                }
            }
            else
            {
                userList += "\n La liste est vide...";
            }

            AnsiConsole.MarkupLine(userList + "\n");
        }
        private void ShowPlayerConfigMenu()
        {
            ShowPlayerList();

            var configPlayerActions = new Dictionary<string, Action>
            {
                { "Ajouter des joueurs", AddPlayer },
                { "Supprimer des joueurs", DelPlayer },
                { "Retourner au menu", ShowGameConfigMenu }
            };

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Édition des joueurs :")
                    .PageSize(5)
                    .AddChoices(configPlayerActions.Keys)
            );

            configPlayerActions[choice].Invoke();
        }
    }
}
