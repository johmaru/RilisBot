using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace RilisBot;

public class Client
{
   private static DiscordSocketClient? _client;
   private static readonly string? Token = Tokenizer.GetToken();
   
   public async Task ClientMain()
   {
      var config = new DiscordSocketConfig()
      {
         GatewayIntents = GatewayIntents.All
      };
      
      _client = new DiscordSocketClient(config);
      _client.Log += Log;
      
      await _client.LoginAsync(TokenType.Bot, Token);
      await _client.StartAsync();

      _client.MessageUpdated += MessageUpdated;
      _client.Ready += ClientOnReady;
      
      CommandsHandler commandsHandler = new(_client, new CommandService());
      await commandsHandler.InstallCommandAsync();
      
      await Task.Delay(-1);
   }

   public async Task ClientOnReady()
   {
      Console.WriteLine("Client is ready!");
   }

   private static async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
   {
      var message = await before.GetOrDownloadAsync();
      Console.WriteLine($"{message} -> {after}");
   }
#if DEBUG
   

   private static Task Log(LogMessage log)
   {
      Console.WriteLine(log.ToString());
      return Task.CompletedTask;
   }
}
#endif
public static class Tokenizer
{
   public static string? GetToken()
   {
      return Environment.GetEnvironmentVariable("DISCORD_TOKEN");
   }
}