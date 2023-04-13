using Ninject;
using Warehouse;
using Warehouse.Initializers;

var _kernel = new StandardKernel(new Container());
_kernel.Get<CameraRolesInitializer>().Initialize();
_kernel.Get<WarehouseSystem>().Run();