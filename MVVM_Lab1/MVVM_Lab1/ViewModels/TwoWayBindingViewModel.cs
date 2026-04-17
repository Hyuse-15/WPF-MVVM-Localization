using MVVM_Lab1.Models;
using System;
using MVVM_Lab1.Libraries.LocalizationLibrary;

namespace MVVM_Lab1.ViewModels
{
    public class TwoWayBindingViewModel : ViewModelBase
    {
        private string _firstName;
        private string _lastName;
        private string _fullName;
        private int _sliderValue;
        private bool _isChecked;
        private DateTime _selectedDate;
        private SampleModel _model;

        private readonly LocalizationService _localization;

        public LocalizationService Localization => _localization;

        public TwoWayBindingViewModel()
        {
            _model = new SampleModel();
            _firstName = "Иван";
            _lastName = "Иванов";
            _sliderValue = 50;
            _isChecked = false;
            _selectedDate = DateTime.Today;
            _localization = LocalizationService.Instance;

            UpdateFullName();
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (SetProperty(ref _firstName, value))
                {
                    UpdateFullName();
                }
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (SetProperty(ref _lastName, value))
                {
                    UpdateFullName();
                }
            }
        }

        public string FullName
        {
            get => _fullName;
            private set => SetProperty(ref _fullName, value);
        }

        public int SliderValue
        {
            get => _sliderValue;
            set
            {
                if (SetProperty(ref _sliderValue, value))
                {
                    System.Diagnostics.Debug.WriteLine($"Slider значение: {value}");
                }
            }
        }

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (SetProperty(ref _isChecked, value))
                {
                    System.Diagnostics.Debug.WriteLine($"Специальный режим: {value}");
                }
            }
        }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

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

        private void UpdateFullName()
        {
            FullName = $"{FirstName} {LastName}";
        }
    }
}