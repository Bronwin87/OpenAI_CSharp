using System.Text.Json;
namespace OpenAI_Test;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public static class OpenAiSpeech
{
    public static async Task<Stream?> CreateSpeechStreamAsync(string visionInput)
    {
        var apiKey = Environment.GetEnvironmentVariable("OpenAI_ApiKey");
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var jsonData = new
        {
            model = "tts-1-hd-1106",
            input = visionInput,
            voice = "alloy"
        };

        var content = new StringContent(JsonSerializer.Serialize(jsonData), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("https://api.openai.com/v1/audio/speech", content);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStreamAsync();
        }
        else
        {
            Console.WriteLine($"Failed to generate speech: {response.Content.ReadAsStringAsync().Result}");
            return null;
        }
    }

}