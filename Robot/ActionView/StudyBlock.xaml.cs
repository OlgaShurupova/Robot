using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static Robot.Study;

namespace Robot
{
    /// <summary>
    /// Логика взаимодействия для StudyBlock.xaml
    /// </summary>
    public partial class StudyBlock : UserControl
    {
        private readonly AlgorithmWindow _algorithmWindow;
        private Study _study = new Study();

        private bool _isEdit { get; set; }
        public StudyBlock(AlgorithmWindow algorithmWindow)
        {
            InitializeComponent();
            _algorithmWindow = algorithmWindow;
            _isEdit = false;
            _study.CurrentAction.Type = AllActions.Study;
            ActionsList.ItemsSource = _study.DictionaryActionForColor;
        }

        public StudyBlock(AbstractAction action, AlgorithmWindow algorithmWindow)
        {
            InitializeComponent();
            _algorithmWindow = algorithmWindow;
            _isEdit = true;
            _study.CurrentAction.Type = AllActions.Study;
            ActionsList.ItemsSource = _study.DictionaryActionForColor;
        }

        private void AddCommand(object sender, RoutedEventArgs e)
        {
            _algorithmWindow.AddAction(_study, _isEdit);
        }

        private void RemoveCommand(object sender, RoutedEventArgs e)
        {
            _algorithmWindow.RemoveAction(DataContext as Study);
        }

        private void AddActionForColorClick(object sender, RoutedEventArgs e)
        {
            var color = (Color)Color.SelectedColor;
            var type = (AllActions)ActionType.SelectedItem;
            var number = int.Parse(ActionNumber.Text);

            _study.DictionaryActionForColor.Add(color, new ActionHelper(type, number));
        }
    }
}
