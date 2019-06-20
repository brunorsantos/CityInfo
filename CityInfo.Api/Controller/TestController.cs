using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.Api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private CityInfoContext _ctx;
        public TestController(CityInfoContext cityInfoContext)
        {
            _ctx = cityInfoContext;
        }
        [HttpGet]
        public ActionResult get()
        {
            return Ok();
        }
    }
}