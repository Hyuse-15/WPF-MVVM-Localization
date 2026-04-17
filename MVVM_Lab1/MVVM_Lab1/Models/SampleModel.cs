using System;

namespace MVVM_Lab1.Models
{
    public class SampleModel
    {
        private string _textData;
        private int _numericValue;
        private DateTime _creationDate;

        public SampleModel()
        {
            _textData = "Начальные данные";
            _numericValue = 42;
            _creationDate = DateTime.Now;
        }

        public string TextData
        {
            get => _textData;
            set
            {
                if (_textData != value)
                {
                    _textData = value;
                }
            }
        }

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

        public DateTime CreationDate => _creationDate;
    }
}