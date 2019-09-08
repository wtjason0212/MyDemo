using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OpenTracing;
using OpenTracing.Util;
using UserCore.Common;

namespace UserCore.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var s = new SelectListItem {Text="",Value="" };
            return new string[] { "value99", "value99" };
        }

        /// <summary>
        ///     ValueGetId
        /// </summary>
        /// <param name="id">2</param>
        /// <returns>ValueGetId</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ActionResult<string>),200)]
        public ActionResult<string> Get(int id)
        {
            ITracer tracer = GlobalTracer.Instance;
            using (var scope = tracer.BuildSpan("GetIp").StartActive(finishSpanOnDispose:true))
            {
                var ip = Http.GetIp();

                tracer.ActiveSpan.Log(new Dictionary<string, object>
                {
                    ["Tag"] = "GetIp",
                    ["Msg"] = ip,
                });
            }
            PubMQ(id.ToString()+":"+DateTime.Now.ToString("HHmmss"));
            return "value";
        }


        private async void PubMQ(string msg)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

            // If serializers are not specified, default serializers from
            // `Confluent.Kafka.Serializers` will be automatically used where
            // available. Note: by default strings are encoded as UTF8.
            using (var p = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var dr = await p.ProduceAsync("test-topic", new Message<Null, string> { Value = msg });
                    Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
