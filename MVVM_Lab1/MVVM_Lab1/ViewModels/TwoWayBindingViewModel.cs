using MVVM_Lab1.Models;
using System;

namespace MVVM_Lab1.ViewModels
{
    /// <summary>
    /// ViewModel для демонстрации двухсторонней привязки (Mode=TwoWay)
    /// </summary>
    public class TwoWayBindingViewModel : ViewModelBase
    {
        private string _firstName;
        private string _lastName;
        private string _fullName;
        private int _sliderValue;
        private bool _isChecked;
        private DateTime _selectedDate;
        private SampleModel _model;

        public TwoWayBindingViewModel()
        {
            _model = new SampleModel();
            _firstName = "Иван";
            _lastName = "Иванов";
            _sliderValue = 50;
            _isChecked = false;
            _selectedDate = DateTime.Today;

            UpdateFullName();
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (SetProperty(ref _firstName, value))
                {
                    UpdateFullName(); // При изменении имени обновляем полное имя
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
                    UpdateFullName(); // При изменении фамилии обновляем полное имя
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
                    // Здесь можно добавить логику при изменении значения слайдера
                }
            }
        }

        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        // Свойство для демонстрации TwoWay привязки с моделью
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