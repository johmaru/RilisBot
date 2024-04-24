using Claudia;

namespace RilisBot;

public class ClaudeClient
{
   private string? Response {get ; set;}
   
   private async Task ClientRun(string dismsg)
   {
      var client = new Anthropic
      {
         ApiKey = Environment.GetEnvironmentVariable("ANTHROPIC_API_KEY")
      };

      var message = await client.Messages.CreateAsync(new()
      {
        Model = "claude-3-haiku-20240307",
        MaxTokens = 1024,
        Messages = [new() {Role = "user", Content = $"以下のメッセージの感想を教えてください。-> {dismsg}"}]
      });
      Response = message.ToString();
   }
   
   public async Task<string?> GetClaudeResponse(string dismsg)
   {
      await ClientRun(dismsg);
      return Response;
   }
}