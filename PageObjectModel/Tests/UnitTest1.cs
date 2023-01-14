using OpenQA.Selenium.Chrome;

namespace PageObjectModel.Tests
{
    public class Tests
    {
        private EbayTestObject chromeTester, firefoxTester;
        private List<string> chromeOptions, firefoxOptions;

        [SetUp]
        public void Setup()
        {

            chromeOptions = new List<string>
            {
                "--headless"
            };
            firefoxOptions = new List<string> { };

            chromeTester = new EbayTestObject("chrome", chromeOptions);
            firefoxTester = new EbayTestObject("firefox", firefoxOptions);
           
        }

        [Test]
        public void GetAllPricesLargerThanInChrome()
        {
           chromeTester.Start();
           chromeTester.Home.SearchBar.SearchFor("Mouse");
           IList<double> prices = chromeTester.Results.GetAllPricesHigherThan(50);
           foreach (double price in prices)
           {
                Console.WriteLine(price);
           }
        }

        [Test]
        public void GetAllPricesLargerThanInFirefox()
        {
            firefoxTester.Start();
            firefoxTester.Home.SearchBar.SearchFor("Keyboard");
            IList<double> prices = firefoxTester.Results.GetAllPricesHigherThan(0);
            foreach (double price in prices)
            {
                Console.WriteLine(price);
            }
        }

        [TearDown]
        public void Close()
        {
            chromeTester.Close();
            firefoxTester.Close();
        }
    }
}