using Ninject;
using Warehouse;
using Warehouse.Models.CameraRoles;

var _kernel = new StandardKernel(new Container());
_kernel.Get<CameraRolesToDB>().AddExistingCameraRolesToDB();
_kernel.Get<WarehouseSystem>().Run();