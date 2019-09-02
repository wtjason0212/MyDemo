using Microsoft.AspNetCore.Mvc;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace UserCore.Controllers
{
    [Route("api/[controller]")]
    public class CircuitBreakerController : ControllerBase
    {
        /// <summary>
        /// CircuitBreaker111
        /// </summary>
        /// param name="q">CircuitBreaker111</param>
        /// <returns>CircuitBreaker111</returns>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var httpClient = new HttpClient();

            var repose = await Policy.HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode).WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(2))
    .ExecuteAsync(() => httpClient.GetAsync("http://localhost:23283/A9Redirect/Index"));

            return Content("");
        }
    }
}
