﻿using Prism.Commands;
using Prism.Mvvm;
using SharedLibrary.DataBaseModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace WeighingControlClient
{
    public class VM : BindableBase
    {
        public static Dispatcher Dispatcher { get; set; }

        private ObservableCollection<Car> awaitingCars;
        private DelegateCommand sendToArmavirCommand;
        private DelegateCommand sendToGencenaCommand;
        private Car selectedCar;
        private Func<WarehouseContext, CarState> _waitingSecondWeightingOnArmavirState;
        private Func<WarehouseContext, CarState> _exitPassGrantedState;
        private int _gercenaAreaId = 2;

        public ObservableCollection<Car> AwaitingCars { get => awaitingCars; set => SetProperty(ref awaitingCars, value); }
        public Car SelectedCar { get => selectedCar; set => SetProperty(ref selectedCar, value); } 
        public ICommand SendToArmavirCommand => sendToArmavirCommand ??= new DelegateCommand(SendToArmavir);
        public ICommand SendToGencenaCommand => sendToGencenaCommand ??= new DelegateCommand(SendToGencena);

        public VM()
        {
            awaitingCars = new ObservableCollection<Car>();

            _waitingSecondWeightingOnArmavirState = new Func<WarehouseContext, CarState>((db) => db.CarStates.First(x => x.Name == "Ожидает второе взвешивание" && x.AreaId == 1));
            _exitPassGrantedState = new Func<WarehouseContext, CarState>((db) => db.CarStates.First(x => x.Name == "Выезд разрешен" && x.AreaId == 1));

            Task.Run(AwaitingListUpdating);
        }

        private void AwaitingListUpdating()
        {
            while (true)
            {
                UpdateAwaitingCars();
                Task.Delay(1000).Wait();
            }
        }

        private void UpdateAwaitingCars()
        {
            using (var db = new WarehouseContext())
            {
                var carsInDb = db.Cars.Where(x => x.CarState.Name == "Ожидает первое взвешивание" || x.CarState.Name == "Первое взвешивание").ToList();
                foreach (var carInDb in carsInDb)
                {
                    var existCar = awaitingCars.FirstOrDefault(x => x.Id == carInDb.Id);
                    if (existCar == null)
                        Dispatcher.Invoke(() => awaitingCars.Add(carInDb));
                }

                foreach (var existCar in awaitingCars.ToArray())
                {
                    if (!carsInDb.Any(x => x.Id == existCar.Id))
                        Dispatcher.Invoke(() => awaitingCars.Remove(existCar));
                }
            }
        }

        private void SendToArmavir()
        {
            if (selectedCar == null) return;

            using (var db = new WarehouseContext())
            {
                var carInDb = db.Cars.First(x => x.Id == selectedCar.Id);
                carInDb.CarState = _waitingSecondWeightingOnArmavirState.Invoke(db);
                db.SaveChanges();
            }
            UpdateAwaitingCars();
        }


        private void SendToGencena()
        {
            if (selectedCar == null) return;

            using (var db = new WarehouseContext())
            {
                var carInDb = db.Cars.First(x => x.Id == selectedCar.Id);
                carInDb.CarState = _exitPassGrantedState.Invoke(db);
                carInDb.CarStateContext = $"Translate to {db.Areas.FirstOrDefault(x=>x.Id == _gercenaAreaId).Name}";
                db.SaveChanges();
            }
            UpdateAwaitingCars();
        }


    }
}
