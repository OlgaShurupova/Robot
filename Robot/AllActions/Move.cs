using System;
using FluentValidation;

namespace Robot
{

    public class Move : AbstractAction
    {
        /// <summary>
        /// Число клеток, на которое перейдет робот
        /// </summary>
        public int CellAmount { get; set; } = 1;

        #region Валидация

        public Move()
        {
            Validator = new MoveValidator();           
        }
               
        public class MoveValidator: AbstractValidator<Move>
        {
            public MoveValidator()
            {              
                RuleFor(customer => customer.CellAmount)
                    .GreaterThan(0)
                    .WithMessage("введите положительное отличное от нуля число");
            }
        }

        #endregion

        /// <summary>
        /// Выполнение текущего действия
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="algorithm"></param>
        /// <param name="field"></param>
        public override void Execute(MainCharacter robot, Algorithm algorithm, Field field)
        {
            var move = robot.CurrentAction as Move;
            switch (robot.Direction)
            {
                case Side.Right:
                    robot.WriteNewRobotPosition(move.CellAmount, 0);
                    break;
                case Side.Left:
                    robot.WriteNewRobotPosition(-move.CellAmount, 0);
                    break;
                case Side.Up:
                    robot.WriteNewRobotPosition(0, -move.CellAmount);
                    break;
                case Side.Down:
                    robot.WriteNewRobotPosition(0, move.CellAmount);
                    break;
            }
        }
    }
}
