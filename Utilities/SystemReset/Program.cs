using System.Collections.Generic;

try
{
    var service = new SystemResetAndInitializeService();
    service.ResetAndInitialize();
}catch(Exception ex)
{
    Console.WriteLine(ex);
    Console.WriteLine("Complete with error");
    Console.ReadLine();
}

Console.WriteLine("Complete");
Console.ReadLine();

