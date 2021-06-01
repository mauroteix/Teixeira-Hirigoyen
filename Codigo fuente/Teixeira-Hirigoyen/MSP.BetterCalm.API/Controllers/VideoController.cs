using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Msp.BetterCalm.HandleMessage;
using MSP.BetterCalm.API.Filters;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.HandleMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UruguayNatural.HandleError;

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

        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpPost()]
        public IActionResult Add([FromBody] Video video)
        {
            try
            {
                _videoLogic.Add(video);
                return Ok("Successfully added track name:" + video.Name);
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

        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Video newVideo)
        {
            try
            {
                _videoLogic.Update(newVideo, id);
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

        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id < 0)
            {
                return NotFound("Id not valid");
            }
            else
            {
                try
                {
                    Video video = _videoLogic.Get(id);
                    _videoLogic.Delete(video);
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


        [HttpGet()]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_videoLogic.GetAll());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
