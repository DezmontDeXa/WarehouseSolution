using Warehouse.Interfaces.DataBase;


namespace Warehouse.Utilities.WaitingListsImporterProject
{
    public class Importer
    {
        public static void Import(string sourceFolder)
        {
            var importerService = new WaitingListImporterService();

            foreach (var file in Directory.GetFiles(sourceFolder))
            {
                ImportFile( file, importerService);
            }
        }

        public static void ImportFile( string file, WaitingListImporterService importerService=null)
        {
            try
            {
                importerService ??= new WaitingListImporterService();

                if (!file.EndsWith(".xml")) return;

                importerService.ImportList(
                    AccessGrantType.Tracked,
                    new FileInfo(file));

                WriteLog($"Импорован файл {Path.GetFileName(file)}.");
            }
            catch (Exception ex)
            {
                WriteLog($"Не удалось импортировать файл {Path.GetFileName(file)}. {ex}");
                return;
            }
        }

        private static void WriteLog(string msg)
        {
            Console.WriteLine($"{msg}");
        }
    }
}
