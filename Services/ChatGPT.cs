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

        var url = "https://api.openai.com/v1/completions";

        var jsonRequest = JsonConvert.SerializeObject(new
        {
          prompt = prompt,
          max_tokens = 2048,
          n = 1,
          model = "text-davinci-003"
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