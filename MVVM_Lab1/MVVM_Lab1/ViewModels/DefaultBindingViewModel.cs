using MVVM_Lab1.Models;
using System;

namespace MVVM_Lab1.ViewModels
{
    /// <summary>
    /// ViewModel для демонстрации привязки по умолчанию (Mode=Default)
    /// </summary>
    public class DefaultBindingViewModel : ViewModelBase
    {
        // Поля для хранения данных
        private string _userName;
        private int _age;
        private bool _isSubscribed;
        private SampleModel _model;

        /// <summary>
        /// Конструктор
        /// </summary>
        public DefaultBindingViewModel()
        {
            _model = new SampleModel();
            _userName = "Иван Петров";
            _age = 30;
            _isSubscribed = true;

            // Запускаем таймер для демонстрации автоматического обновления
            StartTimer();
        }

        /// <summary>
        /// Свойство для имени пользователя
        /// </summary>
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        /// <summary>
        /// Свойство для возраста
        /// </summary>
        public int Age
        {
            get => _age;
            set => SetProperty(ref _age, value);
        }

        /// <summary>
        /// Свойство для подписки на рассылку
        /// </summary>
        public bool IsSubscribed
        {
            get => _isSubscribed;
            set => SetProperty(ref _isSubscribed, value);
        }

        /// <summary>
        /// Свойство только для чтения (OneTime привязка будет здесь хорошо заметна)
        /// </summary>
        public string ReadOnlyData => $"Создано: {_model.CreationDate:HH:mm:ss}";

        /// <summary>
        /// Свойство для демонстрации данных из модели
        /// </summary>
        public string ModelText
        {
            get => _model.TextData;
            set
            {
                if (_model.TextData != value)
                {
                    _model.TextData = value;
                    OnPropertyChanged(); // Уведомляем об изменении
                }
            }
        }

        /// <summary>
        /// Запускаем таймер для автоматического обновления ReadOnlyData
        /// </summary>
        private void StartTimer()
        {
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) =>
            {
                // Принудительно обновляем свойство ReadOnlyData
                OnPropertyChanged(nameof(ReadOnlyData));
            };
            timer.Start();
        }
    }
}