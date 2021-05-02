using Microsoft.AspNetCore.Mvc;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.HandleMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSP.BetterCalm.API.Controllers
{
    [Route("api/administrator")]
    [ApiController]
    public class SessionController : ControllerBase
    {

        private readonly ISessionLogic sessionLogic;

        public SessionController(ISessionLogic sessionLogic)
        {
            this.sessionLogic = sessionLogic;
        }

        [HttpPost]
        public IActionResult Login([FromBody] Administrator admin)
        {
            try
            {
                var token = this.sessionLogic.Login(admin);
                return Ok(token);
            }
            catch (FieldEnteredNotCorrect e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
