namespace ProductsAPIServices.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    [Route("api/[controller]")]
    public class CPController : Controller
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            var aa = new Result()
            {
                Id = "1",
                Name = "Air Pod 2",
                Money = "1000",
            };

            return  JsonConvert.SerializeObject(aa);
        }

        public class Result
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Money { get; set; }
        }
    }
}
