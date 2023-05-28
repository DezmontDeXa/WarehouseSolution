using Newtonsoft.Json;

namespace SharedLibrary.AppSettings
{
    public class Settings
    {
        public string? ConnectionString { get; set; }
        public string? NaisConnectionString { get; set; }

        public static Settings Load()
        {
            var content = File.ReadAllText("AppSettings.json");
            var settings = JsonConvert.DeserializeObject<Settings>(content);
            return settings;
        }
    }
}
