using SharedLibrary.DataBaseModels;

namespace SharedLibrary.Interfaces.Services
{
    public interface IWaitingListImporterService
    {
        void ImportList(AccessGrantType accessGrantType, FileInfo xmlFileInfo);
        void ImportList(AccessGrantType accessGrantType, string xmlFileContent);
    }
}
