using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace Amazon01.TestCases
{
    class AmazonAssertPrice
    {
        
        IWebDriver driver;

        [SetUp]
        public void SetUpTest()
        {
            // Set up Chrome driver
            driver = new ChromeDriver("C:\\Driver\\");
            driver.Manage().Window.Maximize();           
        }

        [Test]
        public void AssertLaptopPrice()
        {
            // Navigate to Amazon
            driver.Navigate().GoToUrl("https://www.amazon.com/");

            // Check if Captcha screen displayed, if yes wait 10 sec to manually enter captcha
            try
            {
                Boolean IsCaptchaDisplayed = driver.FindElement(By.Id("captchacharacters")).Displayed;
                if (IsCaptchaDisplayed)
                {
                    Console.WriteLine("Captcha screen displayed." + IsCaptchaDisplayed);
                    System.Threading.Thread.Sleep(10000);
                }
            }
            catch (Exception e)
            {
                //No captcha screen. Test can continue as expected.
            }                   

            // Find the search text box and type "laptop"
            IWebElement searchBox = driver.FindElement(By.Id("twotabsearchtextbox"));
            searchBox.SendKeys("laptop");

            // Find the search button and click it
            IWebElement searchButton = driver.FindElement(By.Id("nav-search-submit-button"));
            searchButton.Click();

            // Wait for search results to load
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(driver => driver.Title.Equals("Amazon.com : laptop"));

            // Find the first search result and click it
            IWebElement firstResult = driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div[1]/div[1]/div/span[1]/div[1]/div[2]/div/div/div/div/div/div/div/div[1]/div/div[2]/div/span/a/div/img"));            
            firstResult.Click();                              

            // Find the laptop price element
            IWebElement priceElement = driver.FindElement(By.Id ("corePrice_feature_div"));

            // Get the price text then replace newline with '.'            
            string priceText = priceElement.Text.Replace(Environment.NewLine, ".");

            // Convert the price string to a number
            if (decimal.TryParse(priceText.Replace("$", ""), out decimal laptopPrice))
            {
                Console.WriteLine("laptopPrice: " + laptopPrice);

                // Assert that the price is more than $100
                Assert.Greater(laptopPrice, 100);
            }
            else
            {
                Console.WriteLine("laptopPrice: " + laptopPrice);
                Assert.Fail("Unable to parse laptop price.");
            }
        }

        [TearDown]
        public void EndTest()
        {
            // Close the browser
            driver.Quit();        
        }
    }
}
