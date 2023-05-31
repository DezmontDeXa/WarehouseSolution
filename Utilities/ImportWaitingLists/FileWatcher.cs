namespace ImportWaitingLists
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
