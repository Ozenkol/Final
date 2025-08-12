using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")] 
    [ApiController]
    public class HelloWorldController: ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            var data = new { Message = "HelloWorld" };
            return Ok(data);
        }
    }
}