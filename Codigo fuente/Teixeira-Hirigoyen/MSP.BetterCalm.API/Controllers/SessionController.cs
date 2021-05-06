using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Msp.BetterCalm.HandleMessage;
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
    [SwaggerTag("Session")]
    [Route("api/session")]
    [ApiController]
    public class SessionController : ControllerBase
    {

        private readonly ISessionLogic sessionLogic;

        public SessionController(ISessionLogic sessionLogic)
        {
            this.sessionLogic = sessionLogic;
        }

        /// <summary>
        /// Login a administrator
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        /// <response code="200">OK. Returns the requested object.</response>
        /// <response code="404">NotFound. The requested object was not found.</response>
        /// <response code="422">UnprocessableEntity. Error in the semantics.</response>
        /// <response code="501">InternalServerError. The server could not handle an exception in the system.</response>
        [HttpPost]
        public IActionResult Login([FromBody] Administrator admin)
        {
            try
            {
                var token = this.sessionLogic.Login(admin);
                return Ok(token);
            }
            catch (FieldEnteredNotCorrect e)
            {
                return UnprocessableEntity(e.MessageError());
            }
            catch (EntityNotExists e)
            {
                return NotFound(e.MessageError());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
