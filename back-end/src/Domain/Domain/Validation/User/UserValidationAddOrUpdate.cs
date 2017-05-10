using Domain.Specification.User;
using Domain.Validation.Interface;

namespace Domain.Validation.User
{
    public sealed class UserValidationAddOrUpdate : Validation<Entities.User>, IValidation<Entities.User>
    {
        public UserValidationAddOrUpdate(Entities.User user, bool isUpdate = false)
        {
            UserSpecification.ValidIfUserExists userExists = new UserSpecification.ValidIfUserExists(user, isUpdate);

            AddRule("ValidIfUserExists", new Rule<Entities.User>(userExists, string.Format("Usuário com o e-mail {0} já cadastrado em nosso sistema.", user != null ? user.Email : string.Empty)));
        }
    }
}
