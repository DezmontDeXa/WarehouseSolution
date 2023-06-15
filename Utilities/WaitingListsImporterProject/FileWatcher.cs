
namespace Warehouse.Utilities.WaitingListsImporterProject
{
    public class FileWatcher
    {
        private readonly string sourceFolder;
        private readonly string filter;
        List<string> files = new List<string>();

        public event EventHandler<string> FilesChanged;

        public FileWatcher(string sourceFolder, string filter)
        {        
            Task.Run(Watching);
            this.sourceFolder = sourceFolder;
            this.filter = filter;
        }

        private void Watching()
        {
            Task.Delay(2000).Wait();

            files = Directory.GetFiles(sourceFolder, filter).ToList();
            FilesChanged?.Invoke(this, null);

            Task.Delay(1000).Wait();

            while (true)
            {
                foreach (var file in Directory.GetFiles(sourceFolder, filter))
                {
                    if (files.Contains(file)) continue;

                    files.Add(file);

                    FilesChanged?.Invoke(this, file);
                }

                Task.Delay(300).Wait();
            }
        }
    }
}
