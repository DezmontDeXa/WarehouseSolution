using Warehouse.Interfaces.FindCarServices;
using Warehouse.Interfaces.DataBase;
using FuzzySharp;
using FuzzySharp.SimilarityRatio;
using FuzzySharp.SimilarityRatio.Scorer.StrategySensitive;

namespace Warehouse.FindCarServices
{
    public class FuzzyFindCarService : IFindCarService
    {
        private readonly IWarehouseDataBaseMethods dbMethods;

        public int MinScore { get; set; } = 85;

        public FuzzyFindCarService(IWarehouseDataBaseMethods dbMethods)
        {
            this.dbMethods = dbMethods;
        }

        public ICar FindCar(string plateNumber)
        {
            var allCars = dbMethods.GetCars();

            if (allCars.Count() == 0) return null;

            var extractedResult = Process.ExtractOne(plateNumber.ToUpper(), allCars.Select(x => x.PlateNumberForward), s => s.ToUpper(), ScorerCache.Get<DefaultRatioScorer>());
            if (extractedResult.Score < MinScore) return null;
            return allCars.Skip(extractedResult.Index).First();
        }

    }
}