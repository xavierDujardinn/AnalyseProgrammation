using game.Locale;
using Spectre.Console;

namespace game.View
{
    internal class MainMenu
    {
        private SettingsMenu settingsMenu;
        public MainMenu()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine($"[green]{LocaleController.GetLocaleLabel("menu.welcome")}[/]");
            settingsMenu = new SettingsMenu(this);
        }

        public void ShowMainMenu()
        {
            AnsiConsole.Clear();
            var menuActions = new Dictionary<string, Action>
            {
                { LocaleController.GetLocaleLabel("menu.start"), () => AnsiConsole.MarkupLine("[yellow]Starting a new game...[/]") },
                { LocaleController.GetLocaleLabel("menu.load"), () => AnsiConsole.MarkupLine("[yellow]Loading saved game...[/]") },
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
