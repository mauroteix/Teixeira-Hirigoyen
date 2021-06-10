using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Msp.BetterCalm.HandleMessage;
using MSP.BetterCalm.API.Filters;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.HandleMessage;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSP.BetterCalm.API.Controllers
{
    [SwaggerTag("Administrator")]
    [Route("api/administrator")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        IAdministratorLogic adminLogic;
        public AdministratorController(IAdministratorLogic _adminLogic)
        {
            adminLogic = _adminLogic;
        }

        /// <summary>
        /// Add a administrator
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        /// <response code="200">OK. Returns the requested object.</response>
        /// <response code="401">Unauthorized. You do not have permissions to perform this action.</response>
        /// <response code="422">UnprocessableEntity. Error in the semantics.</response>
        /// <response code="501">InternalServerError. The server could not handle an exception in the system.</response>
        [ServiceFilter(typeof(AuthorizationFilter))]
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
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        /// <summary>
        ///  Delete a administrator by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">OK. Returns the requested object.</response>
        /// <response code="401">Unauthorized. You do not have permissions to perform this action.</response>
        /// <response code="404">NotFound. The requested object was not found.</response>
        /// <response code="501">InternalServerError. The server could not handle an exception in the system.</response>
        [ServiceFilter(typeof(AuthorizationFilter))]
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
                catch (EntityNotExists fe)
                {
                    return NotFound(fe.MessageError());
                }
                catch (Exception e)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
                }
            }
        }

        /// <summary>
        /// Update a administrator by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newAdmin"></param>
        /// <returns></returns>
        /// <response code="200">OK. Returns the requested object.</response>
        /// <response code="401">Unauthorized. You do not have permissions to perform this action.</response>
        /// <response code="404">NotFound. The requested object was not found.</response>
        /// <response code="422">UnprocessableEntity. Error in the semantics.</response>
        /// <response code="501">InternalServerError. The server could not handle an exception in the system.</response>
        [ServiceFilter(typeof(AuthorizationFilter))]
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
            catch (EntityNotExists fe)
            {
                return NotFound(fe.MessageError());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpGet()]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(adminLogic.GetAll());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Administrator admin = adminLogic.Get(id);
                return Ok(admin);
            }
            catch (EntityNotExists fe)
            {
                return NotFound(fe.MessageError());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
