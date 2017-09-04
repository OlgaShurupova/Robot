using System;
using FluentValidation;
using System.Windows.Media;
using System.ComponentModel;

namespace Robot
{
    public class Fill : AbstractAction
    {        
        /// <summary>
        /// Цвет текущей ячейки
        /// </summary>
        public Color Color { get; set; }= Color.FromRgb(0, 0, 0);

        /// <summary>
        /// Выполнение текущего действия
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="algorithm"></param>
        /// <param name="field"></param>
        public override void Execute(MainCharacter robot, Algorithm algorithm, Field field)
        {
            var fill = robot.CurrentAction as Fill;
            var algorithmWork = new AlgorithmWork();
        
            for (var x = 0; x < algorithm.FieldSize.Width; x++)
                for (var y = 0; y < algorithm.FieldSize.Height; y++)
                    if (robot.Position.X == x && robot.Position.Y == y)
                        field.CellsColorArray[x, y] = fill.Color;
        }

    }
}
