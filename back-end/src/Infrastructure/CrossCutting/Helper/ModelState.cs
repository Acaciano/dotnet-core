using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Infrastructure.CrossCutting.Helper
{
    public class ModelStateHelper
    {
        public static string GetErros(ModelStateDictionary modelState)
        {
            string errors = string.Empty;

            foreach (var state in modelState.Values)
            {
                foreach (var error in state.Errors.Where(error => !string.IsNullOrWhiteSpace(error.ErrorMessage)))
                {
                    errors += error.ErrorMessage + "<br/>";
                }
            }

            return errors;
        }
    }
}