using MVVM_Lab1.Libraries.LocalizationLibrary;
using MVVM_Lab1.UserControls;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MVVM_Lab1.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly LocalizationService _localization;
        private LanguageInfo _selectedLanguage;
        private ICommand _showMessageCommand;

        public ObservableCollection<LanguageInfo> Languages { get; }

        public LocalizationService Localization => _localization;

        public UserControl DefaultBindingControl { get; }
        public UserControl TwoWayBindingControl { get; }
        public UserControl OneTimeBindingControl { get; }
        public UserControl OneWayBindingControl { get; }
        public UserControl TriggersControl { get; }

        public LanguageInfo SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (SetProperty(ref _selectedLanguage, value) && value != null)
                {
                    _localization.SetLanguage(value.Code);
                }
            }
        }

        /// <summary>
        /// Команда для показа локализованного сообщения
        /// </summary>
        public ICommand ShowMessageCommand
        {
            get
            {
                if (_showMessageCommand == null)
                {
                    _showMessageCommand = new RelayCommand(ShowLocalizedMessage);
                }
                return _showMessageCommand;
            }
        }

        public MainWindowViewModel()
        {
            _localization = LocalizationService.Instance;

            // Создаем экземпляры контролов
            DefaultBindingControl = new DefaultBindingControl();
            TwoWayBindingControl = new TwoWayBindingControl();
            OneTimeBindingControl = new OneTimeBindingControl();
            OneWayBindingControl = new OneWayBindingControl();
            TriggersControl = new TriggersControl();

            Languages = new ObservableCollection<LanguageInfo>();
            foreach (var lang in _localization.AvailableLanguages)
            {
                Languages.Add(lang);
            }

            foreach (var lang in Languages)
            {
                if (lang.Code == _localization.CurrentLanguage)
                {
                    SelectedLanguage = lang;
                    break;
                }
            }
        }

        /// <summary>
        /// Показывает локализованное сообщение в MessageBox
        /// </summary>
        private void ShowLocalizedMessage()
        {
            // Получаем локализованный текст в зависимости от текущего языка
            string messageTitle = _localization.GetString("MessageTitle");
            string messageText = _localization.GetString("LocalizedMessageText");

            // Показываем MessageBox
            MessageBox.Show(messageText, messageTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

  
}