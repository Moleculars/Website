using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Bb.Middleware
{
    public class LoggingSupervisionMiddleware
    {

        public LoggingSupervisionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            Stopwatch sw = new Stopwatch();

            try
            {
                sw.Start();
                await _next(context);
                sw.Stop();
            }
            catch (System.Exception ex)
            {
                var stack = new StackTrace(ex);
                var frame = stack.GetFrame(0);
                var ilOffset = frame.GetILOffset().ToString("X");
                var method = frame.GetMethod();

                

                throw;
            }


            var t = context.Request.ToString();

            Trace.WriteLine(new { Message = "http query", sw.Elapsed }, TraceLevel.Error.ToString());

        }

        private readonly RequestDelegate _next;
    }


}
