using System;
using System.Timers;
using MVVM_Lab1.Libraries.LocalizationLibrary;

namespace MVVM_Lab1.ViewModels
{
    public class OneWayBindingViewModel : ViewModelBase
    {
        private string _statusMessage;
        private double _progressValue;
        private int _counter;
        private Timer _timer;
        private readonly LocalizationService _localization;

        public LocalizationService Localization => _localization;

        public OneWayBindingViewModel()
        {
            _localization = LocalizationService.Instance;
            _statusMessage = _localization.GetString("SystemInitialized");
            _progressValue = 0;
            _counter = 0;

            StartTimer();
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public double ProgressValue
        {
            get => _progressValue;
            set => SetProperty(ref _progressValue, value);
        }

        public int Counter
        {
            get => _counter;
            set => SetProperty(ref _counter, value);
        }

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
                Counter++;
                ProgressValue = (Counter % 100) / 100.0;
                StatusMessage = $"{_localization.GetString("OperationsProcessed")} {Counter}";
                CurrentTime = DateTime.Now;

                if (Counter == 100)
                {
                    System.Diagnostics.Debug.WriteLine("Достигнуто 100 операций");
                }
            };
            _timer.Start();
        }
    }
}