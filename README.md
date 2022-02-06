# Cowboy
Tiny Discord bot for a tiny Discord server

### Commands
 - `/cowboy-help` - Lists all commands.
 - `/roll [low: 1] [high: 10]` - Rolls a random number between `low` and `high` inclusive.
 - `/flip-coin` - Flips a coin, resulting in Heads or Tails.
 - `/random-wiki` - Gets a link to a random Wikipedia article.
 - `/random-street-view` - Gets a link to a random Google Maps Street View.

### Running
Running the following will start the bot, registering all commands to a guild:
```
./Cowboy --token <your Discord token> --guild <your guild ID>
```

Running the following will start the bot, registering all commands globally:
```
./Cowboy --token <your Discord token>
```

Command line arguments can also be omitted in favor of environment variables: `CowboyDiscordToken`, `CowboyDiscordGuildId`.
