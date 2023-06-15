using Warehouse.Interfaces.DataBase;
using Warehouse.Interfaces.DataBase.Configs;

namespace Warehouse.Processors.Car
{
    public class CarInfo
    {
        public ICamera? Camera { get; }
        public MoveDirection DetectedDirection { get; }
        public string RecognizedPlateNumber { get; }


        public string? NormalizedPlateNumber { get; internal set; }
        public ICar? Car { get; internal set; }
        public ICarStateType State { get; internal set; }

        public CarInfo(ICamera camera, MoveDirection detectedDirection, string recognizedPlateNumber)
        {
            Camera = camera ?? throw new ArgumentNullException(nameof(camera));
            DetectedDirection = detectedDirection;
            RecognizedPlateNumber = recognizedPlateNumber ?? throw new ArgumentNullException(nameof(camera));
        }

    }
}