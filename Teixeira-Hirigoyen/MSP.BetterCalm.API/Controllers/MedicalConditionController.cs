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

    [Route("api/medicalcondition")]
    [ApiController]
    public class MedicalConditionController : ControllerBase
    {
        IMedicalConditionLogic _medicalConditionLogic;
        public MedicalConditionController(IMedicalConditionLogic medicalConditionLogic)
        {
            _medicalConditionLogic = medicalConditionLogic;
        }
        [HttpGet()]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_medicalConditionLogic.GetAll());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                MedicalCondition medicalCondition = _medicalConditionLogic.Get(id);
                return Ok(medicalCondition);
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
