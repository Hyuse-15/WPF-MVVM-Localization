using System;
using System.Timers;
using MVVM_Lab1.Libraries.LocalizationLibrary;

namespace MVVM_Lab1.ViewModels
{
    public class OneTimeBindingViewModel : ViewModelBase
    {
        private DateTime _loadTime;
        private int _randomNumber;
        private Timer _updateTimer;
        private readonly LocalizationService _localization;

        public LocalizationService Localization => _localization;

        public OneTimeBindingViewModel()
        {
            _loadTime = DateTime.Now;
            _localization = LocalizationService.Instance;

            Random rnd = new Random();
            _randomNumber = rnd.Next(1, 1000);

            StartTimer();
        }

        public DateTime LoadTime
        {
            get => _loadTime;
            set => SetProperty(ref _loadTime, value);
        }

        public int RandomNumber
        {
            get => _randomNumber;
            set => SetProperty(ref _randomNumber, value);
        }

        private DateTime _currentTime;
        public DateTime CurrentTime
        {
            get => _currentTime;
            set => SetProperty(ref _currentTime, value);
        }

        private void StartTimer()
        {
            _updateTimer = new Timer(1000);
            _updateTimer.Elapsed += (s, e) =>
            {
                LoadTime = DateTime.Now;

                Random rnd = new Random();
                RandomNumber = rnd.Next(1, 1000);

                CurrentTime = DateTime.Now;
            };
            _updateTimer.Start();
        }
    }
}