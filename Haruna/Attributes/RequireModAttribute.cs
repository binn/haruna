using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Haruna;

public class RequireModAttribute : PreconditionAttribute
{
    public async override Task<PreconditionResult> CheckPermissions(ICommandContext context, CommandInfo command, IServiceProvider services)
    {
        bool isAuthorized = false;
        IGuildUser user = (IGuildUser)context.User;
        string[] roles = user.RoleIds.Select(r => r.ToString()).ToArray();
        for (int i = 0; i < roles.Length; i++)
        {
            if (GlobalConfiguration.ModIds.Contains(roles[i]))
            {
                isAuthorized = true;
                break;
            }
        }

        if (isAuthorized)
        {
            return PreconditionResult.FromSuccess();
        }
        else
        {
            return PreconditionResult.FromError("uh oh !!! senpai told me not to let you touch me. >////< srry i can't suck your cock :((((");
        }
    }
}
