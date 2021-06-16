using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Msp.BetterCalm.HandleMessage;
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
    [SwaggerTag("MedicalCondition")]
    [Route("api/medicalcondition")]
    [ApiController]
    public class MedicalConditionController : ControllerBase
    {
        IMedicalConditionLogic _medicalConditionLogic;
        public MedicalConditionController(IMedicalConditionLogic medicalConditionLogic)
        {
            _medicalConditionLogic = medicalConditionLogic;
        }
        /// <summary>
        /// Get all the medical condition
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK. Returns the requested object.</response>        
        /// <response code="500">InternalServerError. The server could not handle an exception in the system.</response>
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

        /// <summary>
        /// Get medical condition by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///  <response code="200">OK. Returns the requested object.</response>
        /// <response code="404">NotFound. The requested object was not found.</response>
        /// <response code="500">InternalServerError. The server could not handle an exception in the system.</response>
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
