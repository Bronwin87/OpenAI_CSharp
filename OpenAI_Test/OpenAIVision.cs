using System.Net.Http.Json;
using System.Text.Json;

namespace OpenAI_Test;

internal static class OpenAiVision
{
    public static async Task<string?> OpenAiVisionCall()
    {
        var apiKey = Environment.GetEnvironmentVariable("OpenAI_ApiKey");
        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://api.openai.com/v1/chat/completions");
        httpClient.DefaultRequestHeaders.Add("Authorization",
            $"Bearer {apiKey}");

        var jsonData = new
        {
            model = "gpt-4o",
            messages = new[]
            {
                new
                {
                    role = "user",
                    content = new object[]
                    {
                        new { type = "text", text = "What’s in this image?" },
                        new
                        {
                            type = "image_url",
                            image_url = new
                            {
                                url =
                                    "https://upload.wikimedia.org/wikipedia/commons/thumb/d/dd/Gfp-wisconsin-madison-the-nature-boardwalk.jpg/2560px-Gfp-wisconsin-madison-the-nature-boardwalk.jpg"
                            }
                        }
                    }
                }
            },
            max_tokens = 300
        };

        var response = await httpClient.PostAsJsonAsync("", jsonData);
        var jsonResponse = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
        return jsonResponse.RootElement
            .GetProperty("choices")
            .EnumerateArray()
            .First()
            .GetProperty("message")
            .GetProperty("content")
            .GetString();
    }
}