using NaisServiceLibrary;
using Ninject;
using TimeControlService;
using Warehouse;
using Warehouse.Models.CameraRoles;
using Warehouse.Models.CarStates;

var _kernel = new StandardKernel(new Container());
_kernel.Get<CameraRolesToDB>().AddExistingCameraRolesToDB();
_kernel.Get<CarStatesToDB>().AddExistingCarStatesToDB();
_kernel.Get<TimeControl>().RunAsync();
_kernel.Get<NaisService>().RunAsync();
_kernel.Get<WarehouseSystem>().RunAsync();

Console.WriteLine("System started");
Console.WriteLine("Write \"exit\" for close app");

while (true)
{
    if (Console.ReadLine().ToLower() == "exit")
        return;
}