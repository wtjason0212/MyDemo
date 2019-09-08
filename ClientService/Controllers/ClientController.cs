namespace CustomersAPIServices.Controllers
{

    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class ClientController : Controller
    {
        [HttpGet] public IEnumerable<string> Get()
        {
            return new string[] { "ClinetService-value1", "ClinetService-value2" };
        }


        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"Catcher Wong - {id}";
        }
    }
}
