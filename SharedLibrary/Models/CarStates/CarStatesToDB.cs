using SharedLibrary.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Models.CarStates
{
    public class CarStatesToDB
    {
        private readonly WarehouseContext db;
        private readonly List<CarStateBase> carStates;

        public CarStatesToDB(WarehouseContext db, List<CarStateBase> carStates)
        {
            this.db = db;
            this.carStates = carStates;
        }

        public void AddExistingCarStatesToDB()
        {
            // Drop table manual if any state changed
            foreach (var state in carStates)
                state.AddThatStateToDB(db);
            db.SaveChanges();
        }
    }
}
