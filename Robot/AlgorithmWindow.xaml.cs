using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Robot
{
    /// <summary>
    /// Логика взаимодействия для AlgorithmWindow.xaml
    /// </summary>
    public partial class AlgorithmWindow : Window
    {
        private Algorithm _algorithm = new Algorithm();
        private Service _service = new Service();
        private ObservableCollection<AbstractAction> _actionsList = new ObservableCollection<AbstractAction>();

        private int _selectedIndex { get; set; }

        public AlgorithmWindow(Algorithm algorithm)
        {
            InitializeComponent();
            _algorithm = algorithm;
            ActionsList.ItemsSource = _actionsList;
            DataContext = _algorithm;
            SetFieldSize();
        }

        private void SetFieldSize()
        {
            var fieldSize = new Size();
            fieldSize.Width = 10;
            fieldSize.Height = 10;
            _algorithm.FieldSize = fieldSize;
        }

        private void SaveAlgorithm(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Сохранить алгоритм?", "Сохранение", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Save();
                    break;
                case MessageBoxResult.No:
                    Close();
                    break;
            }
        }

        private void Save()
        {
            var fieldSize = _algorithm.FieldSize;
            fieldSize.Width += 2;
            fieldSize.Height += 2;
            _algorithm.FieldSize = fieldSize;
            _algorithm.ActionList = new List<AbstractAction>(_actionsList);
            if (_algorithm.GetValidationResult())
            {
                try
                {
                    _service.WriteAlgorithm(_algorithm);
                    Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Алгоритм с таким именем уже существует", "Ошибка");
                }
            }
            else MessageBox.Show("Алгоритм не сохранен", "Ошибка");
        }

        /// <summary>
        /// Выбор соответсвующего шаблона при выборе в списке. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActionSelected(object sender, SelectionChangedEventArgs e)
        {
            var type = (AllActions)Type.SelectedItem;
            switch (type)
            {
                case AllActions.Filling:
                    var fill = new Fill();
                    fill.CurrentAction.Type = type;
                    ActionBlock.Content = new BlockFilling(this) { DataContext = fill };
                    break;
                case AllActions.Move:
                    var move = new Move();
                    move.CurrentAction.Type = type;
                    ActionBlock.Content = new MoveBlock(this) { DataContext = move };
                    break;
                case AllActions.Study:
                    var study = new Study();
                    study.CurrentAction.Type = type;
                    ActionBlock.Content = new StudyBlock(this) { DataContext = study };
                    break;
                case AllActions.Turn:
                    var turn = new Turn();
                    turn.CurrentAction.Type = type;
                    ActionBlock.Content = new BlockOfTurn(this) { DataContext = turn };
                    break;
            }
        }
        /// <summary>
        /// Добавление действия в алгоритм
        /// </summary>
        /// <param name="action"></param>
        public void AddAction(AbstractAction action, bool isEdit)
        {
            if (action.GetValidationResult())
            {
                if (isEdit) _actionsList[_selectedIndex] = action;
                else
                {
                    _actionsList.Add(action);
                }
            }
        }

        /// <summary>
        /// Удаление действия
        /// </summary>
        /// <param name="action"></param>
        public void RemoveAction(AbstractAction action)
        {
            _actionsList.Remove(action);
        }

        private void SelectActionType(object sender, MouseButtonEventArgs e)
        {
            var action = ActionsList.SelectedItem as AbstractAction;
            Type.SelectedItem = action.CurrentAction.Type;
            switch (action.CurrentAction.Type)
            {
                case AllActions.Filling:
                    ActionBlock.Content = new BlockFilling(action, this) { DataContext = action }; ;
                    break;
                case AllActions.Move:
                    ActionBlock.Content = new MoveBlock(action, this) { DataContext = action };
                    break;
                case AllActions.Study:
                    ActionBlock.Content = new StudyBlock(action, this) { DataContext = action };
                    break;
                case AllActions.Turn:
                    ActionBlock.Content = new BlockOfTurn(action, this) { DataContext = action };
                    break;
            }
        }
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
