using Microsoft.AspNetCore.Http;
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
    [Route("api/playlist")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        IPlaylistLogic _playlistLogic;
        public PlaylistController(IPlaylistLogic playlistLogic)
        {
            _playlistLogic = playlistLogic;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Playlist playlist = _playlistLogic.Get(id);
                return Ok(playlist);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost()]
        public IActionResult Add([FromBody] Playlist playlist)
        {
            try
            {
                _playlistLogic.Add(playlist);
                return Ok("Successfully added playlist name:"+ playlist.Name);
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
                catch (Exception e)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
                }
            }
        }
    }
}
