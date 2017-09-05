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
            SetDefaultValues();       
            ActionsList.ItemsSource = _study.DictionaryActionForColor;
        }

        public StudyBlock(AbstractAction action, AlgorithmWindow algorithmWindow)
        {
            InitializeComponent();
            _study=action as Study;
            _algorithmWindow = algorithmWindow;
            _isEdit = true;
            SetDefaultValues();
            ActionsList.ItemsSource = _study.DictionaryActionForColor;
        }

        private void SetDefaultValues()
        {
            NewColor.SelectedColor = Color.FromRgb(0, 0, 0);
            ActionType.SelectedItem = AllActions.Move;
            ActionNumber.Text = "1";
            _study.CurrentAction.Type = AllActions.Study;
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
            var color = (Color)NewColor.SelectedColor;
            if (CheckColor(color))
            {
                var type = (AllActions)ActionType.SelectedItem;
                var number = int.Parse(ActionNumber.Text);
                _study.DictionaryActionForColor.Add(color, new ActionHelper(type, number));
            }
            
        }

        /// <summary>
        /// Проверяет, содержится ли выбранный цвет в словаре
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private bool CheckColor(Color color)
        {
            foreach(var currentColor in _study.DictionaryActionForColor)
            {
                if (currentColor.Key == color) return false;
            }
            return true;
        }
    }
}
