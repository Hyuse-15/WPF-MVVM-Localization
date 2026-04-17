using System.Windows;
using MVVM_Lab1.ViewModels;

namespace MVVM_Lab1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Устанавливаем DataContext
            this.DataContext = new MainWindowViewModel();
        }
    }
}