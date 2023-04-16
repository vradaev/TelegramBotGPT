using Newtonsoft.Json;
using System.Text;

namespace TelegramGPT.Services
{
  public class ChatGPT
  {
    private readonly IConfiguration _configuration;

    public ChatGPT(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public async Task<string> AskQuestions(string prompt)
    {
      try
      {
        var apiKey = _configuration["ChatGPTSettings:Token"];
        var modelGpt = _configuration["ChatGPTSettings:Model"];
        var maxTokens = _configuration["ChatGPTSettings:MaxTokens"];

        var url = "https://api.openai.com/v1/completions";

        var jsonRequest = JsonConvert.SerializeObject(new
        {
          prompt = prompt,
          max_tokens = Convert.ToInt32(maxTokens),
          n = 1,
          model = modelGpt
        });

        using (var client = new HttpClient())
        {
          client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

          var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

          var response = await client.PostAsync(url, httpContent);


          if (!response.IsSuccessStatusCode)
          {

            return string.Empty;
          }
          else
          {
            var jsonResponse = await response.Content.ReadAsStringAsync();

            dynamic responseObject = JsonConvert.DeserializeObject(jsonResponse);

            return responseObject.choices[0].text;
          }
        }
      }
      catch (Exception ex)
      {
        return string.Empty;
      }
    }
  }
}