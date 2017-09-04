using System.Windows;
using System.Windows.Controls;

namespace Robot
{
    /// <summary>
    /// Логика взаимодействия для BlockFilling.xaml
    /// </summary>
    public partial class BlockFilling : UserControl
    {
        private readonly AlgorithmWindow _algorithmWindow;

        private bool _isEdit { get; set; }
        public BlockFilling(AlgorithmWindow algorithmWindow)
        {
            InitializeComponent();
            _algorithmWindow = algorithmWindow;
            _isEdit = false;
        }

        public BlockFilling(AbstractAction action, AlgorithmWindow algorithmWindow)
        {
            InitializeComponent();
            _algorithmWindow = algorithmWindow;
            _isEdit = true;
        }

        private void AddCommand(object sender, RoutedEventArgs e)
        {
            _algorithmWindow.AddAction(DataContext as Fill, _isEdit);
        }

        private void RemoveCommand(object sender, RoutedEventArgs e)
        {
            _algorithmWindow.RemoveAction(DataContext as Fill);
        }
    }
}
