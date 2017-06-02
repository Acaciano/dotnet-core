using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository : RepositoryBase<User> , IUserRepository
    {
        public User Authenticate(string email, string password)
        {
            password = Infrastructure.CrossCutting.Encryption.AdvancedEncryptionStandard.GetSha1Hash(password);
            return DbSet.FirstOrDefault(user => user.Email == email && user.Password == password && user.Active);
        }

        public User GetByEmail(string email)
        {
            return DbSet.FirstOrDefault(user => user.Email == email && user.Active);
        }

        public new IEnumerable<User> GetAll()
        {
            return DbSet.Include(x => x.UserCodes).AsNoTracking();
        }
    }
}
