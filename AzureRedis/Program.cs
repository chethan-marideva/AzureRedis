using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AzureRedis
{
    class Program
    {
        async static Task Main(string[] args)
        {

            string connectionString = "";

            using ( var cache = ConnectionMultiplexer.Connect(connectionString)) {

                IDatabase db = cache.GetDatabase();

                bool setValue = db.StringSet("test:key", "some value");
                Console.WriteLine($"SET: {setValue}");

                string getValue = db.StringGet("test:key");
                Console.WriteLine($"GET: {getValue}");

                setValue = await db.StringSetAsync("test", "100");
                Console.WriteLine($"SET: {setValue}");

                getValue = await db.StringGetAsync("test");
                Console.WriteLine($"GET: {getValue}");

                var result = await db.ExecuteAsync("ping");
                Console.WriteLine($"PING = {result.Type} : {result}");

                result = await db.ExecuteAsync("flushdb");
                Console.WriteLine($"FLUSHDB = {result.Type} : {result}");

            }

        }
    }
}
