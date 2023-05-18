using SharedLibrary.DataBaseModels;
using System.IO;

namespace AdminClient.Services.Interfaces
{
    public interface IWaitingListImporterService
    {
        void ImportList(AccessGrantType accessGrantType, FileInfo xmlFileInfo);
        void ImportList(AccessGrantType accessGrantType, string xmlFileContent);
    }
}
