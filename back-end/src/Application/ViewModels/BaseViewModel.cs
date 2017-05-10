using System;

namespace Application.ViewModels
{
    public class BaseViewModel
    {
        public BaseViewModel()
        {
            Id = Guid.NewGuid();
            RegistrationDate = DateTime.Now;
            Active = true;
        }

        public Guid Id { get; set; }
        public bool Active { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
