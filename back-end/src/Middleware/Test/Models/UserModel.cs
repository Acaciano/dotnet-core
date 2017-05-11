using System;

namespace Middleware.Test.Models
{
    public class UserModel
    {
        public UserModel()
        {
            Id = Guid.NewGuid();
            Active = true;
            RegistrationDate = DateTime.Now;
        }

        public Guid Id {get; set;}
        public string Name {get; set;}
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? DateModified { get; set; }
    }
}