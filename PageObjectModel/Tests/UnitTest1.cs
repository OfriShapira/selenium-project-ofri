using OpenQA.Selenium.Chrome;

namespace PageObjectModel.Tests
{
    public class Tests
    {
        private EbayTestObject tester;
        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            tester = new EbayTestObject("chrome", options);
        }

        [Test]
        public void Test1()
        {
           tester.Start();
           tester.Home.SearchBar.SearchFor("mouse");
           IList<double> prices = tester.Results.GetAllPricesHigherThan(50);
           foreach (double price in prices)
           {
                Console.WriteLine(price);
           }
        }

        [TearDown]
        public void Close()
        {
            tester.Close();
        }
    }
}