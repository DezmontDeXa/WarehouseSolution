using SharedLibrary.DataBaseModels;

namespace WaitingListXMLParser
{
    public interface IWaitingListParser
    {
        WaitingList Parse(Stream fileStream);
        WaitingList Parse(string fileContent);
    }
}