using Ninject;
using Warehouse;
using Warehouse.CameraRoles;

var _kernel = new StandardKernel(new Container());
_kernel.Get<CameraRolesToDB>().UpdateDB();
_kernel.Get<WarehouseSystem>().Run();