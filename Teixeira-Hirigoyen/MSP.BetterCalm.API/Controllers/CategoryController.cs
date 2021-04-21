using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Msp.BetterCalm.HandleMessage;
using MSP.BetterCalm.BusinessLogicInterface;
using System;
using System.Linq;

namespace MSP.BetterCalm.API.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryLogic _categoryLogic;
        public CategoryController(ICategoryLogic categoryLogic)
        {
            _categoryLogic = categoryLogic;
        }

        [HttpGet()]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_categoryLogic.GetAll());
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
    }
}
