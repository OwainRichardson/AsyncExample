using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace Async_Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //await Async.Run();
            await OwainTest.Run();
        }
    }
}
