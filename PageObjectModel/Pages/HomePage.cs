using OpenQA.Selenium;
using PageObjectModel.Components;

namespace PageObjectModel.Pages
{
    class HomePage
    {
        private IWebDriver driver;
        public SearchBar SearchBar;
        public Header Header;
        public Categories Categories;

        public HomePage(IWebDriver webDriver)
        {
            driver = webDriver;
            SearchBar = new SearchBar(driver);
            Header = new Header();
            Categories = new Categories();
        }

    }
}
