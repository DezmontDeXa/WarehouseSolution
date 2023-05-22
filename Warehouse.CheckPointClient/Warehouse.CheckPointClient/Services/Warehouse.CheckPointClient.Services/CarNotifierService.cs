using SharedLibrary.DataBaseModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class CarNotifierService
    {
        public event EventHandler<UnknownCarNotify> NewUnknownCarNotify;
        public event EventHandler<NotInListCarNotify> NewNotInListCarNotify;
        public event EventHandler<InspectionRequiredCarNotify> NewInspectionRequiredCarNotify;
        public event EventHandler<ExpiredListCarNotify> NewExpiredListCarNotify;

        public CarNotifierService()
        {
            Task.Run(Working);
        }

        private void Working()
        {
            using (var db = new WarehouseContext())
            {
                foreach (var notify in db.UnknownCarNotifies.Where(x => !x.IsProcessed))
                {
                    NewUnknownCarNotify?.Invoke(this, notify);
                    notify.IsProcessed = true;
                }
                foreach (var notify in db.NotInListCarNotifies.Where(x => !x.IsProcessed))
                {
                    NewNotInListCarNotify?.Invoke(this, notify);
                    notify.IsProcessed = true;
                }
                foreach (var notify in db.InspectionRequiredCarNotifies.Where(x => !x.IsProcessed))
                {
                    NewInspectionRequiredCarNotify?.Invoke(this, notify);
                    notify.IsProcessed = true;
                }
                foreach (var notify in db.ExpiredListCarNotifies.Where(x => !x.IsProcessed))
                {
                    NewExpiredListCarNotify?.Invoke(this, notify);
                    notify.IsProcessed = true;
                }

                db.SaveChanges();
            }

            Task.Delay(1000).Wait();
        }
    }
}
