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
    }
}
