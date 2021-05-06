using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Msp.BetterCalm.HandleMessage;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.DTO;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Linq;

namespace MSP.BetterCalm.API.Controllers
{
    [SwaggerTag("Category")]
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryLogic _categoryLogic;
        public CategoryController(ICategoryLogic categoryLogic)
        {
            _categoryLogic = categoryLogic;
        }
        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK. Returns the requested object.</response>        
        /// <response code="501">InternalServerError. The server could not handle an exception in the system.</response>
        [HttpGet()]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_categoryLogic.GetAll());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Get category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">OK. Returns the requested object.</response>        
        /// <response code="404">NotFound. The requested object was not found.</response>
        /// <response code="501">InternalServerError. The server could not handle an exception in the system.</response>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                CategoryDTO category = _categoryLogic.Get(id);
                return Ok(category);
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
