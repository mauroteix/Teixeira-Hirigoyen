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

namespace MSP.BetterCalm.API.Controllers
{
    [SwaggerTag("User")]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserLogic _userLogic;
        public UserController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }
        /// <summary>
        /// Create a meeting for a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <response code="200">OK. Returns the requested object.</response>
        /// <response code="404">NotFound. The requested object was not found.</response>
        /// <response code="422">UnprocessableEntity. Error in the semantics.</response>
        /// <response code="500">InternalServerError. The server could not handle an exception in the system.</response>
        [HttpPost()]
        public IActionResult Add([FromBody] User user)
        {
            try
            {

                _userLogic.Add(user);
                List <Meeting> list = user.Meeting.ToList();
                Meeting lastMeeting = list[list.Count-1];
                return Ok("Successfully added user email:" + user.Email + ", " + "Costo de meeting:"+lastMeeting.TotalPrice);
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


        /// <summary>
        /// Update a user by id - discount
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <response code="200">OK. Returns the requested object.</response>
        /// <response code="401">Unauthorized. You do not have permissions to perform this action.</response>
        /// <response code="404">NotFound. The requested object was not found.</response>
        /// <response code="422">UnprocessableEntity. Error in the semantics.</response>
        /// <response code="500">InternalServerError. The server could not handle an exception in the system.</response>
        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] User user)
        {
            try
            {
                _userLogic.UpdateByAdministrator(user, id);
                return Ok("Updated successfully");
            }
            catch (FieldEnteredNotCorrect en)
            {
                return UnprocessableEntity(en.MessageError());
            }
            catch (EntityNotExists en)
            {
                return NotFound(en.MessageError());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Get all the users with more than 5 meeting
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK. Returns the requested object.</response>
        /// <response code="500">InternalServerError. The server could not handle an exception in the system.</response>
        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpGet()]
        public IActionResult GetUserbyCountMeeting()
        {
            try
            {
                return Ok(_userLogic.GetUserbyCountMeeting());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
