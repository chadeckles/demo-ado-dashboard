using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace AdoDash
{
    [TestClass]
    public class OfficeTests
    {
        private IWebDriver _driver;
        private ChromeOptions _chromeOptions;

        public TestContext TestContext { get; set; }
        public string SauceUserName { get; private set; }
        public string SauceAccessKey { get; private set; }

        [TestMethod]
        public void ShouldOpen()
        {
            var sauceOptions = new Dictionary<string, object>
            {
                ["username"] = SauceUserName,
                ["accessKey"] = SauceAccessKey,
                ["name"] = TestContext.TestName
            };
            _chromeOptions.AddAdditionalOption("sauce:options", sauceOptions);
            _driver = new RemoteWebDriver(new Uri("https://ondemand.saucelabs.com/wd/hub"),
                _chromeOptions.ToCapabilities(), TimeSpan.FromSeconds(30));

            var officePage = new OfficePage(_driver);
            officePage.GoTo();
            officePage.Login();
            officePage.IsLoaded().Should().BeTrue("we tried to login to the office application");
        }
        [TestMethod]
        public void ShouldHaveOneNote()
        {
            var sauceOptions = new Dictionary<string, object>
            {
                ["username"] = SauceUserName,
                ["accessKey"] = SauceAccessKey,
                ["name"] = TestContext.TestName
            };
            _chromeOptions.AddAdditionalOption("sauce:options", sauceOptions);
            _driver = new RemoteWebDriver(new Uri("https://ondemand.saucelabs.com/wd/hub"),
                _chromeOptions.ToCapabilities(), TimeSpan.FromSeconds(30));

            var officePage = new OfficePage(_driver);
            officePage.GoTo();
            officePage.Login();
            officePage.IsLoaded().Should().BeTrue("we tried to login to the office application");

            var officeHomePage = new OfficeHomePage(_driver);
            officeHomePage.OneNote.Displayed.Should().BeTrue();
        }
        [TestMethod]
        public void PerformanceTest()
        {
            var sauceOptions = new Dictionary<string, object>
            {
                ["username"] = SauceUserName,
                ["accessKey"] = SauceAccessKey,
                ["name"] = TestContext.TestName,
                ["extendedDebugging"] = true,
                ["capturePerformance"] = true
            };
            _chromeOptions.AddAdditionalOption("sauce:options", sauceOptions);
            _driver = new RemoteWebDriver(new Uri("https://ondemand.saucelabs.com/wd/hub"),
                _chromeOptions.ToCapabilities(), TimeSpan.FromSeconds(30));

            _driver.Navigate().GoToUrl("https://www.office.com/?auth=1");
        }
        [TestInitialize]
        public void SetupTests()
        {
            //TODO please supply your Sauce Labs user name in an environment variable
            SauceUserName = Environment.GetEnvironmentVariable("SAUCE_USERNAME", EnvironmentVariableTarget.User);
            //TODO please supply your own Sauce Labs access Key in an environment variable
            SauceAccessKey = Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);
            _chromeOptions = new ChromeOptions()
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
