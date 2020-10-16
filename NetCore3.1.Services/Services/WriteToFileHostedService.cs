using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace NetCore3_1.Services.Services
{
    public class WriteToFileHostedService : IHostedService, IDisposable
    {
        private readonly IHostingEnvironment environment;
        private readonly string fileName = "file_1.txt";
        private Timer timer;

        public WriteToFileHostedService(IHostingEnvironment environment)
        {
            this.environment = environment;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            WriteToFile("WriteToFileHostedService: Process started");

            timer = new Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            WriteToFile("WriteToFileHostedService: Process stopped");

            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void TimerCallback(object state) => WriteToFile($"WriteToFileHostedService: {DateTime.UtcNow}");

        private void WriteToFile(string message)
        {
            var path = $@"{environment.ContentRootPath}\{fileName}";

            using (StreamWriter writer = new StreamWriter(path, append: true))
            {
                writer.WriteLine(message);
            }
        }
    }
}