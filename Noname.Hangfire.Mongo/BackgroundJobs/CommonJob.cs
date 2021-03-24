using Hangfire;
using Noname.Hangfire.Mongo.Models;
using System.Threading.Tasks;

namespace Noname.Hangfire.Mongo.BackgroundJobs
{
    public class CommonJob
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommonJob" /> class.
        /// </summary>
        public CommonJob()
        {
        }

        [Queue(QueueCustom.Queue0)]
        public async Task<bool> TestJob()
        {
            var a = 1;
            var b = 2;
            var c = a + b;
            await Task.CompletedTask;
            return true;
        }
    }
}