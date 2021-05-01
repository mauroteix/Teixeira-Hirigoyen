﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Msp.BetterCalm.HandleMessage;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.HandleMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSP.BetterCalm.API.Controllers
{
    [Route("api/administrator")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        IAdministratorLogic adminLogic;
        public AdministratorController(IAdministratorLogic _adminLogic)
        {
            adminLogic = _adminLogic;
        }

        [HttpPost()]
        public IActionResult Add([FromBody] Administrator admin)
        {
            try
            {
                adminLogic.Add(admin);
                return Ok("Successfully added admin name:" + admin.Name);
            }
            catch (FieldEnteredNotCorrect fe)
            {
                return UnprocessableEntity(fe.MessageError());
            }
            catch (EntityNotExists fe)
            {
                return UnprocessableEntity(fe.MessageError());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id < 0)
            {
                return NotFound("Id not valid");
            }
            else
            {
                try
                {
                    Administrator admin = adminLogic.Get(id);
                    adminLogic.Delete(admin);
                    return Ok("Erased successfully");
                }
                catch (Exception e)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
                }
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Administrator newAdmin)
        {
            try
            {
                adminLogic.Update(newAdmin, id);
                return Ok("Updated successfully");
            }
            catch (FieldEnteredNotCorrect en)
            {
                return UnprocessableEntity(en.MessageError());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
