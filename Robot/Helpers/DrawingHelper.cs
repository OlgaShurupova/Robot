using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Robot
{
    class DrawingHelper
    {
        /// <summary>
        /// Получение прямоугольника для рисования робота
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Rectangle GetRobotRectangle(Rectangle rectangle, int x, int y, MainCharacter robot)
        {
            var rotateTransform = new RotateTransform() { CenterX = 0.5, CenterY = 0.5, Angle = (int)robot.Direction };
            var imageBrush = GetImageBrush(rotateTransform);

            rectangle.StrokeThickness = 5;
            rectangle.Stroke = Brushes.Red;
            rectangle.Fill = imageBrush;

            return rectangle;
        }

        /// <summary>
        /// Получение кисти для рисования робота
        /// </summary>
        /// <param name="rotateTransform"></param>
        /// <returns></returns>
        private ImageBrush GetImageBrush(RotateTransform rotateTransform)
        {           
            var source = Properties.Resources.walle;

            var imageSource= System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap( 
                source.GetHbitmap(),
                IntPtr.Zero,
                System.Windows.Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            var imageBrush = new ImageBrush() { ImageSource = imageSource, RelativeTransform = rotateTransform };      

            return imageBrush;
        }

        /// <summary>
        /// Задание параметров новой линии на гриде
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public Line GetLine(double x1, double y1, double x2, double y2)
        {
            var line = new Line()
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
            };
            line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            return line;
        }

        /// <summary>
        /// Получение прямоугольников (клеток поля)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="lengthX"></param>
        /// <param name="lengthY"></param>
        /// <param name="color"></param>
        /// <param name="robot"></param>
        /// <returns></returns>
        public Rectangle GetRect(int x, int y, int lengthX, int lengthY, Color color)
        {
            var colorBrush = new SolidColorBrush(color);
            var rectangle = new Rectangle()
            {
                Width = lengthX,
                Height = lengthY,
                Fill = colorBrush
            };

            Canvas.SetLeft(rectangle, x * lengthX);
            Canvas.SetTop(rectangle, y * lengthY);
            return rectangle;
        }
    }
}
