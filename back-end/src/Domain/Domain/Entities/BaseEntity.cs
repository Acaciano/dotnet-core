using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            RegistrationDate = DateTime.Now;
        }
        
        public Guid Id { get; set; }
        public bool Active { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? DateModified { get; set; }
    }
}