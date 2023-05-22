using CheckPointControl.Views.Popups;
using Prism.Regions;
using SharedLibrary.DataBaseModels;
using System;
using System.Threading.Tasks;
using System.Windows.Threading;
using Warehouse.CheckPointClient.Core;

namespace CheckPointControl.Services
{
    public class NotifiesViewShowerService
    {
        private readonly NotifiesQueueService notifiesQueueService;
        private readonly IRegionManager regionManager;
        private readonly Dispatcher dispatcher;
        private bool _block = false;

        public InspectionRequiredCarNotify CurrentInspectionNotify;
        public UnknownCarNotify CurrentUnknownCarNotify;
        //TODO: реализовать ExpiredListCarNotifyPopup и NotInListCarNotifyPopup 
        //public ExpiredListCarNotify CurrentExpiredNotify;
        //public NotInListCarNotify CurrentNotInListCarNotify;

        public NotifiesViewShowerService(NotifiesQueueService notifiesQueueService, IRegionManager regionManager, Dispatcher dispatcher)
        {
            this.notifiesQueueService = notifiesQueueService;
            this.regionManager = regionManager;
            this.dispatcher = dispatcher;
            Task.Run(Working);
        }

        private void Working()
        {
            while (true)
            {
                while (_block) Task.Delay(100).Wait();

                try
                {
                    CurrentInspectionNotify = null;
                    CurrentUnknownCarNotify = null;
                    //CurrentNotInListCarNotify = null;

                    if (notifiesQueueService.InspectionQueue.TryDequeue(out var inspectionNotify))
                    {
                        _block = true;
                        CurrentInspectionNotify = inspectionNotify;
                        dispatcher.Invoke(() => regionManager.RequestNavigate(RegionNames.PopupRegion, nameof(InspectionRequiredPopup)));
                        continue;
                    }

                    if (notifiesQueueService.UnknownQueue.TryDequeue(out var unknownCarNotify))
                    {
                        _block = true;
                        CurrentUnknownCarNotify = unknownCarNotify;
                        dispatcher.Invoke(() => regionManager.RequestNavigate(RegionNames.PopupRegion, nameof(UnknownCarPopup)));
                        continue;
                    }

                    //if (notifiesQueueService.NotInlistQueue.TryDequeue(out var notInListCarNotify))
                    //{
                    //    _block = true;
                    //    CurrentNotInListCarNotify = notInListCarNotify;
                    //dispatcher.Invoke(() => regionManager.RequestNavigate(RegionNames.PopupRegion, nameof(UnknownCarPopup)));
                    //    continue;
                    //}


                    //CurrentExpiredNotify = null;

                    //if(notifiesQueueService.ExpiredQueue.TryDequeue(out var expiredNotify))
                    //{
                    //    _block = true;
                    //    CurrentExpiredNotify = expiredNotify;
                    //    dispatcher.Invoke(() => regionManager.RequestNavigate(RegionNames.PopupRegion, nameof(ExpiredListPopup)));
                    //    continue;
                    //}
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
        }

        public void Unblock()
        {
            _block = false;
        }
    }
}
