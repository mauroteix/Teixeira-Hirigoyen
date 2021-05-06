using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Msp.BetterCalm.HandleMessage;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.HandleMessage;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Linq;

namespace MSP.BetterCalm.API.Controllers
{
    [SwaggerTag("User")]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserLogic _userLogic;
        IPsychologistLogic _psychologistLogic;
        public UserController(IUserLogic userLogic, IPsychologistLogic psychologistLogic)
        {
            _userLogic = userLogic;
            _psychologistLogic = psychologistLogic;
        }
        /// <summary>
        /// Create a meeting for a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <response code="200">OK. Returns the requested object.</response>
        /// <response code="404">NotFound. The requested object was not found.</response>
        /// <response code="422">UnprocessableEntity. Error in the semantics.</response>
        /// <response code="501">InternalServerError. The server could not handle an exception in the system.</response>
        [HttpPost()]
        public IActionResult Add([FromBody] User user)
        {
            try
            {

                _userLogic.Add(user);
                return Ok("Successfully added user name:" + user.Name);
            }
            catch (FieldEnteredNotCorrect fe)
            {
                return UnprocessableEntity(fe.MessageError());
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
