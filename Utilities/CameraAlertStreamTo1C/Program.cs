using CameraAlertStreamTo1C;
using CameraListenerService;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

// Get values from the config given their key and their target type.
Settings settings = config.GetSection("Settings").Get<Settings>();

var _http = new HttpClient();
var _callbackLink = settings.CallbackUri;

RunCameras(settings.Cameras);

void RunCameras(List<string> cameras)
{
	foreach (var cameraUri in cameras)
	{
        var listener = new CameraListener(new Uri(cameraUri));
        listener.OnNotification += Listener_OnNotification;
        listener.OnError += Listener_OnError;
    }
}

void Listener_OnError(object? sender, Exception e)
{
    Console.WriteLine($"Exception: {e}. Listener will be restarted.");
}

void Listener_OnNotification(object? sender, CameraNotifyBlock e)
{
    Console.WriteLine((object)e);
    try
    {
        var content = new MultipartFormDataContent();
        content.Headers.ContentType = new MediaTypeHeaderValue(e.ContentType);
        content.Add(new ByteArrayContent(e.ContentBytes));
        var result = _http.PostAsync(_callbackLink, content).Result;

        if (!result.IsSuccessStatusCode)
            Console.WriteLine($"API return error. Status Code: {result.StatusCode}.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error on sending callback to API: {ex.Message}.");
    }
}