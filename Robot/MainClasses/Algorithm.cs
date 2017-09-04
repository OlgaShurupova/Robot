using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Robot
{
    public class Algorithm : ValidatingBase, IDataErrorInfo 
    {
        #region Свойства
        /// <summary>
        /// Имя алгоритма
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Размер поля
        /// </summary>
        public Size FieldSize { get; set; }  

        /// <summary>
        /// Цвет ячеек по умолчанию
        /// </summary>
        public Color DefaultColor { get; set; } = Color.FromRgb(255, 255, 255);
      
        /// <summary>
        /// Цвет границы
        /// </summary>
        public Color BorderColor { get; set; } = Color.FromRgb(222, 168, 126);
             
        public List<AbstractAction> ActionList { get; set; }
        #endregion

        #region Валидация 

        public Algorithm()
        {
            Validator = new AlgorithmValidator();
        }

        public class AlgorithmValidator : AbstractValidator<Algorithm>
        {
            public AlgorithmValidator()
            {
                RuleFor(customer => customer.Name)
                    .NotEmpty()
                    .WithMessage("обязательное поле"); 
            }        
        }
        #endregion
    }
}