using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Extensions
{
    public static class StringExtensions
    {
        public static string TransliterateToRu(string input)
        {
            var _input = input;
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
