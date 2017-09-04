using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Robot
{
    /// <summary>
    /// Логика взаимодействия для RobotCoordinatesWindow.xaml
    /// </summary>
    public partial class RobotCoordinatesWindow : Window
    {
        private MainCharacter _robot;
        public RobotCoordinatesWindow(MainCharacter robot)
        {
            InitializeComponent();
            _robot = robot;
            DataContext = _robot;
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (_robot.GetValidationResult()) DialogResult = true;
        }
    }
}
