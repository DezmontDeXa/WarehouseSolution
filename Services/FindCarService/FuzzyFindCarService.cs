using FuzzySharp;
using FuzzySharp.SimilarityRatio.Scorer.StrategySensitive;
using FuzzySharp.SimilarityRatio;
using SharedLibrary.DataBaseModels;

namespace Warehouse.Services
{
    public class FuzzyFindCarService
    {
        public int MinScore { get; set; } = 85;

        public Car FindCar(string plateNumber)
        {
            plateNumber = TransliterateToRu(plateNumber);

            using (var db = new WarehouseContext())
            {
                var allCars = db.Cars.ToList();
                if (allCars.Count == 0) return null;
                var extractedResult = Process.ExtractOne(plateNumber.ToUpper(), allCars.Select(x=>x.PlateNumberForward), s => s.ToUpper(), ScorerCache.Get<DefaultRatioScorer>());
                if (extractedResult.Score < MinScore) return null;
                return allCars[extractedResult.Index];
            }

            // А, В, Е, К, М, Н, О, Р, С, Т, У и Х
        }

        private string TransliterateToRu(string input)
        {
            var ru = "АВЕКМНОРСТУХ";
            var en = "ABEKMHOPCTYX";

            var inputArray = input.ToCharArray();
            for (var i = 0; i < inputArray.Length; i++)
            {
                var ch = input[i];
                if (en.Contains(ch))
                    inputArray[i] = ru[en.IndexOf(ch)];
            }

            return string.Join("", inputArray);
        }
    }
}