using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EchoServer.Controllers
{
    [Route("/api/value")]
    public class ValueController : Controller
    {
        protected IDistributedCache Cache { get; set; }

        public ValueController(IDistributedCache cache)
        {
            Cache = cache;
        }

        [HttpGet]
        public IActionResult Get()
        {
            for(int i = 0; i < 10000; i++)
            {
                Cache.SetString(i.ToString(), "123456789123456789abcdefghijklmnopqrsduvwxyzabcdefghijklmnopqrstuvwxyzadeewrwerji123j12ij1241241232141242");
                var result = Cache.GetString(i.ToString());
            }
            return Ok();
        }
    }
}
