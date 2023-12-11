using Microsoft.AspNetCore.SignalR;
using signalrprojectacs.cloudwatchdata;

namespace signalrprojectacs
{

    namespace SignalRChat.Hubs
    {
        public class ChatHub : Hub
        {

            public async override Task OnConnectedAsync()
            {
                var logger = await CloudWatchLogger.GetLoggerAsync("test/log/group");

                await logger.LogMessageAsync($"OnConnectedAsync {Context.ConnectionId}");

            }

            public async override Task OnDisconnectedAsync(Exception? exception)
            {

                var logger = await CloudWatchLogger.GetLoggerAsync("test/log/group");

                await logger.LogMessageAsync($"OnDisconnectedAsync {Context.ConnectionId}");


            } 
            public async Task SendMessage(string user, string message)
            {

                var logger = await CloudWatchLogger.GetLoggerAsync("/dotnet/logging-demo/awssdk");

                await logger.LogMessageAsync("This is my first message!");

            }
        }
    }
}
