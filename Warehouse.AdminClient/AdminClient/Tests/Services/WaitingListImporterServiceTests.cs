using AdminClient.Services;

namespace Services
{
    public class WaitingListImporterServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ImportTest()
        {
            var importerService = new WaitingListImporterService();
            importerService.ImportList( 
                SharedLibrary.DataBaseModels.AccessGrantType.Tracked, 
                new FileInfo("TestData/СписокПогрузкиРазгрузки (3).xml"));
            Assert.Pass();
        }
    }
}