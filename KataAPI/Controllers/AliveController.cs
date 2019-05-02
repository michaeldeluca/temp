using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;

namespace KataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AliveController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Connection Successful!");
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class EchoNumberController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(string number)
        {
            if (number == null || !int.TryParse(number, out int item))
            {
                return Ok("Please ensure to enter a valid integer!");
            }

            return Ok($"{number}");
        }
    }

   
}
