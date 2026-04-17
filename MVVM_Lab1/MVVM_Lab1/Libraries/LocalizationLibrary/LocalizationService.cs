using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace MVVM_Lab1.Libraries.LocalizationLibrary
{
    /// <summary>
    /// Сервис локализации для использования в ViewModel
    /// </summary>
    public class LocalizationService : INotifyPropertyChanged
    {
        private readonly TranslationManager _translationManager;
        private static LocalizationService _instance;

        public event PropertyChangedEventHandler PropertyChanged;

        public static LocalizationService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LocalizationService();
                return _instance;
            }
        }

        private LocalizationService()
        {
            _translationManager = new TranslationManager();
            _translationManager.LanguageChanged += OnLanguageChanged;
        }

        public string this[string key] => GetString(key);

        public string GetString(string key)
        {
            return _translationManager.GetString(key);
        }

        public void SetLanguage(string languageCode)
        {
            _translationManager.LoadLanguage(languageCode);
        }

        public string CurrentLanguage => _translationManager.CurrentLanguage;

        public IReadOnlyList<LanguageInfo> AvailableLanguages => _translationManager.AvailableLanguages;

        private void OnLanguageChanged(object sender, LanguageChangedEventArgs e)
        {
            OnPropertyChanged(string.Empty);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}