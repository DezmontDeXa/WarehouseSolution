using Warehouse.Interfaces.CamerasListener;
using Warehouse.Interfaces.DataBase;
using Warehouse.Interfaces.DataBase.Configs;
using Warehouse.Interfaces.WaitingListServices;

namespace Warehouse.Processors.Car.Core
{
    public class CarInfo
    {
        public ICamera? Camera { get; }
        public ICameraNotifyBlock AnprBlock { get; }
        public ICameraNotifyBlock PictureBlock { get; }


        public string? RecognizedPlateNumber { get; internal set; }
        public string? NormalizedPlateNumber { get; internal set; }
        public ICar? Car { get; internal set; }
        public ICarStateType State { get; internal set; }
        public ICollection<IWaitingList> WaitingLists { get; internal set; }
        public AccessType AccessType { get; internal set; }
        public IEnumerable<string> Purposes { get; internal set; }
        public bool AnotherAreaProgress { get; internal set; }
        public string? MoveDirectionString { get; internal set; }
        public bool HasFirstWeightning { get; internal set; }
        public bool HasSecondWeightning { get; internal set; }

        public CarInfo(ICamera camera, ICameraNotifyBlock anprBlock, ICameraNotifyBlock pictureBlock)
        {
            Camera = camera;
            AnprBlock = anprBlock;
            PictureBlock = pictureBlock;
        }
    }
}