using Domain.Specification.Interface;

namespace Domain.Specification.User
{
    public class UserSpecification
    {
        public class ValidIfUserExists : ISpecification<Entities.User>
        {
            private readonly Entities.User _user;
            private readonly bool _isUpdate;

            public ValidIfUserExists(Entities.User user = null, bool isUpdate = false)
            {
                _user = user;
                _isUpdate = isUpdate;
            }

            public bool IsSatisfiedBy(Entities.User user)
            {
                if (_isUpdate)
                {
                    if (user != null && _user != null)
                    {
                        if (!string.IsNullOrEmpty(user.Email) && !string.IsNullOrEmpty(_user.Email))
                            if (user.Email == _user.Email)
                                return true;
                    }
                }

                return _user == null;
            }
        }
    }
}
