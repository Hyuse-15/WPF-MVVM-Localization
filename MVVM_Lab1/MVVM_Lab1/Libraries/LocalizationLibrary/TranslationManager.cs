using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace MVVM_Lab1.Libraries.LocalizationLibrary
{
    /// <summary>
    /// Менеджер переводов
    /// </summary>
    public class TranslationManager
    {
        private Dictionary<string, string> _currentTranslations;
        private string _currentLanguage;
        private string _resourcesPath;

        public event EventHandler<LanguageChangedEventArgs> LanguageChanged;

        public string CurrentLanguage => _currentLanguage;

        public IReadOnlyList<LanguageInfo> AvailableLanguages { get; }

        public TranslationManager()
        {
            // Путь к ресурсам внутри папки Libraries
            _resourcesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Libraries/LocalizationLibrary/Resources/");

            // Если папки нет, пробуем альтернативный путь
            if (!Directory.Exists(_resourcesPath))
            {
                _resourcesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/");
            }

            // Если всё равно нет, создаём папку
            if (!Directory.Exists(_resourcesPath))
            {
                Directory.CreateDirectory(_resourcesPath);
            }

            AvailableLanguages = new List<LanguageInfo>
            {
                new LanguageInfo("ru", "Russian", "Русский"),
                new LanguageInfo("zh", "Chinese", "中文")
            };

            _currentTranslations = new Dictionary<string, string>();
            _currentLanguage = "ru";

            LoadLanguage(_currentLanguage);
        }

        public void LoadLanguage(string languageCode)
        {
            if (_currentLanguage == languageCode && _currentTranslations.Count > 0)
                return;

            try
            {
                string filePath = Path.Combine(_resourcesPath, $"Strings.{languageCode}.json");

                if (!File.Exists(filePath))
                {
                    System.Diagnostics.Debug.WriteLine($"Файл не найден: {filePath}");
                    // Создаём файл с пустыми данными, если его нет
                    CreateDefaultLanguageFile(languageCode);
                }

                string jsonContent = File.ReadAllText(filePath);
                var translations = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent);

                if (translations != null)
                {
                    _currentTranslations = translations;
                    _currentLanguage = languageCode;

                    LanguageChanged?.Invoke(this, new LanguageChangedEventArgs(languageCode));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка загрузки языка {languageCode}: {ex.Message}");
            }
        }

        private void CreateDefaultLanguageFile(string languageCode)
        {
            var defaultTranslations = new Dictionary<string, string>
            {
                { "WindowTitle", "WPF Application" },
                { "MainTitle", "MVVM Demo" },
                { "LanguageLabel", "Language:" }
            };

            string jsonContent = JsonSerializer.Serialize(defaultTranslations, new JsonSerializerOptions { WriteIndented = true });
            string filePath = Path.Combine(_resourcesPath, $"Strings.{languageCode}.json");
            File.WriteAllText(filePath, jsonContent);
        }

        public string GetString(string key)
        {
            if (_currentTranslations != null && _currentTranslations.TryGetValue(key, out string value))
                return value;

            System.Diagnostics.Debug.WriteLine($"Ключ не найден: {key}");
            return key;
        }
    }

    public class LanguageChangedEventArgs : EventArgs
    {
        public string LanguageCode { get; }

        public LanguageChangedEventArgs(string languageCode)
        {
            LanguageCode = languageCode;
        }
    }
}