namespace PageObjectModel
{
    public class Tests
    {
        private EbayTestObject tester;
        [SetUp]
        public void Setup()
        {
            tester = new EbayTestObject();
            
        }

        [Test]
        public void Test1()
        {
           tester.Start();
           tester.Home.SearchBar.SearchFor("Mouse");
           tester.Results.GetAllItems();
        }

        [TearDown]
        public void Close()
        {
            tester.Close();
        }
    }
}