using System.ComponentModel;
using System.Runtime.CompilerServices;
using FluentValidation;
using System.Linq;
using System;
using System.Windows;

namespace Robot
{
    public class MainCharacter:ValidatingBase
    {
        #region Свойства

        private Point _position;
        /// <summary>
        /// Позиция робота
        /// </summary>
        public Point Position
        {
            get { return _position; }
            set { if (_position!=value) { _position = value; NotifyPropertyChanged(); } }
        }
      
        /// <summary>
        /// Последнее выполненное действие робота
        /// </summary>
        public AbstractAction CurrentAction { get; set; }

        /// <summary>
        /// Направление движения робота
        /// </summary>
        public Side Direction { get; set; } = Side.Right;

        /// <summary>
        /// Интервал смены действия робота в автоматическом режиме 
        /// </summary>
        public int Interval { get; set; } = 1000;
        #endregion

        #region Методы
        /// <summary>
        /// Запись нового положения робота на сетке
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void WriteNewRobotPosition(int x, int y)
        {    
            var position = Position;
            position.X += x;
            position.Y += y;
            Position = position;
        }
        #endregion

        #region Валидация 
       
        public MainCharacter(Algorithm algorithm)
        {
            Validator = new MainCharacterValidator(algorithm);
        }

        public class MainCharacterValidator : AbstractValidator<MainCharacter>
        {
            private Algorithm _algorithm;
            public MainCharacterValidator(Algorithm algorithm)
            {
                _algorithm = algorithm;
                RuleFor(mainCharacter => mainCharacter.Position)
                    .Must(CheckPosition)
                    .WithMessage("робот должен оставаться в пределах поля");
            }

            /// <summary>
            /// Проверка попадания робота в поле
            /// </summary>
            /// <param name="position"></param>
            /// <returns></returns>
            private bool CheckPosition(Point position)
            {
                if (position.X < 0 || position.X >= _algorithm.FieldSize.Width) return false;
                if (position.Y < 0 || position.Y >= _algorithm.FieldSize.Height) return false;
                return true;
            }
        }
        #endregion

    }
}
