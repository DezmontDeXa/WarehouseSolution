using AdminClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportWaitingLists
{
    public class Importer
    {
        public static void Import(string sourceFolder)
        {
            var importerService = new WaitingListImporterService();

            foreach (var file in Directory.GetFiles(sourceFolder))
            {
                try
                {
                    if (!file.EndsWith(".xml")) continue;

                    importerService.ImportList(
                        SharedLibrary.DataBaseModels.AccessGrantType.Tracked,
                        new FileInfo(file));
                }catch(Exception ex)
                {
                    WriteLog($"Не удалось импортировать файл {Path.GetFileName(file)}. {ex.Message}");
                    continue;
                }
            }
        }

        private static void WriteLog(string msg)
        {
            Console.WriteLine($"{msg}");
        }
    }
}
