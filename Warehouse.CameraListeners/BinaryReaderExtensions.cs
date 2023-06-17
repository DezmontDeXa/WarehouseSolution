using System.Text;

namespace Warehouse.CameraListeners
{

    public static class BinaryReaderExtension
    {

        public static string? ReadLine(this BinaryReader reader)
        {
            var result = new StringBuilder();
            bool foundEndOfLine = false;
            char ch;
            while (!foundEndOfLine)
            {
                try
                {
                    ch = reader.ReadChar();
                }
                catch (ArgumentException)
                {
                    continue;
                }
                catch (EndOfStreamException ex)
                {
                    if (result.Length == 0) return null;
                    else break;
                }

                if (ch == '\n' || ch == '\r')
                {
                    while (ch == '\n' || ch == '\r')
                    {
                        reader.ReadChar();
                        foundEndOfLine = true;
                        break;
                    }
                }
                else
                    result.Append(ch);


            }
            return result.ToString();
        }
    }
}
