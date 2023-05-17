using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using SharedLibrary.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace AdministratorClient
{
    public class VM : BindableBase
    {
        private ObservableCollection<WaitingList> waitingLists;
        private WaitingList selectedWaitingList;
        private ObservableCollection<Car> cars;
        private DelegateCommand uploadCarsCommand;
        private DelegateCommand uploadFreeCarsCommand;

        public ObservableCollection<WaitingList> WaitingLists { get => waitingLists; set => SetProperty(ref waitingLists, value); }
        public WaitingList SelectedWaitingList
        {
            get => selectedWaitingList;
            set
            {
                SetProperty(ref selectedWaitingList, value);
                Cars = new ObservableCollection<Car>(SelectedWaitingList.Cars);
            }
        }
        public ObservableCollection<Car> Cars { get => cars; set => SetProperty(ref cars, value); }
        public ICommand UploadCarsCommand => uploadCarsCommand ??= new DelegateCommand(UploadCars);
        public ICommand UploadFreeCarsCommand => uploadFreeCarsCommand ??= new DelegateCommand(UploadFreeCars);

        public VM()
        {
            using (var db = new WarehouseContext())
            {
                waitingLists = new ObservableCollection<WaitingList>(db.WaitingLists.Include(x => x.Cars).ThenInclude(x => x.CarState));
            }
        }


        private void UploadCars()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                UploadCarFromFile(ofd.FileName);
            }
        }

        private void UploadCarFromFile(string fileName)
        {
            var lines = File.ReadAllLines(fileName);
            var cars = new List<Car>();
            foreach (var line in lines)
                cars.Add(new Car() { PlateNumberForward = line, PlateNumberBackward = line, CarStateId = 0 });

            using (var db = new WarehouseContext())
            {
                var waitingListId = WaitingLists[1].Id;

                var waitingList = db.WaitingLists.First(x => x.Id == waitingListId);
                waitingList.Cars.AddRange(cars);
                db.SaveChanges();
            }
        }

        private void UploadFreeCars()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                var lines = File.ReadAllLines(ofd.FileName);
                var cars = new List<Car>();
                foreach (var line in lines)
                    cars.Add(new Car() { PlateNumberForward = line, PlateNumberBackward = line, CarStateId = 0 });

                using (var db = new WarehouseContext())
                {
                    var waitingListId = WaitingLists[0].Id;

                    var waitingList = db.WaitingLists.First(x => x.Id == waitingListId);
                    waitingList.Cars.AddRange(cars);
                    db.SaveChanges();
                }
            }
        }
    }
}
