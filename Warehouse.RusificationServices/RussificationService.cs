using Warehouse.Interfaces.RusificationServices;

namespace Warehouse.RusificationServices
{
    public class RussificationService : IRussificationService
    {
        public string ToRu(string enStr)
        {
            if (enStr == null) return "";
            if (enStr.Length == 0) return "";

            var _input = enStr;
            var ru = "АВЕКМНОРСТУХ";
            var en = "ABEKMHOPCTYX";

            var inputArray = _input.ToCharArray();
            for (var i = 0; i < inputArray.Length; i++)
            {
                var ch = _input[i];
                if (en.Contains(ch))
                    inputArray[i] = ru[en.IndexOf(ch)];
            }

            return string.Join("", inputArray);
        }
    }
}