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
        const string SourceFolder = "C:\\Users\\DezmontDeXa\\Downloads\\Telegram Desktop";

        public static void Import()
        {
            var importerService = new WaitingListImporterService();

            foreach (var file in Directory.GetFiles(SourceFolder))
            {
                try
                {
                    if (!file.EndsWith(".xml")) continue;
                    if (!Path.GetFileNameWithoutExtension(file).StartsWith("СписокПогрузкиРазгрузки")) continue;

                    importerService.ImportList(
                        SharedLibrary.DataBaseModels.AccessGrantType.Tracked,
                        new FileInfo(file));
                }catch(Exception ex)
                {
                    Console.WriteLine(  ex);
                }
            }
        }
    }
}
