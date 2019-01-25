using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiHost
{
    public class TestController : ControllerBase
    {
        [Route("test")]
        [Authorize]
        public IActionResult Get()
        {
            var claims = User.Claims.Select(x => x.Type + ":" + x.Value);
            return Ok(new { message = "Hello API!", claims });
        }
    }
}
