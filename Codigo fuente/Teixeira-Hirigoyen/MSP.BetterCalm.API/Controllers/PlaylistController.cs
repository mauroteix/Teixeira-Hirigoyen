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
    [SwaggerTag("Playlist")]
    [Route("api/playlist")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        IPlaylistLogic _playlistLogic;
        public PlaylistController(IPlaylistLogic playlistLogic)
        {
            _playlistLogic = playlistLogic;
        }

        /// <summary>
        /// Get a playlist by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///  <response code="200">OK. Returns the requested object.</response>
        /// <response code="404">NotFound. The requested object was not found.</response>
        /// <response code="501">InternalServerError. The server could not handle an exception in the system.</response>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Playlist playlist = _playlistLogic.Get(id);
                return Ok(playlist);
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
        /// Add a playlist
        /// </summary>
        /// <param name="playlist"></param>
        /// <returns></returns>
        /// <response code="200">OK. Returns the requested object.</response>
        /// <response code="401">Unauthorized. You do not have permissions to perform this action.</response>
        /// <response code="404">NotFound. The requested object was not found.</response>
        /// <response code="422">UnprocessableEntity. Error in the semantics.</response>
        /// <response code="501">InternalServerError. The server could not handle an exception in the system.</response>
        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpPost()]
        public IActionResult Add([FromBody] Playlist playlist)
        {
            try
            {
                _playlistLogic.Add(playlist);
                return Ok("Successfully added playlist name:" + playlist.Name);
            }
            catch (FieldEnteredNotCorrect fe)
            {
                return UnprocessableEntity(fe.MessageError());
            }
            catch (EntityNotExists fe)
            {
                return NotFound(fe.MessageError());
            }
            catch (EntityAlreadyExist fe)
            {
                return UnprocessableEntity(fe.MessageError());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Delete a playlist by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">OK. Returns the requested object.</response>
        /// <response code="401">Unauthorized. You do not have permissions to perform this action.</response>
        /// <response code="404">NotFound. The requested object was not found.</response>
        /// <response code="501">InternalServerError. The server could not handle an exception in the system.</response>
        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpDelete("{id}")]
        public IActionResult DeletePlaylist(int id)
        {
            if (id < 0)
            {
                return NotFound("Id not valid");
            }
            else
            {
                try
                {
                    Playlist playlist = _playlistLogic.Get(id);
                    _playlistLogic.Delete(playlist);
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
        /// Update a playlist by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newPlaylist"></param>
        /// <returns></returns>
        /// <response code="200">OK. Returns the requested object.</response>
        /// <response code="401">Unauthorized. You do not have permissions to perform this action.</response>
        /// <response code="404">NotFound. The requested object was not found.</response>
        /// <response code="422">UnprocessableEntity. Error in the semantics.</response>
        /// <response code="501">InternalServerError. The server could not handle an exception in the system.</response>
        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpPut("{id}")]
        public IActionResult UpdatePlaylist(int id, [FromBody] Playlist newPlaylist)
        {
            try
            {
                _playlistLogic.Update(newPlaylist, id);
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
            catch (EntityAlreadyExist fe)
            {
                return UnprocessableEntity(fe.MessageError());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Get all playlist
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK. Returns the requested object.</response>
        /// <response code="501">InternalServerError. The server could not handle an exception in the system.</response>
        [HttpGet()]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_playlistLogic.GetAll());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
