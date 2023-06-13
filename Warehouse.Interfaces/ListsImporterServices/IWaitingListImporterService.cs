using Warehouse.Interfaces.DataBase;

namespace Warehouse.Interfaces.ListsImporterServices
{
    public interface IWaitingListImporterService
    {
        void ImportList(AccessGrantType accessGrantType, FileInfo xmlFileInfo);
        void ImportList(AccessGrantType accessGrantType, string xmlFileContent);
    }
}
