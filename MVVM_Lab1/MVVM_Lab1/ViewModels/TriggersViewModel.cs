using System;
using System.Windows.Input;
using MVVM_Lab1.Libraries.LocalizationLibrary;

namespace MVVM_Lab1.ViewModels
{
    public class TriggersViewModel : ViewModelBase
    {
        private bool _isSpecialMode;
        private string _statusText;
        private int _progressValue;
        private bool _isButtonEnabled;
        private ICommand _increaseProgressCommand;
        private ICommand _resetCommand;
        private readonly LocalizationService _localization;

        public LocalizationService Localization => _localization;

        public TriggersViewModel()
        {
            _localization = LocalizationService.Instance;
            _isSpecialMode = false;
            _statusText = _localization.GetString("NormalMode");
            _progressValue = 0;
            _isButtonEnabled = true;
        }

        public bool IsSpecialMode
        {
            get => _isSpecialMode;
            set
            {
                if (SetProperty(ref _isSpecialMode, value))
                {
                    StatusText = value ?
                        _localization.GetString("SpecialModeActive") :
                        _localization.GetString("NormalMode");

                    System.Diagnostics.Debug.WriteLine($"Специальный режим: {value}");
                }
            }
        }

        public string StatusText
        {
            get => _statusText;
            set => SetProperty(ref _statusText, value);
        }

        public int ProgressValue
        {
            get => _progressValue;
            set
            {
                if (SetProperty(ref _progressValue, value))
                {
                    if (value >= 100)
                    {
                        IsButtonEnabled = false;
                        StatusText = _localization.GetString("Completed");
                        System.Diagnostics.Debug.WriteLine("Прогресс достиг 100%");
                    }
                }
            }
        }

        public bool IsButtonEnabled
        {
            get => _isButtonEnabled;
            set => SetProperty(ref _isButtonEnabled, value);
        }

        public ICommand IncreaseProgressCommand
        {
            get
            {
                if (_increaseProgressCommand == null)
                {
                    _increaseProgressCommand = new RelayCommand(IncreaseProgress, CanIncreaseProgress);
                }
                return _increaseProgressCommand;
            }
        }

        private void IncreaseProgress()
        {
            if (ProgressValue < 100)
            {
                ProgressValue += 10;
                System.Diagnostics.Debug.WriteLine($"Прогресс увеличен до: {ProgressValue}%");
            }
        }

        private bool CanIncreaseProgress()
        {
            return ProgressValue < 100;
        }

        public ICommand ResetCommand
        {
            get
            {
                if (_resetCommand == null)
                {
                    _resetCommand = new RelayCommand(Reset);
                }
                return _resetCommand;
            }
        }

        private void Reset()
        {
            ProgressValue = 0;
            IsButtonEnabled = true;
            IsSpecialMode = false;
            StatusText = _localization.GetString("NormalMode");
            System.Diagnostics.Debug.WriteLine("Сброс выполнен");
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

        public void Execute(object parameter) => _execute();
    }
}