using OpenAI_Test;
using NAudio.Wave;

var result = await OpenAiVision.OpenAiVisionCall();
if (result == null) return;

var audioStream = await OpenAiSpeech.CreateSpeechStreamAsync(result);
if (audioStream == null)
{
    Console.WriteLine("Failed to generate audio.");
    return;
}

await using var mp3Reader = new Mp3FileReader(audioStream);
using var waveOut = new WaveOutEvent();
waveOut.Init(mp3Reader);
waveOut.Play();

Console.WriteLine("Audio is playing. Press any key to stop...");
Console.ReadKey(true);
waveOut.Stop();