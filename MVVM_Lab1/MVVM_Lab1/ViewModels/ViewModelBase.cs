using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVM_Lab1.ViewModels
{
    /// <summary>
    /// Базовый класс для всех ViewModel в ручной реализации.
    /// Реализует интерфейс INotifyPropertyChanged для уведомления об изменениях свойств.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Событие, которое возникает при изменении значения свойства.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод для вызова события PropertyChanged.
        /// </summary>
        /// <param name="propertyName">Имя свойства, которое изменилось. 
        /// Атрибут CallerMemberName автоматически подставит имя вызывающего свойства.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Универсальный метод для установки значения свойства с проверкой на изменение
        /// и автоматическим вызовом OnPropertyChanged.
        /// </summary>
        /// <typeparam name="T">Тип свойства</typeparam>
        /// <param name="field">Ссылка на поле, где хранится значение</param>
        /// <param name="value">Новое значение</param>
        /// <param name="propertyName">Имя свойства (подставляется автоматически)</param>
        /// <returns>true, если значение изменилось; false, если значение осталось прежним</returns>
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            // Проверяем, изменилось ли значение
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}