using FluentValidation;
using System.ComponentModel;

namespace Robot
{
    public abstract class AbstractAction : ValidatingBase, IDataErrorInfo
    {
        #region Свойства

        /// <summary>
        /// Текущее действие
        /// </summary>   
        public ActionHelper CurrentAction { get; set; } = new ActionHelper(AllActions.Move, 1);

        /// <summary>
        /// Следующее действие
        /// </summary>
        public ActionHelper NextAction { get; set; } = new ActionHelper(AllActions.Move, 1);       

        #endregion

        #region Methods

        /// <summary>
        /// Выполнение текущего действия
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="algorithm"></param>
        /// <param name="field"></param>
        public abstract void Execute(MainCharacter robot, Algorithm algorithm, Field field);

        #endregion

        #region Валидация

        public AbstractAction()
        {
            Validator = new AbstractActionValidator();
        }

        public class AbstractActionValidator : AbstractValidator<AbstractAction>
        {
            public AbstractActionValidator()
            {
                RuleFor(customer => customer.CurrentAction.Number)
                    .GreaterThan(0)
                    .WithMessage("введите положительное отличное от нуля число");

                RuleFor(customer => customer.NextAction.Number)
                    .GreaterThan(0)
                    .WithMessage("введите положительное отличное от нуля число");
            }
        }

        #endregion  
    }
}
