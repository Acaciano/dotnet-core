using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Validation.Interface;

namespace Domain.Entities
{
    public class UserCode : BaseEntity
    {
        [Column(Order = 1)]
        public Guid Code {get; set;}
        
        [Column(Order = 2)]
        public Guid UserId {get; set;}

        public virtual User User {get; set;}

        [NotMapped]
        public Validation.ValidationResult ResultValidation { get; private set; }

        public bool IsValid(IValidation<UserCode> validation)
        {
            ResultValidation = validation.Validate(this);
            return ResultValidation.IsValid;
        }
    }
}