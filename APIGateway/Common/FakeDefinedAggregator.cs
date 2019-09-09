using Newtonsoft.Json;
using Ocelot.Middleware;
using Ocelot.Middleware.Multiplexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace APIGateway.Common
{
    public class FakeDefinedAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<DownstreamResponse> responses)
        {
            Console.WriteLine("This should be written but isn't");
            //return new Task<DownstreamResponse>(() =>
            //{
            //    return responses[0];
            //});
            //return responses[0];
            try
            {
                List<Result> returnResult = new List<Result>();
                foreach (var response in responses)
                {
                    var contentString = response.Content.ReadAsStringAsync().Result;
                    var content = JsonConvert.DeserializeObject<Result>(contentString);
                    returnResult.Add(content);

                }

                HttpResponseMessage aaa = new HttpResponseMessage();
                aaa.StatusCode = System.Net.HttpStatusCode.OK;
                using (var httpClient = new HttpClient())
                {

                    var content = new StringContent(JsonConvert.SerializeObject(returnResult),encoding:Encoding.UTF8);
                    HttpResponseMessage response;
                    var uri = new Uri("http://localhost:5557/api/CP");
                    ServicePoint serverPoint = ServicePointManager.FindServicePoint(uri);
                    HttpRequestMessage requestMessage = CreateRequestMessage(uri, HttpMethod.Post, content);

                    response = await httpClient.SendAsync(requestMessage);
                    aaa.Content = response.Content;

                }
                    //DownstreamResponse(HttpContent content, HttpStatusCode statusCode, List<Header> headers, string reasonPhrase);
                    DownstreamResponse resultResponse = new DownstreamResponse(aaa);

                return resultResponse;
            }
            catch (Exception e)
            {
                throw new Exception();
            }


        }

        private static HttpRequestMessage CreateRequestMessage(Uri uri, HttpMethod method, ByteArrayContent content)
        {
            var requestMessage = new HttpRequestMessage
            {
                // 使用 HTTP/1.0
                Version = HttpVersion.Version10,
                Method = method,
                RequestUri = uri
            };

            if (content != null)
            {
                requestMessage.Content = content;
            }

            // 完成後關閉連接, 預設為 false (Keep-Alive)
            requestMessage.Headers.ConnectionClose = true;

            var cacheControl = new CacheControlHeaderValue
            {
                NoCache = true
            };

            requestMessage.Headers.CacheControl = cacheControl;
            return requestMessage;
        }

        public class Result
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Money { get; set; }
        }
    }
}
