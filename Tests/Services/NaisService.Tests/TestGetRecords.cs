

namespace NaisServiceLibrary.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            using(var db = new NaisDataBase())
            {
                var records = db.GetRecordsAsync(DateTime.Now).Result;
            }
            Assert.Pass();
        }
    }
}