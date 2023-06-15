using Newtonsoft.Json;
using Warehouse.Interfaces.AppSettings;

namespace Warehouse.AppSettings
{
    public class DefaultAppSettings : IAppSettings
    {
        public string? ConnectionString { get; set; }
        public string? ConfigConnectionString { get; set; }
        public string? NaisConnectionString { get; set; }
        public string? WaitingListsImportFolder { get; set; }

        public void Load()
        {
            var content = File.ReadAllText("AppSettings.json");
            var settings = JsonConvert.DeserializeObject<DefaultAppSettings>(content);
            ConnectionString = settings.ConnectionString;
            ConfigConnectionString = settings.ConfigConnectionString;
            NaisConnectionString = settings.NaisConnectionString;
            WaitingListsImportFolder = settings.WaitingListsImportFolder;
        }
    }
}
