using game.Controller;
using game.Locale;
using Spectre.Console;

namespace game.View
{
    internal class MainMenu
    {
        private SettingsMenu settingsMenu;
        private GameConfigMenu gameConfigMenu;
        private GameMenu gameMenu;
        public MainMenu()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine($"[green]{LocaleController.GetLocaleLabel("menu.welcome")}[/]");
            
            settingsMenu = new SettingsMenu(this);
            gameMenu = new GameMenu(this);
            gameConfigMenu = new GameConfigMenu(this, gameMenu);
        }

        public void ShowMainMenu()
        {
            AnsiConsole.Clear();
            var menuActions = new Dictionary<string, Action>
            {
                { LocaleController.GetLocaleLabel("menu.start"), () => 
                    {
                        AnsiConsole.Clear();
                        gameConfigMenu.ShowGameConfigMenu(); 
                    } 
                },
                { LocaleController.GetLocaleLabel("menu.load"), () => AnsiConsole.MarkupLine($"[yellow]{LocaleController.GetLocaleLabel("menu.load.message")}[/]") },
                { LocaleController.GetLocaleLabel("menu.settings"), settingsMenu.ShowSettingsMenu },
                { LocaleController.GetLocaleLabel("menu.exit"), () => { AnsiConsole.MarkupLine($"[red]{LocaleController.GetLocaleLabel("goodbye")}[/]"); Environment.Exit(0); } }
            };

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title(LocaleController.GetLocaleLabel("menu.title"))
                    .PageSize(10)
                    .AddChoices(menuActions.Keys));

            menuActions[choice].Invoke();
        }
    }
}
