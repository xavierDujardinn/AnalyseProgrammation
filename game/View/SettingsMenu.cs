using game.Locale;
using Spectre.Console;

namespace game.View
{
    internal class SettingsMenu
    {
        private MainMenu mainMenu;

        public SettingsMenu(MainMenu mainMenu)
        {
            this.mainMenu = mainMenu;
        }

        private void ShowLanguageSettings()
        {
            AnsiConsole.Clear();
            bool showing = true;
            while (showing)
            {
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine($"[yellow]{LocaleController.GetLocaleLabel("greeting")}[/]");

                var languageActions = new Dictionary<string, Action>
                {
                    { LocaleController.GetLocaleLabel("language.french"), () => LocaleController.LoadLocale(LocaleEnum.FR) },
                    { LocaleController.GetLocaleLabel("language.english"), () => LocaleController.LoadLocale(LocaleEnum.EN) },
                    { LocaleController.GetLocaleLabel("menu.back"), () => {showing = false; } }
                };

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title(Markup.Escape(LocaleController.GetLocaleLabel("settings.language.title")))
                        .AddChoices(languageActions.Keys.Select(Markup.Escape))
                );

                languageActions.First(kv => Markup.Escape(kv.Key) == choice).Value.Invoke();
            }
        }

        public void ShowSettingsMenu()
        {
            bool showing = true;
            while (showing)
            {
                var settingsActions = new Dictionary<string, Action>
                {
                    { LocaleController.GetLocaleLabel("settings.language"), ShowLanguageSettings },
                    { LocaleController.GetLocaleLabel("menu.back"), () => {showing=false; } }
                };

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title(Markup.Escape(LocaleController.GetLocaleLabel("settings.title")))
                        .AddChoices(settingsActions.Keys.Select(Markup.Escape))
                );

                settingsActions.First(kv => Markup.Escape(kv.Key) == choice).Value.Invoke();
            }
            mainMenu.ShowMainMenu();
        }
    }
}
