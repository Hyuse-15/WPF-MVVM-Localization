using System;

namespace MVVM_Lab1.Models
{
    /// <summary>
    /// Модель данных для демонстрации привязок.
    /// В реальном приложении здесь могла бы быть бизнес-логика, работа с БД и т.д.
    /// </summary>
    public class SampleModel
    {
        private string _textData;
        private int _numericValue;
        private DateTime _creationDate;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public SampleModel()
        {
            _textData = "Начальные данные";
            _numericValue = 42;
            _creationDate = DateTime.Now;
        }

        /// <summary>
        /// Текстовые данные
        /// </summary>
        public string TextData
        {
            get => _textData;
            set
            {
                if (_textData != value)
                {
                    _textData = value;
                    // Обратите внимание: Model НЕ реализует INotifyPropertyChanged!
                    // Поэтому ViewModel должна следить за изменениями модели.
                }
            }
        }

        /// <summary>
        /// Числовые данные
        /// </summary>
        public int NumericValue
        {
            get => _numericValue;
            set
            {
                if (_numericValue != value)
                {
                    _numericValue = value;
                }
            }
        }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate => _creationDate;
    }
}