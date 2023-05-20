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

            foreach (var file in Directory.GetFiles("TestData"))
            {
                importerService.ImportList(
                    SharedLibrary.DataBaseModels.AccessGrantType.Tracked,
                    new FileInfo(file));

            }

            Assert.Pass();
        }
    }
}