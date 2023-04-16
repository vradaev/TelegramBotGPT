﻿using TelegramGPT.Services;

namespace TelegramGPT.EndPoints.WebHook
{
  public class WebHook
  {
    public static string Template => "/TelegramGPT/";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    public static async Task<IResult> Action(WebHookRequest request, ChatGPT chatGPT, TelegramBot tgBot)
    {
      try
      {
        var message = "Please use text.";
        var botMention = $"@CarTrekDevBot";
        if (request.Message?.Text != null && request.Message.Text.StartsWith(botMention))
        {
          // Удаляем упоминание бота из сообщения
          var text = request.Message.Text.Substring(botMention.Length).Trim();
          if (!string.IsNullOrEmpty(text))
          {
            message = await chatGPT.AskQuestions(text);
          }
          var chatId = request.Message.Chat.Id;

          var result = await tgBot.SendMessageTextAsync(chatId, message);

          if (!result)
          {
            return Results.BadRequest();
          }

        }

        return Results.Ok();
      }
      catch (Exception ex)
      {
        return Results.BadRequest();
      }
    }
  }
}