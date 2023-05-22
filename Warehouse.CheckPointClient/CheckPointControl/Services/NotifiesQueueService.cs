using Services;
using SharedLibrary.DataBaseModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPointControl.Services
{
    public class NotifiesQueueService
    {
        public Queue<InspectionRequiredCarNotify> InspectionQueue;
        public Queue<ExpiredListCarNotify>  ExpiredQueue;
        public Queue<UnknownCarNotify> UnknownQueue;
        public Queue<NotInListCarNotify> NotInlistQueue;

        public NotifiesQueueService(CarNotifierService carNotifierService)
        {
            InspectionQueue = new Queue<InspectionRequiredCarNotify>();
            ExpiredQueue = new Queue<ExpiredListCarNotify>();
            UnknownQueue = new Queue<UnknownCarNotify>();
            NotInlistQueue = new Queue<NotInListCarNotify>();

            carNotifierService.NewNotInListCarNotify += OnNewNotInListCarNotify;
            carNotifierService.NewUnknownCarNotify += OnNewUnknownCarNotify; ;
            carNotifierService.NewExpiredListCarNotify += OnNewExpiredListCarNotify; ;
            carNotifierService.NewInspectionRequiredCarNotify += OnNewInspectionRequiredCarNotify;
        }

        private void OnNewInspectionRequiredCarNotify(object sender, InspectionRequiredCarNotify e)
        {
            InspectionQueue.Enqueue(e);
        }

        private void OnNewExpiredListCarNotify(object sender, ExpiredListCarNotify e)
        {
            ExpiredQueue.Enqueue(e);
        }

        private void OnNewUnknownCarNotify(object sender, UnknownCarNotify e)
        {
            UnknownQueue.Enqueue(e);
        }

        private void OnNewNotInListCarNotify(object sender, NotInListCarNotify e)
        {
            NotInlistQueue.Enqueue(e);
        }
    }
}
