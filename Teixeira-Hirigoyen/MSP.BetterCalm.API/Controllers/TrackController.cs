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
    [Route("api/track")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        ITrackLogic _trackLogic;
        public TrackController(ITrackLogic trackLogic)
        {
            _trackLogic = trackLogic;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Track track = _trackLogic.Get(id);
                return Ok(track);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    
    }
}
