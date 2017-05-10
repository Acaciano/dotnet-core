using System.Collections.Generic;
using System.Linq;

namespace Domain.Validation
{
    public class ValidationResult
    {
        private readonly List<ValidationError> _errors = new List<ValidationError>();

        public string Message { get; set; }
        public bool IsValid { get { return _errors.Count == 0; } }

        public List<ValidationError> Erros { get { return this._errors; } }
        public void AddError(ValidationError error)
        {
            _errors.Add(error);
        }
        public void RemoverErro(ValidationError error)
        {
            if (_errors.Contains(error))
                _errors.Remove(error);
        }

        public void AddError(params ValidationResult[] validationResults)
        {
            if (validationResults == null) return;

            foreach (var validationResult in validationResults.Where(result => result != null))
                this._errors.AddRange(validationResult.Erros);
        }

        public void AddError(string message)
        {
            if(string.IsNullOrEmpty(message)) return;

            ValidationError validationError = new ValidationError(message);

            this.Erros.Add(validationError);
        }

        public void AddError(string[] message)
        {
            if(message == null) return;

            foreach (ValidationError validationError in message.Select(item => new ValidationError(item)))
                this.Erros.Add(validationError);
        }
    }
}
