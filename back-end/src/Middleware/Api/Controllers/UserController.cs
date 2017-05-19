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
            try
            {
               List<User> users = _userApplication.GetAll().ToList();

               return Ok(ResultDataSuccess<List<UserViewModel>>.Ok(AutoMapperExtensionMethods<UserViewModel>.Map(users)));
            }
            catch (Exception exception)
            {
                return Ok(ResultData.Error(exception));
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]UserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = AutoMapperExtensionMethods<User>.Map<UserViewModel>(model);
                    user.Active = true;

                    UserCode userCode = new UserCode();

                    userCode.Code = Guid.NewGuid();
                    userCode.Active = true;
                    
                    user.UserCodes.Add(userCode);

                    ValidationAppResult validationAppResult = _userApplication.Add(user);

                    if (validationAppResult.IsValid)
                        return Ok("Dados cadastrado com sucesso.");

                    return Ok(validationAppResult.Erros);
                }

                return Ok("Erro: Não foi possivel gravar o registro.");
            }
            catch (Exception exception)
            {
                return Ok(exception.Message);
            }
        }

        [Route("{id}"), HttpPut]
        public IActionResult Put(Guid id, [FromBody]UserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
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

                    return Ok("Não foi possivel atualizar o registro."); 
                }

                return Ok("Erro: Não foi possivel atualizar o registro.");
            }
            catch (Exception exception)
            {
                return Ok(exception.Message);
            }
        }

         [Route("{id}"), HttpDelete]
        public IActionResult Delete(Guid id)
        {
            try
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
            catch (Exception exception)
            {
                return Ok(exception.Message);
            }
        }
    }
}
