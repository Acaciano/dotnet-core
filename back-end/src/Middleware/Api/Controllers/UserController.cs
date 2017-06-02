using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Interface.Application;
using Application.Validation;
using Application.ViewModels;
using Domain.Entities;
using Infrastructure.CrossCutting.ExtensionMethods;
using Infrastructure.CrossCutting.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Middleware.Api.Models;

namespace Middleware.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ApiBaseController
    {
        private readonly IUserApplication _userApplication;
        
        public UserController(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<User> users = _userApplication.GetAll().ToList();
            return Ok(ResultDataSuccess<List<UserViewModel>>.Ok(AutoMapperExtensionMethods<UserViewModel>.Map(users)));
        }

        [HttpPost]
        public IActionResult Post([FromBody]UserViewModel model)
        {
            User user = AutoMapperExtensionMethods<User>.Map<UserViewModel>(model);
            ValidationAppResult validationAppResult = _userApplication.Add(user);

            if (validationAppResult.IsValid)
                return Ok(ResultDataSuccess<string>.Ok("Dados cadastrado com sucesso."));
            
            return Ok(ResultDataError<List<ValidationAppError>>.Error(validationAppResult.Erros.ToList()));
        }

        [Route("{id}"), HttpPut]
        public IActionResult Put(Guid id, [FromBody]UserViewModel model)
        {
            User user = _userApplication.GetById(id);
                    
            if(user != null)
            {
                user.Name = model.Name;
                user.Email = model.Email;
                user.Password = model.Password;

                _userApplication.Update(user);
                _userApplication.Commit();

                return Ok("Dados atualizado com sucesso."); 
            }

            return Ok("Erro: Não foi possivel atualizar o registro.");
        }

        [Route("{id}"), HttpDelete]
        public IActionResult Delete(Guid id)
        {
            User user = _userApplication.GetById(id);
                
            if(user != null)
            {
                _userApplication.Remove(user);
                _userApplication.Commit();
                return Ok("User removido com sucesso.");
            }

            return Ok("Não foi possivel remover o user.");
        }
    }
}
