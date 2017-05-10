using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Validation.Interface;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            UserCodes = new List<UserCode>();
        }
        
        public string Name {get; set;}
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<UserCode> UserCodes {get; set;}

        [NotMapped]
        public Validation.ValidationResult ResultValidation { get; private set; }

        public bool IsValid(IValidation<User> validation)
        {
            ResultValidation = validation.Validate(this);
            return ResultValidation.IsValid;
        }
    }
}