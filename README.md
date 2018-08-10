[![Haruna Logo](https://cdn.discordapp.com/avatars/477192814405484564/42cafea631db81597aa908ca353fd8b9.jpg?size=256)](https://bin.moe)

# Haruna

[![Build Status](https://travis-ci.org/binsenpai/haruna.svg?branch=master)](https://travis-ci.org/binsenpai/haruna)

[![Docker Status](https://img.shields.io/badge/docker-1.0-blue.svg)](https://hub.docker.com/r/binzy/haruna/)


Haruna is *the* lightweight and simple moderation bot. She's only got a handful of commands and emergency server management. She's designed and made for the "[cakechan](https://discord.gg/QEtRdka)" server, a Discord server owned by a friend of mine.

She's simple, easy to run, and is fast. She runs on .NET Core so she's also cross-platform!

Haruna relies on the .NET Core runtime, Discord.Net, Microsoft.Extensions.Logging, and Microsoft.Extensions.DependencyInjection. All these packages can be found on NuGet and can be restored using `dotnet restore`.

## Getting Started

Hosting Haruna is easy, but you'll need Docker to use Haruna's one-click run.
To run Haruna:

```s
docker run -d -e HARUNA_TOKEN="YOUR_BOTS_TOKEN_HERE" \
-e HARUNA_MUTE="MUTE_ROLE_ID" \
-e HARUNA_PREFIX="BOT_PREFIX" \
-e HARUNA_MODS="MOD_IDS" \
-e HARUNA_CHANNELS="CHANNEL_IDS" binzy/haruna
```

<small><b>Quick note:</b> Docker environment files are also compatible.</small>

**Note**: Haruna's responses contain vulgar language that isn't acceptable in some areas, please modify the source code if you'd like this removed. In the future these vulgar responses will be removed from her.

| Environment Variable | Description |
|----------------------|-------------|
| `HARUNA_TOKEN` | The token of your Discord bot. |
| `HARUNA_PREFIX` | The prefix of Haruna's commands. Haruna also accepts @mention commands. |
| `HARUNA_MUTE` | The ID of the role to give to muted users. |
| `HARUNA_MODS` | A list of role IDs seperated by a semicolon (`;`) that have moderation permissions |
| `HARUNA_CHANNELS` | A list of channel IDs seperated by a semicolon (`;`) that will get locked when using the `lock all` command. |

## Commands

| Base Command | Description | Example |
|--------------|-------------|---------|
| `.mute` / `.m` | Assigns `HARUNA_MUTE` role to a given user. Mute is permanent unless otherwhise specified | `.m @user` / `.m @user 10` |
| `.unmute` / `.um` | Unmutes user (Unassigns `HARUNA_MUTE` role.) | `.um @user` |
| `.prune` / `.p` | Deletes last `x` messages from the current channel, or messages from a specified user in the last `x` messages if specified. | `.p 10` / `.p 10 @user` |
| `.kick` / `.k` | Kicks given user. Any text after mention is used as a reason in the guild's Audit Log. | `.k @user spamming` |
| `.ban` / `.b` | Bans given user. Any text after mention is used as a reason in the guild's Audit Log. | `.b @user spamming` |
| `.softban` / `.sb` | Bans and then immidiately unbans given user. Any text after mention is used as a reason in the guild's Audit Log. | `.sb @user spamming` |
| `.lock` / `.l` | Denies `Send Messages` and `Add Reactions` permissions for the `@everyone` role in the specified channel, or the current channel if none is specified. | `.l #general` |
| `.unlock` / `.ul` | Resets `Send Messages` and `Add Reactions` permissions for the `@everyone` role in the specified channel, or the current channel if none is specified. | `.ul #general` |
| `.lock all` / `.l a` | Locks all channels specified in `HARUNA_CHANNELS` | `.l a` |
| `.unlock all` / `.ul a` | Unlocks all channels specified in `HARUNA_CHANNELS` | `.ul a` |