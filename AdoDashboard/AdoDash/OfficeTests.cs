using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace AdoDash
{
    [TestClass]
    public class OfficeTests
    {
        private Dictionary<string, object> sauceOptions;
        private IWebDriver _driver;
        private ChromeOptions chromeOptions;

        public TestContext TestContext { get; set; }
        public string sauceUserName { get; private set; }
        public string sauceAccessKey { get; private set; }

        [TestMethod]
        public void ShouldOpen()
        {
            var sauceOptions = new Dictionary<string, object>
            {
                ["username"] = sauceUserName,
                ["accessKey"] = sauceAccessKey,
                ["name"] = TestContext.TestName,
                ["extendedDebugging"] = true,
                ["capturePerformance"] = true
            };
            chromeOptions.AddAdditionalOption("sauce:options", sauceOptions);
            _driver = new RemoteWebDriver(new Uri("https://ondemand.saucelabs.com/wd/hub"),
                chromeOptions.ToCapabilities(), TimeSpan.FromSeconds(600));

            _driver.Navigate().GoToUrl("https://www.office.com/?auth=1");

            var user = Environment.GetEnvironmentVariable("365_USER", 
                EnvironmentVariableTarget.User);
            var pass = Environment.GetEnvironmentVariable("365_PASS", 
                EnvironmentVariableTarget.User);

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var userName = wait.Until(
                ExpectedConditions.ElementIsVisible(By.Id("i0116")));
            userName.SendKeys(user);

            var button = wait.Until(
                ExpectedConditions.ElementIsVisible(By.Id("idSIButton9")));
            button.Click();

            var password = wait.Until(
                ExpectedConditions.ElementIsVisible(By.Id("i0118")));
            password.SendKeys(pass);

            button = wait.Until(
                ExpectedConditions.ElementIsVisible(By.Id("idSIButton9")));
            button.Click();
        }
        [TestMethod]
        public void PerformanceTest()
        {
            var sauceOptions = new Dictionary<string, object>
            {
                ["username"] = sauceUserName,
                ["accessKey"] = sauceAccessKey,
                ["name"] = TestContext.TestName,
                ["extendedDebugging"] = true,
                ["capturePerformance"] = true
            };
            chromeOptions.AddAdditionalOption("sauce:options", sauceOptions);
            _driver = new RemoteWebDriver(new Uri("https://ondemand.saucelabs.com/wd/hub"),
                chromeOptions.ToCapabilities(), TimeSpan.FromSeconds(600));

            _driver.Navigate().GoToUrl("https://www.office.com/?auth=1");
        }
        [TestInitialize]
        public void SetupTests()
        {
            //TODO please supply your Sauce Labs user name in an environment variable
            sauceUserName = Environment.GetEnvironmentVariable("SAUCE_USERNAME", EnvironmentVariableTarget.User);
            //TODO please supply your own Sauce Labs access Key in an environment variable
            sauceAccessKey = Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);
            chromeOptions = new ChromeOptions()
            {
                BrowserVersion = "latest",
                PlatformName = "Windows 10",
                UseSpecCompliantProtocol = true
            };

        }
        [TestCleanup]
        public void CleanUpAfterEveryTestMethod()
        {
            var passed = TestContext.CurrentTestOutcome == UnitTestOutcome.Passed;
            if (_driver is null) return;
            ((IJavaScriptExecutor) _driver).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
            _driver?.Quit();
        }
    }
}
