using System;
using FluentValidation;
using System.Collections.Generic;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace Robot
{
    public class Study : AbstractAction
    {
              
        /// <summary>
        /// Cловарь, устанавливающий соответствие "цвет-действие"
        /// </summary>
        public ObservablePairCollection<Color, ActionHelper> DictionaryActionForColor = new ObservablePairCollection<Color, ActionHelper>();

        /// <summary>
        /// Выполнение текущего действия
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="algorithm"></param>
        /// <param name="field"></param>
        public override void Execute(MainCharacter robot, Algorithm algorithm, Field field)
        {
            var study = robot.CurrentAction as Study; 
            var color = field.CellsColorArray[(int)robot.Position.X, (int)robot.Position.Y];
            var isColor=false;
            foreach (var t in DictionaryActionForColor)
            {
                if (t.Key == color)
                {
                    NextAction.Type = t.Value.Type;
                    NextAction.Number = t.Value.Number;
                    isColor = true;
                }
            }
            if (!isColor) throw new Exception("элемент отсутствует в словаре");
        }
    }
}
