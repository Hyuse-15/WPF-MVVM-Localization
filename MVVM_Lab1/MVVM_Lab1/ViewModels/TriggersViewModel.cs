using System;
using System.Windows.Input;

namespace MVVM_Lab1.ViewModels
{
    /// <summary>
    /// ViewModel для демонстрации триггеров
    /// </summary>
    public class TriggersViewModel : ViewModelBase
    {
        private bool _isSpecialMode;
        private string _statusText;
        private int _progressValue;
        private bool _isButtonEnabled;
        private ICommand _increaseProgressCommand;
        private ICommand _resetCommand;

        public TriggersViewModel()
        {
            _isSpecialMode = false;
            _statusText = "Обычный режим";
            _progressValue = 0;
            _isButtonEnabled = true;
        }

        /// <summary>
        /// Флаг специального режима (для DataTrigger)
        /// </summary>
        public bool IsSpecialMode
        {
            get => _isSpecialMode;
            set
            {
                if (SetProperty(ref _isSpecialMode, value))
                {
                    StatusText = value ? "Специальный режим активен" : "Обычный режим";
                }
            }
        }

        /// <summary>
        /// Текст статуса (для отображения)
        /// </summary>
        public string StatusText
        {
            get => _statusText;
            set => SetProperty(ref _statusText, value);
        }

        /// <summary>
        /// Значение прогресса (для триггеров)
        /// </summary>
        public int ProgressValue
        {
            get => _progressValue;
            set
            {
                if (SetProperty(ref _progressValue, value))
                {
                    // Автоматически отключаем кнопку при достижении 100%
                    if (value >= 100)
                    {
                        IsButtonEnabled = false;
                        StatusText = "Завершено!";
                    }
                }
            }
        }

        /// <summary>
        /// Доступность кнопки (для триггеров)
        /// </summary>
        public bool IsButtonEnabled
        {
            get => _isButtonEnabled;
            set => SetProperty(ref _isButtonEnabled, value);
        }

        /// <summary>
        /// Команда для увеличения прогресса
        /// </summary>
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
            }
        }

        private bool CanIncreaseProgress()
        {
            return ProgressValue < 100;
        }

        /// <summary>
        /// Команда для сброса
        /// </summary>
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
            StatusText = "Обычный режим";
        }
    }

    /// <summary>
    /// Простая реализация ICommand для ручной ветки
    /// </summary>
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