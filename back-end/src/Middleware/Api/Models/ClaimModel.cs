using System;
using System.Collections.Generic;
using System.Security.Claims;
using Domain.Entities;

namespace Middleware.Api.Models
{
    public class ClaimModel
    {
        public ClaimModel(User user)
        {
            this.IdUser = user.Id;
            this.Name = user.Name;
        }

        public Guid IdUser {get; set;}
        public string Name {get; set;}

        private List<Claim> Claims { get; set; }
        
        public List<Claim> Get()
        {
            Claims = new List<Claim>
            {
                new Claim(nameof(this.IdUser), this.IdUser.ToString()) { },
                new Claim(nameof(this.Name), this.Name) { }
            };
            
            return Claims;
        }
    }
}