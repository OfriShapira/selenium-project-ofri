using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Edge;

namespace PageObjectModel
{
    class BrowserFactory
    {
        private static IDictionary<string, IWebDriver> drivers = new Dictionary<string, IWebDriver>();
        private static IWebDriver driver;

        public IWebDriver Driver
        {
            get
            {
                if (driver == null)
                    throw new NullReferenceException("The WebDriver browser instance was not initialized. You should first call the method InitBrowser.");
                return driver;
            }
            private set
            {
                driver = value;
            }
        }

        public IDictionary<string, IWebDriver> Drivers
        {
            get
            {
                return drivers;
            }
            private set
            {
                drivers = value;
            }
        }

        public IWebDriver InitBrowser(string browserName, List<string> options)
        {
            switch (browserName.ToLower())
            {
                case "firefox":
                    if (!drivers.ContainsKey("firefox"))
                    {
                        FirefoxOptions firefoxOptions = new FirefoxOptions();
                        firefoxOptions.AddArguments(options);
                        try
                        {
                            driver = new FirefoxDriver(@".\Drivers", firefoxOptions);
                            drivers.Add("firefox", driver);
                            return driver;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("The firefox driver didn't start, did you put the right options?", ex.Message);
                        }

                    }
                    break;
                case "ie":
                    if (!drivers.ContainsKey("ie"))
                    {
                        EdgeOptions edgeOptions = new EdgeOptions();
                        edgeOptions.AddArguments(options);
                        try
                        {
                            driver = new EdgeDriver(@".\Drivers", edgeOptions);
                            drivers.Add("ie", driver);
                            return driver;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("The edge driver didn't start, did you put the right options?", ex.Message);
                        }

                    }
                    break;
                case "chrome":
                default:
                    if (!drivers.ContainsKey("chrome"))
                    {
                        ChromeOptions chromeOptions = new ChromeOptions();
                        chromeOptions.AddArguments(options);
                        try
                        {
                            driver = new ChromeDriver(@".\Drivers", chromeOptions);
                            drivers.Add("chrome", driver);
                            return driver;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("The chrome driver didn't start, did you put the right options?", ex.Message);
                        }
                    }
                    break;
            }
            return null;
        }


        public void LoadApplication(string url, string driverName)
        {

            if (Drivers.Count() > 0)
            {
                if (drivers.ContainsKey(driverName))
                {
                    drivers[driverName].Url = url;
                }
            }
        }

/*        public void CloseAllDrivers()
        {
            foreach (var key in drivers.Keys)
            {
                drivers[key].Close();
                drivers[key].Close();
            }
        }*/
    }
}

