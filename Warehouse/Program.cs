using Ninject;
using Warehouse;

var _kernel = new StandardKernel(new Container());
_kernel.Get<WarehouseSystem>().Run();