using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;


namespace Robot
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Algorithm _algorithm = new Algorithm();
        private MainCharacter _robot = new MainCharacter(new Algorithm());
        private System.Timers.Timer _timer;
        private AlgorithmWork _algorithmWork = new AlgorithmWork();
        private Field _field;
        public MainWindow()
        {
            InitializeComponent();
            _timer = new System.Timers.Timer();
            _timer.Elapsed += _timer_Elapsed;
        }

        /// <summary>
        /// Создание нового алгоритма
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewAlgorithmClick(object sender, RoutedEventArgs e)
        {
            var algorithmWindow = new AlgorithmWindow(new Algorithm());
            algorithmWindow.Show();
        }

        #region Открытие и обновление

        /// <summary>
        /// Открытие алгоритма
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenAlgorithmClick(object sender, RoutedEventArgs e)
        {
            var service = new Service();            
            var dialog = new Microsoft.Win32.OpenFileDialog();

            if (Directory.Exists(Path.Combine("Data")))
            {
                dialog.InitialDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Data");

                if (dialog.ShowDialog() == true)
                {
                    _algorithm = service.ReadAlgorithm(dialog.FileName);
                    RefreshData();
                    DrawNewField();
                }
            }
            else MessageBox.Show("Ещё не создано ни одного алгоритма", "Ошибка");
        }

        /// <summary>
        /// Обновление данных при открытии нового алгоритма
        /// </summary>
        /// <param name="fileName"></param>
        private void RefreshData()
        {
            _robot = new MainCharacter(_algorithm);
            DataContext = _robot;
            _algorithmWork = new AlgorithmWork();
            _field = new Field((int)_algorithm.FieldSize.Width + 1, (int)_algorithm.FieldSize.Height + 1);
            _field.InitializeField(_algorithm.BorderColor, _algorithm.DefaultColor);
            ActionsList.ItemsSource = _algorithm.ActionList;
        }
        #endregion

        #region Выполнение алгоритма
        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                _robot.CurrentAction = _algorithmWork.ExecuteAction(_robot, _algorithm, _field);
            }
            catch
            {
                _timer.Stop();
                MessageBox.Show("Роботу встретился незнакомый цвет", "Ошибка");
            }
            if (_algorithmWork.CheckPosition(_algorithm, _robot)) UpdateCanvas();
            else
            {
                _timer.Stop();
                MessageBox.Show("Роботу запрещено покидать свой маленьки мир(", "Попытка выхода за пределы поля");
            }
        }

        /// <summary>
        /// Запуск алгоритма
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartClick(object sender, RoutedEventArgs e)
        {
            int interval;

            if (int.TryParse(Interval.Text, out interval))
                if (interval > 0 && interval < 15) _robot.Interval = interval * 1000;

            _timer.Interval = _robot.Interval;

            var actionList = _algorithm.ActionList;
            _robot.CurrentAction = actionList.First();
            ActionsList.SelectedIndex = actionList.IndexOf(_robot.CurrentAction);
            if (_robot.CurrentAction != null) _timer.Start();
        }

        /// <summary>
        /// Остановка выполнения алгоритма
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopClick(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
        }

        /// <summary>
        /// Выполнение алгоритма по шагам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StepClick(object sender, RoutedEventArgs e)
        {
            if (_robot.CurrentAction == null) _robot.CurrentAction = _algorithm.ActionList.FirstOrDefault();
            try
            {
                _robot.CurrentAction = _algorithmWork.ExecuteAction(_robot, _algorithm, _field);
            }
            catch
            {
                MessageBox.Show("Роботу встретился незнакомый цвет", "Ошибка");
            }
            ActionsList.Focus();
            ActionsList.SelectedIndex = _algorithm.ActionList.IndexOf(_robot.CurrentAction);
            if (_algorithmWork.CheckPosition(_algorithm, _robot))
            {
                UpdateBackPattern();
                DrawField();
            }
            else MessageBox.Show("Роботу запрещено покидать свой маленьки мир(", "Попытка выхода за пределы поля");
        }
        #endregion

        #region Отрисовка

        /// <summary>
        /// Перериовка холста
        /// </summary>
        private void UpdateCanvas()
        {
            Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate ()
            {
                ActionsList.Focus();
                ActionsList.SelectedIndex = _algorithm.ActionList.IndexOf(_robot.CurrentAction);
                UpdateBackPattern();
                DrawField();
            }));
        }

        /// <summary>
        /// Обновление сетки и робота при изменении размеров окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void WindowChangeHandler(object sender, SizeChangedEventArgs e)
        {
            if (_algorithm.FieldSize.Width != 0)
            {
                UpdateBackPattern();
                DrawField();
            }
        }

        /// <summary>
        /// Отриосвка сетки
        /// </summary>
        private void UpdateBackPattern()
        {
            AlgorithmColumn.Background = new SolidColorBrush(Color.FromRgb(240, 237, 253));

            var lengthX = (int)(Background.ActualWidth / _algorithm.FieldSize.Width);
            var lengthY = (int)(Background.ActualHeight / _algorithm.FieldSize.Height);

            var canvasWidth = lengthX * (int)_algorithm.FieldSize.Width;
            var canvasHeight = lengthY * (int)_algorithm.FieldSize.Height;

            Background.Children.Clear();
            AddLines(canvasWidth, canvasHeight, lengthX, lengthY);
        }

        /// <summary>
        /// Добавленеие линий сетки
        /// </summary>
        /// <param name="canvasWidth"></param>
        /// <param name="canvasHeight"></param>
        /// <param name="lengthX"></param>
        /// <param name="lengthY"></param>
        private void AddLines(int canvasWidth, int canvasHeight, int lengthX, int lengthY)
        {
            var drawingHelper = new DrawingHelper();

            for (var x = 0; x < canvasWidth; x += lengthX)
            {
                var line = drawingHelper.GetLine(x, 0, x, canvasHeight);
                Background.Children.Add(line);
            }
            for (var y = 0; y < canvasHeight; y += lengthY)
            {
                var line = drawingHelper.GetLine(0, y, canvasWidth, y);
                Background.Children.Add(line);
            }
        }

        /// <summary>
        /// Отрисовка границ поля
        /// </summary>
        private void DrawField()
        {
            for (var x = 0; x < _algorithm.FieldSize.Width; x++)
                for (var y = 0; y < _algorithm.FieldSize.Height; y++)
                    DrawRect(x, y, _field.CellsColorArray[x, y]);
        }

        /// <summary>
        /// Отрисовка прямоуголльников на поле
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void DrawRect(int x, int y, Color color)
        {
            var dravingHelper = new DrawingHelper();
            var lengthX = (int)(Background.ActualWidth / _algorithm.FieldSize.Width);
            var lengthY = (int)(Background.ActualHeight / _algorithm.FieldSize.Height);
            var rectangle = dravingHelper.GetRect(x, y, lengthX, lengthY, color);
            if (_robot.Position.X == x && _robot.Position.Y == y) rectangle = dravingHelper.GetRobotRectangle(rectangle, x, y, _robot);

            NewCanvas.Children.Add(rectangle);
        }

        /// <summary>
        /// Начальная отрисовка поля
        /// </summary>
        private void DrawNewField()
        {
            var position = _robot.Position;

            position.X++;
            position.Y++;
            _robot.WriteNewRobotPosition((int)position.X, (int)position.Y);

            UpdateBackPattern();
            DrawField();
        }

        #endregion

        private void ChangeRobotPosition(object sender, RoutedEventArgs e)
        {
            var robotCoordinatesWindow = new RobotCoordinatesWindow(_robot);

            if (robotCoordinatesWindow.ShowDialog() == true)
            {
                if (robotCoordinatesWindow.DialogResult == true) _robot = robotCoordinatesWindow.DataContext as MainCharacter;
                UpdateBackPattern();
                DrawField();
            }
        }
    }
}
