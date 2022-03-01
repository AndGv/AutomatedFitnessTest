using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace TestAutomation
{
    public class FitnessTest
    {
        public static IWebDriver driver;

        [OneTimeSetUp]

        public static void OneTimeSetUp()
        {
            driver = new ChromeDriver();
            driver.Url = "https://www.active.com/fitness/calculators/pace";
            driver.Manage().Window.Maximize();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.ClassName("icon-awesome-close-btn")).Displayed);
            driver.FindElement(By.ClassName("icon-awesome-close-btn")).Click();

            Thread.Sleep(2000);
            driver.FindElement(By.ClassName("accept-cookies-button")).Click();
        }

        [OneTimeTearDown]
        public static void OneTimeTearDown()
        {
            driver.Quit();
        }


        [TestCase("1", "5", "13", TestName = "13km + 1h + 5min =5km/h ")]
        public static void PaceTest(string hours, string min, string length)
        {
            IWebElement hoursInputField = driver.FindElement(By.CssSelector("#calculator-pace > form > div:nth-child(2) > div > label:nth-child(1) > input[type=number]"));
            hoursInputField.SendKeys(hours);
            IWebElement minInputField = driver.FindElement(By.CssSelector("#calculator-pace > form > div:nth-child(2) > div > label:nth-child(2) > input[type=number]"));
            minInputField.SendKeys(min);
            IWebElement lengthInputField = driver.FindElement(By.CssSelector("#calculator-pace > form > div:nth-child(3) > div > label > input[type=number]"));
            lengthInputField.SendKeys(length);

            driver.FindElement(By.CssSelector("#calculator-pace span[name=distance_type]")).Click();
            driver.FindElement(By.CssSelector("#calculator-pace > form > div:nth-child(3) > div > span > ul > li.selectboxit-option.selectboxit-option-first")).Click();

            driver.FindElement(By.CssSelector("#calculator-pace span[name=pace_type]")).Click();
            driver.FindElement(By.CssSelector("#calculator-pace > form > div:nth-child(4) > div > span > ul > li.selectboxit-option.selectboxit-option-first")).Click();



            driver.FindElement(By.CssSelector("#calculator-pace > form > div:nth-child(6) > div > a")).Click();

            IWebElement paceHours = driver.FindElement(By.Name("pace_hours"));
            IWebElement paceMinutes = driver.FindElement(By.Name("pace_minutes"));
            IWebElement paceSeconds = driver.FindElement(By.Name("pace_seconds"));

            Assert.AreEqual("00", paceHours.GetAttribute("value"), "Actual pace hours differs from expected");
            Assert.AreEqual("05", paceMinutes.GetAttribute("value"), "Actual pace minutes differs from expected");
            Assert.AreEqual("00", paceSeconds.GetAttribute("value"), "Actual pace seconds differs from expected");

        }






    }
}
