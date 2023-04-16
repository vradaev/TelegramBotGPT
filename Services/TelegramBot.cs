using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramGPT.Services
{
  public class TelegramBot
  {
    private readonly IConfiguration _configuration;

    public TelegramBot(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public async Task<bool> SendMessageTextAsync(long chatId, string message)
    {
      try
      {
        var bot = new TelegramBotClient(_configuration["TelegramSettings:Token"]);

        await bot.SendTextMessageAsync(chatId, message);

        return true;

      }
      catch (Exception ex)
      {
        return false;
      }
    }
  }
}