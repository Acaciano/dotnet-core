using System.Collections.Generic;
using Domain.Validation.Interface;

namespace Domain.Validation.Base
{
    public abstract class FiscalBase<TEntity> : IFiscal<TEntity> where TEntity : class
    {
        private readonly Dictionary<string, IRule<TEntity>> _validations = new Dictionary<string, IRule<TEntity>>();

        protected virtual void AddRule(string nameRule, IRule<TEntity> rule)
        {
            _validations.Add(nameRule, rule);
        }

        protected virtual void RemoveRule(string nomeRegra)
        {
            _validations.Remove(nomeRegra);
        }

        public virtual ValidationResult Validate(TEntity entity)
        {
            var result = new ValidationResult();
            foreach (var x in _validations.Keys)
            {
                var rule = _validations[x];
                if (!rule.Validate(entity))
                    result.AddError(new ValidationError(rule.ErrorMessage));
            }

            return result;
        }

        protected IRule<TEntity> GetRule(string nameRule)
        {
            IRule<TEntity> rule;
            _validations.TryGetValue(nameRule, out rule);
            return rule;
        }
    }
}
