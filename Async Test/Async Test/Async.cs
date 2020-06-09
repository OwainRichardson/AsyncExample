using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace Async_Test
{
    public static class Async
    {
        public static async Task Run()
        {
            // Warm up
            Console.WriteLine("Warm Up");
            RunSynchronously();
            RunSynchronously();

            // Run
            Console.WriteLine("Run");
            RunSynchronously();
            await RunAsynchronously();
            await RunParallelAsynchronously();
            RunTaskParallelAsynchronously();

        }

        private static async Task RunAsynchronously()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            await RunDownloadAsync();

            stopwatch.Stop();
            Console.WriteLine($"Asynchronous run took {stopwatch.Elapsed.TotalMilliseconds}ms");
            Console.WriteLine();
        }

        private static async Task RunParallelAsynchronously()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            await RunDownloadParallelAsync();

            stopwatch.Stop();
            Console.WriteLine($"Asynchronous parallel run took {stopwatch.Elapsed.TotalMilliseconds}ms");
            Console.WriteLine();
        }

        private static void RunTaskParallelAsynchronously()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            RunDownloadTaskParallelAsync();

            stopwatch.Stop();
            Console.WriteLine($"Asynchronous parallel task run took {stopwatch.Elapsed.TotalMilliseconds}ms");
            Console.WriteLine();
        }

        private static async Task RunDownloadAsync()
        {
            foreach (var website in websiteData)
            {
                WebsiteDataModel model = await Task.Run(() => DownloadWebsite(website));
                ReportData(model);
            }
        }

        private static async Task RunDownloadParallelAsync()
        {
            List<Task<WebsiteDataModel>> tasks = new List<Task<WebsiteDataModel>>();

            foreach (var website in websiteData)
            {
                tasks.Add(Task.Run(() => DownloadWebsite(website)));
            }

            var results = await Task.WhenAll(tasks);

            foreach (var item in results)
            {
                ReportData(item);
            }
        }

        private static void RunDownloadTaskParallelAsync()
        {
            Parallel.ForEach(websiteData, (website) =>
            {
                var model = DownloadWebsite(website);
                ReportData(model);
            });
        }

        private static void RunSynchronously()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            RunDownloadSync();

            stopwatch.Stop();
            Console.WriteLine($"Synchronous run took {stopwatch.Elapsed.TotalMilliseconds}ms");
            Console.WriteLine();
        }

        private static void RunDownloadSync()
        {
            foreach (var website in websiteData)
            {
                WebsiteDataModel model = DownloadWebsite(website);
                ReportData(model);
            }
        }

        private static void ReportData(WebsiteDataModel model)
        {
            //Console.WriteLine($"{model.WebsiteUrl} downloaded {model.WebsiteData.Length} characters");
        }

        private static WebsiteDataModel DownloadWebsite(string website)
        {
            WebsiteDataModel model = new WebsiteDataModel();
            using (WebClient client = new WebClient())
            {
                model.WebsiteUrl = website;
                model.WebsiteData = client.DownloadString(website);
            }

            return model;
        }

        private static List<string> websiteData = new List<string>
        {
            "https://www.google.com",
            "https://www.yahoo.com",
            "https://www.microsoft.com",
            "https://cnn.com",
            "https://www.codeproject.com",
            "https://www.stackoverflow.com"
        };
    }
}
