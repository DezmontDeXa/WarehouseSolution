using Warehouse.Interfaces.DataBase;

namespace Warehouse.Interfaces.FindCarServices
{
    public interface IFindCarService
    {
        ICar FindCar(string plateNumber);
    }
}