using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.Domain;
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
    }
}
