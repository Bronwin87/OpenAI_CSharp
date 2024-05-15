using System.Net.Http.Json;

namespace OpenAI_Test;

internal static class OpenAiSpeech
{
    public static async Task<Stream?> CreateSpeechStreamAsync(string visionInput)
    {
        var requestUri = new Uri("https://api.openai.com/v1/audio/speech");
        var apiKey = Environment.GetEnvironmentVariable("OpenAI_ApiKey");
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("Authorization",
            $"Bearer {apiKey}");

        var jsonData = new
        {
            model = "tts-1-hd-1106",
            input = visionInput,
            voice = "alloy"
        };


        var response = await httpClient.PostAsJsonAsync(requestUri, jsonData);

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