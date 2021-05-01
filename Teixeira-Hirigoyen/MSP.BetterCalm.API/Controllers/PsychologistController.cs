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
    public class PsychologistController : ControllerBase
    {
        IPsychologistLogic _psychologistLogic;
        public PsychologistController(IPsychologistLogic psychologistLogic)
        {
            _psychologistLogic = psychologistLogic;
        }
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
                return UnprocessableEntity(fe.MessageError());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
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
                return UnprocessableEntity(fe.MessageError());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
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
                catch (Exception e)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
                }
            }
        }
    }
}
