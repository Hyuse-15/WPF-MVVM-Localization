using System.Windows;
using System.Windows.Controls;

namespace MVVM_Lab1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LanguageSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LanguageSelector.SelectedItem is ComboBoxItem selectedItem)
            {
                string languageCode = selectedItem.Tag.ToString();
                ChangeLanguage(languageCode);
            }
        }

        private void ChangeLanguage(string languageCode)
        {
            // Базовый метод, который будет переопределен в каждой ветке
            // В каждой ветке реализация будет разной
        }
    }
}