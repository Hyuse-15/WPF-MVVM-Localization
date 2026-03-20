using System;
using System.Timers;

namespace MVVM_Lab1.ViewModels
{
    /// <summary>
    /// ViewModel для демонстрации односторонней привязки (Mode=OneWay)
    /// Изменения в ViewModel отображаются в UI, но UI не может изменить ViewModel
    /// </summary>
    public class OneWayBindingViewModel : ViewModelBase
    {
        private string _statusMessage;
        private double _progressValue;
        private int _counter;
        private Timer _timer;

        public OneWayBindingViewModel()
        {
            _statusMessage = "Система инициализирована";
            _progressValue = 0;
            _counter = 0;

            StartTimer();
        }

        /// <summary>
        /// Статусное сообщение (OneWay из VM в UI)
        /// </summary>
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        /// <summary>
        /// Значение прогресса (OneWay из VM в UI)
        /// </summary>
        public double ProgressValue
        {
            get => _progressValue;
            set => SetProperty(ref _progressValue, value);
        }

        /// <summary>
        /// Счетчик (OneWay из VM в UI)
        /// </summary>
        public int Counter
        {
            get => _counter;
            set => SetProperty(ref _counter, value);
        }

        /// <summary>
        /// Текущее время (OneWay из VM в UI)
        /// </summary>
        private DateTime _currentTime;
        public DateTime CurrentTime
        {
            get => _currentTime;
            set => SetProperty(ref _currentTime, value);
        }

        private void StartTimer()
        {
            _timer = new Timer(100);
            _timer.Elapsed += (s, e) =>
            {
                // Обновляем значения в VM - UI должен отобразить изменения
                Counter++;
                ProgressValue = (Counter % 100) / 100.0;
                StatusMessage = $"Обработано операций: {Counter}";
                CurrentTime = DateTime.Now;
            };
            _timer.Start();
        }
    }
}