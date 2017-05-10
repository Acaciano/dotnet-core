using System;
using Application.Interface.Application;
using Application.Validation;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Validation;

namespace Application.Application
{
    public class UserApplication : ApplicationBase<User> , IUserApplication
    {
        private readonly IUserService _userService; 
        public UserApplication(IUserService userService) : base(userService)
        {
            _userService = userService;
        }

        public new ValidationAppResult Add(User user)
        {
            ValidationResult validationResult = _userService.Add(user);

            if (validationResult.IsValid)
            {
                Commit();
                return DomainToApplicationResult(validationResult);
            }

            Rollback();
            return DomainToApplicationResult(validationResult);
        }

        public User Authenticate(string accessCode, string password)
        {
            return _userService.Authenticate(accessCode,password);
        }

        public User GetByEmail(string email)
        {
            return _userService.GetByEmail(email);
        }
    }
}