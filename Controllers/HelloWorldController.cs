using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using signalrprojectacs.cloudwatchdata;
using signalrprojectacs.SignalRChat.Hubs;

namespace signalrprojectacs.Controllers
{
    public class HelloWorldController : Controller
    {
        private readonly IHubContext<ChatHub> _hubContext;


        public HelloWorldController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }
        // 
        // GET: /HelloWorld/
        public string Index()
        {
            return "This is my default action...";
        }
        // 
        // GET: /HelloWorld/Welcome/ 
        public async Task<string> Welcome()
        {

            var logger = await CloudWatchLogger.GetLoggerAsync("test/log/group");

            await logger.LogMessageAsync($"OnConnectedAsync {DateTime.Now}");

            return "This is the Welcome action method...";
        }
    }
}
