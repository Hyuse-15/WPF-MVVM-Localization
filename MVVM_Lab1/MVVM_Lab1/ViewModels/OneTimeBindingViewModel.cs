using System;
using System.Timers;

namespace MVVM_Lab1.ViewModels
{
    /// <summary>
    /// ViewModel для демонстрации одноразовой привязки (Mode=OneTime)
    /// Значения устанавливаются один раз при загрузке и больше не обновляются
    /// </summary>
    public class OneTimeBindingViewModel : ViewModelBase
    {
        private string _staticText;
        private DateTime _loadTime;
        private int _randomNumber;
        private Timer _updateTimer;

        public OneTimeBindingViewModel()
        {
            // Эти значения устанавливаются в конструкторе
            _staticText = "Это значение установлено при загрузке";
            _loadTime = DateTime.Now;

            // Генерируем случайное число
            Random rnd = new Random();
            _randomNumber = rnd.Next(1, 1000);

            // Запускаем таймер, который будет пытаться обновить значения
            StartTimer();
        }

        /// <summary>
        /// Статический текст (OneTime привязка)
        /// </summary>
        public string StaticText
        {
            get => _staticText;
            set => SetProperty(ref _staticText, value);
        }

        /// <summary>
        /// Время загрузки (OneTime привязка)
        /// </summary>
        public DateTime LoadTime
        {
            get => _loadTime;
            set => SetProperty(ref _loadTime, value);
        }

        /// <summary>
        /// Случайное число (OneTime привязка)
        /// </summary>
        public int RandomNumber
        {
            get => _randomNumber;
            set => SetProperty(ref _randomNumber, value);
        }

        /// <summary>
        /// Текущее время (будет обновляться, но OneTime привязка этого не увидит)
        /// </summary>
        private DateTime _currentTime;
        public DateTime CurrentTime
        {
            get => _currentTime;
            set => SetProperty(ref _currentTime, value);
        }

        private void StartTimer()
        {
            _updateTimer = new Timer(1000); // Каждую секунду
            _updateTimer.Elapsed += (s, e) =>
            {
                // Пытаемся обновить значения
                StaticText = $"Попытка обновления в {DateTime.Now:HH:mm:ss}";
                LoadTime = DateTime.Now;

                Random rnd = new Random();
                RandomNumber = rnd.Next(1, 1000);

                CurrentTime = DateTime.Now;
            };
            _updateTimer.Start();
        }
    }
}