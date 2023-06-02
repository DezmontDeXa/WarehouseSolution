using ImportWaitingLists;
using Newtonsoft.Json;
using NLog;
using System.Diagnostics;

SharedLibrary.Logging.LoggingConfigurator.ConfigureLogger();
var logger = LogManager.GetCurrentClassLogger();
try
{
    var sourceFolder = JsonConvert.DeserializeObject<Settings>(
        File.ReadAllText("ImportWaitingList.AppSettings.json"))
        .SourceFolder;

    logger.Info($"Папка с листами: {sourceFolder}");

    var watcher = new FileWatcher(sourceFolder, "*.xml");
    watcher.FilesChanged += Watcher_FilesChanged;
    logger.Info("Ожидаем изменений в файлах...");

    var title = Console.Title;

    while (true)
    {
        Task.Delay(1000).Wait();
        Console.Title = $"{title} - {DateTime.Now}";

    }

    void Watcher_FilesChanged(object? sender, string e)
    {
        logger.Info("Файлы были изменены - обновляем БД");
        UpdateDb(sourceFolder, e);
        logger.Info("Ожидаем изменений в файлах...");
    }

    void UpdateDb(string sourceFolder, string file)
    {
        try
        {
            if (file == null)
                Importer.Import(sourceFolder);
            else
                Importer.ImportFile(file);
        }
        catch (Exception ex)
        {
            logger.Error($"Не удалось выполнить обновление БД. Ожидаем изменений. Ex:\r\n{ex}");
        }
    }

}
catch (Exception ex)
{
    logger.Fatal($"{ex}");
    Console.WriteLine("Press Enter for Exit");
    Console.ReadLine();
}
