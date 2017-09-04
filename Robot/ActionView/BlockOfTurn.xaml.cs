using System.Windows;
using System.Windows.Controls;

namespace Robot
{
    /// <summary>
    /// Логика взаимодействия для BlockOfTurn.xaml
    /// </summary>
    public partial class BlockOfTurn : UserControl
    {
        private readonly AlgorithmWindow _algorithmWindow;
        private bool _isEdit { get; set; }

        public BlockOfTurn(AlgorithmWindow algorithmWindow)
        {
            InitializeComponent();
            _algorithmWindow = algorithmWindow;
            _isEdit = false;
        }
        public BlockOfTurn(AbstractAction action, AlgorithmWindow algorithmWindow)
        {
            InitializeComponent();
            _algorithmWindow = algorithmWindow;
            _isEdit = true;
        }

        private void AddCommand(object sender, RoutedEventArgs e)
        {
            _algorithmWindow.AddAction(DataContext as Turn, _isEdit);
        }

        private void RemoveCommand(object sender, RoutedEventArgs e)
        {
            _algorithmWindow.RemoveAction(DataContext as Turn);
        }
    }
}
