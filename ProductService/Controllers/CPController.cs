namespace ProductsAPIServices.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
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
                Id = "2",
                Name = "Surface Book 2",
                Money = "600",
            };

            //return new string[] { JsonConvert.SerializeObject(aa) };
            return JsonConvert.SerializeObject(aa);
        }

        [HttpPost]
        public ActionResult<string> Post(string requestString)
        {
            var aa = new Result()
            {
                Id = "0000",
                Name = "00000",
                Money = "600",
            };

            //return new string[] { JsonConvert.SerializeObject(aa) };
            return JsonConvert.SerializeObject(aa);
        }

        public void JsonReader()
        {
            //using (StreamReader reader = new StreamReader(Request, Encoding.UTF8, false, 1024, true))
            //{
            //    string postBody = reader.ReadToEnd().Trim();

               

            //    Request.InputStream.Seek(0, SeekOrigin.Begin);
            //}

        }

        public class Result
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Money { get; set; }
        }
    }
}
