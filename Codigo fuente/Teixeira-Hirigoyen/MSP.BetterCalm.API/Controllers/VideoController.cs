using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Msp.BetterCalm.HandleMessage;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSP.BetterCalm.API.Controllers
{
    public class VideoController : ControllerBase
    {

        IVideoLogic _videoLogic;
        public VideoController(IVideoLogic videoLogic)
        {
            _videoLogic = videoLogic;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Video video = _videoLogic.Get(id);
                return Ok(video);
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
