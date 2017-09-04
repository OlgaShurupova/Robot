using System;
using System.Windows;
using System.Windows.Controls;

namespace Robot
{
    /// <summary>
    /// Логика взаимодействия для MoveBlock.xaml
    /// </summary>
    public partial class MoveBlock : UserControl
    {

        private readonly AlgorithmWindow _algorithmWindow;
        private bool _isEdit { get; set; }

        public MoveBlock(AlgorithmWindow algorithmWindow)
        {
            InitializeComponent();
            _algorithmWindow = algorithmWindow;
            _isEdit = false;
        }
        public MoveBlock(AbstractAction action, AlgorithmWindow algorithmWindow)
        {
            InitializeComponent();
            _algorithmWindow = algorithmWindow;
            _isEdit = true;
        }


        private void AddCommand(object sender, RoutedEventArgs e)
        {
            _algorithmWindow.AddAction(DataContext as Move, _isEdit);
        }


        private void RemoveCommand(object sender, RoutedEventArgs e)
        {
            _algorithmWindow.RemoveAction(DataContext as Move);
        }
    }
}
