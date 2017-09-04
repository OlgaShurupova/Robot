using System;
using FluentValidation;

namespace Robot
{
    public class Turn :AbstractAction
    {    
        /// <summary>
        /// Направление поворота робота
        /// </summary>
        public Directions Direction { get; set; }

        public override void Execute(MainCharacter robot, Algorithm algorithm, Field field)
        {
            var turn = robot.CurrentAction as Turn;

            switch (turn.Direction)
            {
                case Directions.Right:
                    if (robot.Direction == Side.Left) robot.Direction = Side.Up;
                    else robot.Direction+=90;                 
                    break;
                case Directions.Left:
                    if (robot.Direction == Side.Up) robot.Direction = Side.Left;
                    else robot.Direction-=90;                
                    break;
            }
        }
    }
}
