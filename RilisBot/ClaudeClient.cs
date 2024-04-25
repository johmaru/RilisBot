using Claudia;

namespace RilisBot;

public class ClaudeClient
{
   private string? Response {get ; set;}
   private const int MessageAiCommand = 1;
   private const int TranslateAiCommand = 2;
   private const string MessageAiString = "以下のメッセージの感想をメッセージの言語で教えてください。 ->";
   private const string TranslateAiString = "以下のメッセージを日本語に翻訳してください。翻訳だけを出力してください ->";
   
   private readonly Dictionary <int, string> _aiCommandStrings = new Dictionary<int, string>
   {
      {MessageAiCommand, MessageAiString},
      {TranslateAiCommand, TranslateAiString}
   };
   
   private async Task ClientRun(int aiCommandGenre,string dismsg)
   {
      var client = new Anthropic
      {
         ApiKey = Environment.GetEnvironmentVariable("ANTHROPIC_API_KEY")
      };

      var message = await client.Messages.CreateAsync(new()
      {
        Model = "claude-3-haiku-20240307",
        MaxTokens = 1024,
        Messages = [new() {Role = "user", Content = $"{_aiCommandStrings[aiCommandGenre]} {dismsg}"}]
      });
      Response = message.ToString();
   }
   
   public async Task<string?> GetClaudeResponse(int aiCommandGenre,string dismsg)
   {
      await ClientRun(aiCommandGenre,dismsg);
      return Response;
   }
}