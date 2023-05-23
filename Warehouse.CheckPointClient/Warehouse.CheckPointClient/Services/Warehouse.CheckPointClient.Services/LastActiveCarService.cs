using SharedLibrary.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LastActiveCarService
    {
        public Car? LastCar { get; private set; }

        public event EventHandler<Car> LastActiveCarChanged;

        public LastActiveCarService()
        {
            Task.Run(Working);
        }

        private void Working()
        {
            while (true)
            {
                using(var db = new WarehouseContext())
                {
                }

                Task.Delay(1000).Wait();
            }
        }
    }
}
