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
    [SwaggerTag("Psychologist")]
    [Route("api/psychologist")]
    [ApiController]
    public class PsychologistController : ControllerBase
    {
        IPsychologistLogic _psychologistLogic;
        public PsychologistController(IPsychologistLogic psychologistLogic)
        {
            _psychologistLogic = psychologistLogic;
        }

        /// <summary>
        /// Get all the psychologist
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK. Returns the requested object.</response>
        /// <response code="401">Unauthorized. You do not have permissions to perform this action.</response>
        /// <response code="500">InternalServerError. The server could not handle an exception in the system.</response>
        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpGet()]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_psychologistLogic.GetAll());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Get a psychologist by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">OK. Returns the requested object.</response>
        /// <response code="401">Unauthorized. You do not have permissions to perform this action.</response>
        /// <response code="404">NotFound. The requested object was not found.</response>
        /// <response code="500">InternalServerError. The server could not handle an exception in the system.</response>
        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Psychologist psychologist = _psychologistLogic.Get(id);
                return Ok(psychologist);
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
        /// Add a psychologist
        /// </summary>
        /// <param name="psychologist"></param>
        /// <returns></returns>
        /// <response code="200">OK. Returns the requested object.</response>
        /// <response code="401">Unauthorized. You do not have permissions to perform this action.</response>
        /// <response code="404">NotFound. The requested object was not found.</response>
        /// <response code="422">UnprocessableEntity. Error in the semantics.</response>
        /// <response code="500">InternalServerError. The server could not handle an exception in the system.</response>
        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpPost()]
        public IActionResult Add([FromBody] Psychologist psychologist)
        {
            try
            {
                _psychologistLogic.Add(psychologist);
                return Ok("Successfully added psychologist name:" + psychologist.Name);
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
        /// Delete a pyschologist by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">OK. Returns the requested object.</response>
        /// <response code="401">Unauthorized. You do not have permissions to perform this action.</response>
        /// <response code="404">NotFound. The requested object was not found.</response>
        /// <response code="500">InternalServerError. The server could not handle an exception in the system.</response>
        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpDelete("{id}")]
        public IActionResult DeletePsychologist(int id)
        {
            if (id < 0)
            {
                return NotFound("Id not valid");
            }
            else
            {
                try
                {
                    Psychologist psychologist = _psychologistLogic.Get(id);
                    _psychologistLogic.Delete(psychologist);
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
        /// Update a pyschologist by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newPsychologist"></param>
        /// <returns></returns>
        /// <response code="200">OK. Returns the requested object.</response>
        /// <response code="401">Unauthorized. You do not have permissions to perform this action.</response>
        /// <response code="404">NotFound. The requested object was not found.</response>
        /// <response code="422">UnprocessableEntity. Error in the semantics.</response>
        /// <response code="500">InternalServerError. The server could not handle an exception in the system.</response>
        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpPut("{id}")]
        public IActionResult UpdatePsychologist(int id, [FromBody] Psychologist newPsychologist)
        {
            try
            {
                _psychologistLogic.Update(newPsychologist, id);
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
    }
}
