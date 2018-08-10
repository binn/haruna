using System;
using System.Threading.Tasks;
using Discord.Commands;

namespace Haruna.Modules
{
    [Group("error")]
    public class ErrorModule : ModuleBase
    {
        [Command]
        public Task CauseSomeCoolError()
        {
            throw new Exception(Context.User.ToString() + " made this happen to me !!!! how am i supposed to MODERATE THE SERVER WHEN I'M THIS FUCKING WET!!! >:( u better watch out, i'm coming for ur ochinchin");
        }
    }
}
