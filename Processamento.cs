using Microsoft.AspNetCore.SignalR;
using signalrprojectacs.SignalRChat.Hubs;

namespace signalrprojectacs
{
    public class Processamento
    {
        private IHubContext<ChatHub> _hubContext { get; set; }

     

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ChatHub>();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            _hubContext = serviceProvider.GetRequiredService<IHubContext<ChatHub>>();
            
        }
    }
}
