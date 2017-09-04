using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot
{
    public class ValidatingBase: NotificatingBase
    {
        protected IValidator Validator;

        public string this[string columnName]
        {
            get
            {
                if (Validator == null) throw new InvalidOperationException($"{nameof(Validator)} is null");

                var validationResult = Validator.Validate(this);
                var propertyError = validationResult.Errors.FirstOrDefault(x => x.PropertyName == columnName);
                return propertyError != null ? propertyError.ErrorMessage : string.Empty;
            }
        }
        public string Error
        {
            get
            {
                if (Validator == null) throw new InvalidOperationException($"{nameof(Validator)} is null");

                var results = Validator.Validate(this);
                if (results.Errors.Any())
                {
                    return string.Join(Environment.NewLine, results.Errors.Select(x => x.ErrorMessage).ToArray());
                }
                return string.Empty;
            }
        }

        internal bool GetValidationResult()
        {
            if (Validator == null) throw new InvalidOperationException($"{nameof(Validator)} is null");

            var results = Validator.Validate(this);
            return results.IsValid;
        }
    }
}
