using Application.Validation;
using Domain.Entities;

namespace Application.Interface.Application
{
    public interface IUserApplication : IApplicationBase<User>
    {
        User Authenticate(string email, string password);
        User GetByEmail(string email);
        new ValidationAppResult Add(User user);
    }
}