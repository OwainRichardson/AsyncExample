using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Async_Test
{
    public class OwainTest
    {
        public static async Task Run()
        {
            Stopwatch stop = new Stopwatch();
            stop.Start();

            string name = GetName();
            int age = GetAge();

            stop.Stop();

            Console.WriteLine($"Name: {name} - age: {age} - {stop.Elapsed.TotalMilliseconds}ms");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Task<string> nameTask = Task.Run(() => GetName());
            Task<int> ageTask = Task.Run(() => GetAge());

            await Task.WhenAll(nameTask, ageTask);

            string name2 = await nameTask;
            int age2 = await ageTask;

            stopwatch.Stop();

            Console.WriteLine($"Name: {name2} - age: {age2} - {stopwatch.Elapsed.TotalMilliseconds}ms");
        }

        private static int GetAge()
        {
            Thread.Sleep(1500);

            return 32;
        }

        private static string GetName()
        {
            Thread.Sleep(1000);

            return "Owain";
        }
    }
}
