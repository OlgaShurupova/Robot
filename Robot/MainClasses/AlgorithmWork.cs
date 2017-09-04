using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace Robot
{
    class AlgorithmWork
    {    
        #region Положение робота на сетке
      
        /// <summary>
        /// Проверить позицию робота на предмет выхода за пределы поля
        /// </summary>
        /// <param name="algorithm"></param>
        public bool CheckPosition(Algorithm algorithm, MainCharacter robot)
        {
            if (robot.Position.X < 0 || robot.Position.X >= algorithm.FieldSize.Width) return false;
            if (robot.Position.Y < 0 || robot.Position.Y >= algorithm.FieldSize.Height) return false;
            return true;
        }

        #endregion

        #region Команды, исполняемые роботом

        /// <summary>
        /// ВЫбор следующего действия. 
        /// </summary>
        /// <param name="action"></param>
        public AbstractAction ExecuteAction(MainCharacter robot, Algorithm algorithm, Field field)
        {
            var currentAction = robot.CurrentAction;
           
            robot.CurrentAction.Execute(robot, algorithm, field);
            return GoToNextAction(currentAction.NextAction.Type, currentAction.NextAction.Number, algorithm);
        }

        /// <summary>
        /// Переход к следующей команде
        /// </summary>
        private AbstractAction GoToNextAction(AllActions nextActionType, int nextActionNumber, Algorithm algorithm)
        {
            var actionList = algorithm.ActionList;
            var nextAction = actionList.FirstOrDefault(x => x.CurrentAction.Type == nextActionType && x.CurrentAction.Number == nextActionNumber);
            return nextAction;          
        }
        #endregion
    }
}
