namespace WaitingListXMLParser.Tests
{
    public class Tests
    {
        XMLWaitingListParser parser;
        string filepath = "СписокПогрузкиРазгрузки.xml";
        [SetUp]
        public void Setup()
        {
            parser = new XMLWaitingListParser();
        }

        [Test]
        public void TestParseFromFile()
        {
            var result = parser.Parse(File.OpenRead(filepath));
            Assert.Pass();
        }
    }
}