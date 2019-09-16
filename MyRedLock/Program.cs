using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;

namespace MyRedLock
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var redLockFactory = RedisConnection();

            var sss = await RedisLockTest(redLockFactory);



            Console.WriteLine("Hello World!");
        }

        static async Task<string> RedisLockTest(RedLockFactory redLockFactory)
        {
            var resource = "lock_key";

            var expiry = TimeSpan.FromSeconds(30);

            var wait = TimeSpan.FromSeconds(10);

            var retry = TimeSpan.FromSeconds(10);

            while (true)
            {
                try
                {
                    using (var redLock = await redLockFactory.CreateLockAsync(resource, expiry, wait, retry))
                    {
                        if (redLock.IsAcquired)
                        {
                            Console.WriteLine("Hello!99" + DateTime.Now.ToString("mm:ss"));
                        }
                        Thread.Sleep(5000);
                    }
                }
                catch(Exception e)
                {

                }
               

            }


            Thread.Sleep(100000);
        }

        static RedLockFactory RedisConnection()
        {
            //自行管理連線
            //var endPoints = new List<RedLockEndPoint>
            //{
            //    new DnsEndPoint("redis1",6379),
            //};

            //var redlockFactory = RedLockFactory.Create(endPoints);

            //公用連線
            var existingConnectionMultiplexer1 = ConnectionMultiplexer.Connect("localhost:6379");

            var multiplexers = new List<RedLockMultiplexer>
            {
                existingConnectionMultiplexer1,
            };

            var redlockFactory = RedLockFactory.Create(multiplexers);

            return redlockFactory;
        }
    }
}
