using Newtonsoft.Json;

namespace TelegramGPT.EndPoints.WebHook
{
  public class WebHookRequest
  {
    [JsonProperty("update_id")]
    public int UpdateId { get; set; }

    [JsonProperty("message")]
    public TelegramMessage Message { get; set; }
  }
  public class TelegramMessage
  {
    [JsonProperty("message_id")]
    public int MessageId { get; set; }

    [JsonProperty("from")]
    public TelegramUser From { get; set; }

    [JsonProperty("chat")]
    public TelegramChat Chat { get; set; }

    [JsonProperty("date")]
    public int Date { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }
  }

  public class TelegramUser
  {
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("is_bot")]
    public bool IsBot { get; set; }

    [JsonProperty("first_name")]
    public string FirstName { get; set; }

    [JsonProperty("last_name")]
    public string LastName { get; set; }

    [JsonProperty("username")]
    public string UserName { get; set; }

    [JsonProperty("language_code")]
    public string LanguageCode { get; set; }
  }

  public class TelegramChat
  {
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("first_name")]
    public string FirstName { get; set; }

    [JsonProperty("last_name")]
    public string LastName { get; set; }

    [JsonProperty("username")]
    public string UserName { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }
  }
}