using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace projectB
{
    [TestFixture]
    public class Tests
    {
        private object driver;

        public class GoolSearchTest
        {
            private IWebDriver driver;
            private Alert alert;
            private Window window;
        }

        [OneTimeSetUp]
        public void Setup()
        {
            string path = "C:\\Users\\user1\\Desktop\\lern2\\otomatzia\\projectB";

            driver = new ChromeDriver(path + @"\driver\");
            Alert alert = new Alert();
            Window window = new Window();

        }

        [Test]
        public void TestHandleAlert()
        {
           driver.Navigate().GoToUrl("https://demoqa.com/alerts");

            var alertButton = driver.FindElement(By.Id("timerAlertButton"));
            alertButton.Click();

            IAlert alert = WaitForAlert(driver, TimeSpan.FromSeconds(10));
            ClassicAssert.IsNotNull(alert, "Alert was not displayed.");
            alert.Accept();
        }
        [Test]
        public void TestSwitchBetweenWindowsAndTabs()
        {
            driver.Navigate().GoToUrl("https://demoqa.com/browser-windows");
            var windowButton = driver.FindElement(By.Id("windowButton"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();",windowButton);
            string originalWindow = driver.CurrentWindowHandle;
            WaitForNewWindow(driver, 2);
            foreach(string windowHandle in driver.windowHandles)
            {
                if(windowHandle != originalWindow)
                {
                    driver.SwitchTo().window(windowHandle);
                    break;
                }
            }

            var newTabHeading = driver.FindElement(By.Id("sampleHeading"));
            classicAssert.AreEqual("this is a sample page", newTabHeading.Text);

            driver.Close();
            driver.SwitchTo().Window(originalWindow);
        }



        [OneTimeTearDown]
        public void TearDown()
        {
            NewMethod();
        }

        private static void NewMethod()
        {
            driver.Dispose();
        }
    }
}