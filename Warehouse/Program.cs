using Ninject;
using Warehouse;
using Warehouse.Interfaces.AppSettings;
using Warehouse.Interfaces.CameraRoles;
using Warehouse.Interfaces.CarStates;
using Warehouse.Interfaces.DataBase;
using Warehouse.Interfaces.TimeControl;
using Warehouse.Nais;

try
{
    var _kernel = new StandardKernel(new Container());
    _kernel.Get<IAppSettings>().Load();
    _kernel.Get<IWarehouseDataBaseMethods>().RegisterCameraRoles(_kernel.GetAll<ICameraRoleBase>());
    _kernel.Get<IWarehouseDataBaseMethods>().RegisterCarStates(_kernel.GetAll<ICarStateBase>());
    _kernel.Get<ITimeControler>().RunAsync();
    _kernel.Get<NaisService>().RunAsync();
    _kernel.Get<WarehouseSystem>().RunAsync();

    Console.WriteLine("System started");
    Console.WriteLine("Write \"exit\" for close app");

    while (true)
    {
        if (Console.ReadLine().ToLower() == "exit")
            return;
    }
}catch(Exception e)
{
    Console.WriteLine( e);
    Console.ReadLine();
}