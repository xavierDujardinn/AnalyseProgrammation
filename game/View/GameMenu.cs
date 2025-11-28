using game.Controller;
using game.GamePackage;
using game.GamePackage.Buildings;
using game.Locale;
using Spectre.Console;
using System;
using System.Collections.Generic;

namespace game.View
{
    internal class GameMenu
    {
        private MainMenu mainMenu;
        public GameController GameController { get; private set; }

        public GameMenu(MainMenu mainMenu)
        {
            this.mainMenu = mainMenu;
            GameController = new GameController();
            GameController.OnNewRoundStarted += DisplayRoundMenu;
        }

        private void DisplayRoundMenu()
        {
            // Format du titre du tour via la locale
            var roundTitleTemplate = LocaleController.GetLocaleLabel("round.title");
            AnsiConsole.MarkupLine($"\n[blue]{string.Format(roundTitleTemplate, GameController.CurrentRoundNumber)}[/]\n");

            AnsiConsole.MarkupLine(GetRessourcesInfos());

            var roundActions = new Dictionary<string, Action>
            {
                { LocaleController.GetLocaleLabel("round.continue"), () => {  } },
                { LocaleController.GetLocaleLabel("round.showTab"), ShowGameTab},
                { LocaleController.GetLocaleLabel("round.placeBuilding"), PlaceBuilding },
                { LocaleController.GetLocaleLabel("round.removeBuilding"), RemoveBuilding },
                { LocaleController.GetLocaleLabel("round.stop"), () =>
                    {
                        GameController.EndGame();
                        AnsiConsole.MarkupLine($"[red]{LocaleController.GetLocaleLabel("round.endgame.message")}[/]");
                        mainMenu.ShowMainMenu();
                    }
                }
            };

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title(LocaleController.GetLocaleLabel("round.prompt"))
                    .PageSize(5)
                    .AddChoices(roundActions.Keys)
            );

            roundActions[choice].Invoke();
        }

        #region building methods
        private BaseBuilding SelectBuilding()
        {
            BaseBuilding building = null;

            var buildingActions = new Dictionary<string, Action>
            {
                { "Iron mine", () => { building = new IronMine(); } },
                { "Gold mine", () => { building = new GoldMine(); } },
            };

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title(LocaleController.GetLocaleLabel("Choississez le type de batiment"))
                    .PageSize(5)
                    .AddChoices(buildingActions.Keys)
            );

            buildingActions[choice].Invoke();

            return building;
        }

        private void PlaceBuilding()
        {
            AnsiConsole.Clear();

            BaseBuilding building = SelectBuilding();

            AnsiConsole.MarkupLine($"[yellow]{LocaleController.GetLocaleLabel("placebuilding.header")}[/]\n");

            int maxCoord = GameController.GameTab.Size - 1;

            int x = AnsiConsole.Prompt(
                new TextPrompt<int>(string.Format(LocaleController.GetLocaleLabel("prompt.coordX"), maxCoord))
                    .Validate(n => n>= 0 && n <= maxCoord)
                    .ValidationErrorMessage($"[red]{string.Format(LocaleController.GetLocaleLabel("validation.coord.range"), maxCoord)}[/]")
            );

            int y = AnsiConsole.Prompt(
                new TextPrompt<int>(string.Format(LocaleController.GetLocaleLabel("prompt.coordY"), maxCoord))
                    .Validate(n => n >= 0 && n <= maxCoord)
                    .ValidationErrorMessage($"[red]{string.Format(LocaleController.GetLocaleLabel("validation.coord.range"), maxCoord)}[/]")
            );

            AnsiConsole.Clear();

            if (GameController.GameTab.PlaceBuilding(x, y, building))
            {
                AnsiConsole.MarkupLine($"[green]{string.Format(LocaleController.GetLocaleLabel("placebuilding.success"), x, y)}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]{string.Format(LocaleController.GetLocaleLabel("placebuilding.failure"), x, y)}[/]");
            }
            DisplayRoundMenu();
        }

        private void RemoveBuilding()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine($"[yellow]{string.Format(LocaleController.GetLocaleLabel("removebuilding.header"))}[/]");

            int maxCoord = GameController.GameTab.Size - 1;

            int x = AnsiConsole.Prompt(
                new TextPrompt<int>(string.Format(LocaleController.GetLocaleLabel("prompt.coordX"), maxCoord))
                    .Validate(n => n >= 0 && n <= maxCoord)
                    .ValidationErrorMessage($"[red]{string.Format(LocaleController.GetLocaleLabel("validation.coord.range"), maxCoord)}[/]")
            );

            int y = AnsiConsole.Prompt(
                new TextPrompt<int>(string.Format(LocaleController.GetLocaleLabel("prompt.coordY"), maxCoord))
                    .Validate(n => n >= 0 && n <= maxCoord)
                    .ValidationErrorMessage($"[red]{string.Format(LocaleController.GetLocaleLabel("validation.coord.range"), maxCoord)}[/]")
            );

            CellContent? content = GameController.GameTab.GetCellContent(x, y);

            if (content is BaseBuilding)
            {
                GameController.GameTab.RemoveBuilding(x, y);
                AnsiConsole.MarkupLine($"[green]{string.Format(LocaleController.GetLocaleLabel("removebuilding.success"), x, y)}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]{string.Format(LocaleController.GetLocaleLabel("removebuilding.failure"), x, y)}[/]");
            }
            DisplayRoundMenu();
        }
        #endregion

        public void ShowGameTab()
        {
            Tab tab = GameController.GameTab;
            Cell[,] cells = tab.Cells;
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                string line = "";

                for (int x = 0; x < cells.GetLength(0); x++)
                {
                    line += $"[yellow]{cells[x, y]}[/] ";
                }

                AnsiConsole.MarkupLine(line);
            }
            DisplayRoundMenu();
        }

        public string GetRessourcesInfos()
        {
            string infos = "[yellow]|----------------------------------------------------------------------------\n|\t";

            Dictionary<string, int> ressources = GameController.GameTab.GetAllRessourcesOnTab();

            if (ressources.Count > 0)
            {
                foreach (string ressource in ressources.Keys)
                {
                    infos += $"{ressource} : {ressources[ressource]} \t";
                }
            }
            else
            {
                infos += "Aucune ressource";
            }

                return infos + "\n|----------------------------------------------------------------------------[/]\n";
        }
    }
}
