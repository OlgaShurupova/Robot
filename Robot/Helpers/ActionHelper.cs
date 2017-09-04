using FluentValidation;
using System.ComponentModel;

namespace Robot
{
    public class ActionHelper: ValidatingBase, IDataErrorInfo
    {
        #region Свойства
        private AllActions _type;
        private int _number;

        /// <summary>
        /// Тип команды
        /// </summary>
        public AllActions Type
        {
            get { return _type; }
            set { if (_type != value) { _type = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Номер команды
        /// </summary>
        public int Number
        {
            get { return _number; }
            set { if (_number != value) { _number = value; NotifyPropertyChanged(); } }
        }
        #endregion

        public ActionHelper(AllActions type, int number)
        {
            _type = type;
            _number = number;
            Validator = new ActionHelperValidator();
        }

        #region Валидация        
        public class ActionHelperValidator : AbstractValidator<ActionHelper>
        {
            public ActionHelperValidator()
            {
                RuleFor(customer => customer.Number)
                    .GreaterThan(0)
                    .WithMessage("введите положительное отличное от нуля число");
            }                
        }        

        #endregion

    }
}
