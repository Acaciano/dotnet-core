using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Interfaces.UnitOfWork;
using Domain.Service.Services;
using Domain.Validation;
using Domain.Validation.User;
using Infrastructure.CrossCutting.Encryption;

namespace Domain.Service.Services
{
    public class UserService : ServiceBase<User> , IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
            : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public User Authenticate(string email, string password)
        {
            return _userRepository.Authenticate(email, password);
        }

        public User GetByEmail(string email)
        {
            return _userRepository.GetByEmail(email);
        }

        public new ValidationResult Add(User user)
        {
            try
            {
                ValidationResult validationResult = new ValidationResult();

                if (user != null)
                {
                    User getByEmail = _userRepository.GetByEmail(user.Email);

                    if (user.IsValid(new UserValidationAddOrUpdate(getByEmail,false)))
                    {
                        user.Password = AdvancedEncryptionStandard.GetSha1Hash(user.Password);
                        user.Active = true;

                        UserCode userCode = new UserCode();
                        userCode.Code = Guid.NewGuid();
                        userCode.Active = true;
                        
                        user.UserCodes.Add(userCode);

                        _userRepository.Add(user);
                        return validationResult;
                    }

                    validationResult = user.ResultValidation;
                    return validationResult;
                }

                validationResult.AddError("Ocorreu um erro, dados do usúario inválidos.");
                return validationResult;
            }
            catch (Exception exception)
            {
                ValidationResult validationResult = new ValidationResult();
                validationResult.AddError(string.Format("Ocorreu um erro, Detalhes do erro: {0}",exception.Message));

                return validationResult;
            }
        }
    }
}
