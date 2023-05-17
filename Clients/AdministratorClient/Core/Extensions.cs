using System;
using System.Collections.Generic;
using System.Linq;

namespace AdministratorClient.Core
{
    public static class Extensions
    {
        public static IEnumerable<T> GetEnumTypes<T>() where T : Enum
            => Enum.GetValues(typeof(T)).Cast<T>() ;
    }
}
