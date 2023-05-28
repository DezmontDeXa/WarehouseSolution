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
        public static void Import()
        {
            var importerService = new WaitingListImporterService();

            foreach (var file in Directory.GetFiles("XmlWaitingLists"))
            {
                try
                {
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
