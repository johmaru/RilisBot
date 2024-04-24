namespace RilisBot;

class Program
{
    static async Task Main(string[] args)
    {
        var client = new Client();
       await client.ClientMain();
    }
}