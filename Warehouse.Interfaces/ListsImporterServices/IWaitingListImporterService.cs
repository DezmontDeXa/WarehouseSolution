using Warehouse.Interfaces.DataBase;

namespace Warehouse.Interfaces.ListsImporterServices
{
    public interface IWaitingListImporterService
    {
        void ImportList(AccessType accessGrantType, FileInfo xmlFileInfo);
        void ImportList(AccessType accessGrantType, string xmlFileContent);
    }
}
