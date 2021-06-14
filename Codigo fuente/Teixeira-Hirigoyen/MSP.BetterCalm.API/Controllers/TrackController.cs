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
using UruguayNatural.HandleError;

namespace MSP.BetterCalm.API.Controllers
{
    [SwaggerTag("Track")]
    [Route("api/track")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        ITrackLogic _trackLogic;
        public TrackController(ITrackLogic trackLogic)
        {
            _trackLogic = trackLogic;
        }

        /// <summary>
        /// Get track by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">OK. Returns the requested object.</response>
        /// <response code="404">NotFound. The requested object was not found.</response>
        /// <response code="501">InternalServerError. The server could not handle an exception in the system.</response>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Track track = _trackLogic.Get(id);
                return Ok(track);
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
        /// Add a track
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        /// <response code="200">OK. Returns the requested object.</response>
        /// <response code="401">Unauthorized. You do not have permissions to perform this action.</response>
        /// <response code="404">NotFound. The requested object was not found.</response>
        /// <response code="422">UnprocessableEntity. Error in the semantics.</response>
        /// <response code="501">InternalServerError. The server could not handle an exception in the system.</response>
        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpPost()]
        public IActionResult Add([FromBody] Track track)
        {
            try
            {
                _trackLogic.Add(track);
                return Ok("Successfully added track name:" + track.Name);
            }
            catch (FieldEnteredNotCorrect fe)
            {
                return UnprocessableEntity(fe.MessageError());
            }
            catch (EntityAlreadyExist fe)
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
        /// Update a track by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newTrack"></param>
        /// <returns></returns>
        /// <response code="200">OK. Returns the requested object.</response>
        /// <response code="401">Unauthorized. You do not have permissions to perform this action.</response>
        /// <response code="404">NotFound. The requested object was not found.</response>
        /// <response code="422">UnprocessableEntity. Error in the semantics.</response>
        /// <response code="501">InternalServerError. The server could not handle an exception in the system.</response>
        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Track newTrack)
        {
            try
            {
                _trackLogic.Update(newTrack, id);
                return Ok("Updated successfully");
            }
            catch (FieldEnteredNotCorrect en)
            {
                return UnprocessableEntity(en.MessageError());
            }
            catch (EntityAlreadyExist fe)
            {
                return UnprocessableEntity(fe.MessageError());
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
        /// Delete a track by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">OK. Returns the requested object.</response>
        /// <response code="401">Unauthorized. You do not have permissions to perform this action.</response>
        /// <response code="404">NotFound. The requested object was not found.</response>
        /// <response code="501">InternalServerError. The server could not handle an exception in the system.</response>
        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpDelete("{id}")]
        public IActionResult DeleteTrack(int id)
        {
            if (id < 0)
            {
                return NotFound("Id not valid");
            }
            else
            {
                try
                {
                    Track track = _trackLogic.Get(id);
                    _trackLogic.Delete(track);
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
        /// Get all the tracks
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK. Returns the requested object.</response>
        /// <response code="501">InternalServerError. The server could not handle an exception in the system.</response>
        [HttpGet()]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_trackLogic.GetAll());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

    }
}
