using System.Text.Json;

namespace game.Locale
{
    public enum LocaleEnum
    {
        FR = 1,
        EN = 2,
    }

    internal static class LocaleController
    {
        // La langue par défaut est le français
        private static LocaleEnum CurrentLocale = LocaleEnum.FR;

        private static Dictionary<string, string> translations = new();

        public static void LoadLocale(LocaleEnum locale)
        {
            string fileName = locale switch
            {
                LocaleEnum.FR => "Locale/Translations/fr.json",
                LocaleEnum.EN => "Locale/Translations/en.json",
                _ => "Locale/Translations/fr.json"
            };

            if (!File.Exists(fileName))
                throw new FileNotFoundException($"Locale file not found: {fileName}");

            string json = File.ReadAllText(fileName);

            translations = JsonSerializer.Deserialize<Dictionary<string, string>>(json)
                            ?? new Dictionary<string, string>();

            CurrentLocale = locale;
        }

        public static string GetLocaleLabel(string key)
        {
            if (translations.TryGetValue(key, out var value))
                return value;

            // Si la clé n'existe pas on renvoie la clé pour debug
            return $"{key}";
        }
    }
}
