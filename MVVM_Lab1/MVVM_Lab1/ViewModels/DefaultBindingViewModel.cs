using MVVM_Lab1.Models;
using System;
using MVVM_Lab1.Libraries.LocalizationLibrary;

namespace MVVM_Lab1.ViewModels
{
    public class DefaultBindingViewModel : ViewModelBase
    {
        private string _userName;
        private int _age;
        private bool _isSubscribed;
        private SampleModel _model;
        private readonly LocalizationService _localization;

        public LocalizationService Localization => _localization;

        public DefaultBindingViewModel()
        {
            _model = new SampleModel();
            _userName = "Иван Петров";
            _age = 30;
            _isSubscribed = true;
            _localization = LocalizationService.Instance;

            StartTimer();
        }

        public string UserName
        {
            get => _userName;
            set
            {
                if (SetProperty(ref _userName, value))
                {
                    System.Diagnostics.Debug.WriteLine($"Имя изменено на: {value}");
                }
            }
        }

        public int Age
        {
            get => _age;
            set => SetProperty(ref _age, value);
        }

        public bool IsSubscribed
        {
            get => _isSubscribed;
            set
            {
                if (SetProperty(ref _isSubscribed, value))
                {
                    System.Diagnostics.Debug.WriteLine($"Подписка изменена на: {value}");
                }
            }
        }

        public string ReadOnlyData => $"{Localization.GetString("Created")} {_model.CreationDate:HH:mm:ss}";

        public string ModelText
        {
            get => _model.TextData;
            set
            {
                if (_model.TextData != value)
                {
                    _model.TextData = value;
                    OnPropertyChanged();
                }
            }
        }

        private void StartTimer()
        {
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) =>
            {
                OnPropertyChanged(nameof(ReadOnlyData));
            };
            timer.Start();
        }
    }
}