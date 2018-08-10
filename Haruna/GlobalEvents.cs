using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace Haruna
{
    public class GlobalEvents
    {
        public static Task HandleUserJoinAsync(SocketGuildUser user)
        {
            return Task.CompletedTask;
            //throw new NotImplementedException();
        }
    }
}
