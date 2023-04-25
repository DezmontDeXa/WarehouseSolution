﻿using Ninject;
using Warehouse;
using Warehouse.Models.CameraRoles;
using Warehouse.Services;

var _kernel = new StandardKernel(new Container());
_kernel.Get<CameraRolesToDB>().AddExistingCameraRolesToDB();
_kernel.Get<TimeControlService>();
_kernel.Get<WarehouseSystem>().Run();

Console.WriteLine("System started");
Console.WriteLine("Write \"exit\" for close app");

while (true)
{
    if (Console.ReadLine().ToLower() == "exit")
        return;
}