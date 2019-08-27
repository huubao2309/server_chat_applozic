using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerChatOmnicasa.Models;
using ServerChatOmnicasa.Service;

namespace ServerChatOmnicasa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : Controller
    {
        [HttpPost] // Send SMS
        public async Task<ActionResult<InfoUserSms>> Post(InfoUserSms info)
        {
            try
            {
                if (info == null)
                {
                    return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
                }

                // Handle Type: Send or Receive
                if (true) /*Type Send*/
                {
                    return StatusCode(StatusCodes.Status200OK);
                }

                if (true) /*Type Reveive*/
                {
                    return StatusCode(StatusCodes.Status200OK);
                }

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}