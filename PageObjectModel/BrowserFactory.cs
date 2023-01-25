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

        // Method which gets browser name to start, and a dictionary of the browser options, and start the requested driver
        public static IWebDriver GetDriver(string browserName, List<string> options)
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
                            IWebDriver fireFoxDriver = new FirefoxDriver(@".\Drivers", firefoxOptions);
                            drivers.Add("firefox", fireFoxDriver);
                            return fireFoxDriver;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("The firefox driver didn't start, did you set the right options?", ex.Message);
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
                            IWebDriver edgeDriver = new EdgeDriver(@".\Drivers", edgeOptions);
                            drivers.Add("ie", edgeDriver);
                            return edgeDriver;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("The edge driver didn't start, did you set the right options?", ex.Message);
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
                            IWebDriver chromeDriver = new ChromeDriver(@".\Drivers", chromeOptions);
                            drivers.Add("chrome", chromeDriver);
                            return chromeDriver;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("The chrome driver didn't start, did you set the right options?", ex.Message);
                        }
                    }
                    break;
            }
            return null;
        }

        // Method to load a specific url in the given driver
        public static void LoadApplication(string url, IWebDriver driver)
        {
            driver.Url = url;   
        }

        // Method to close the given driver
        public static void CloseDriver(IWebDriver driver)
        {
            driver.Close();
        }
    }
}

