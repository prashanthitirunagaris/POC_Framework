using System;
using System.IO;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;


namespace NHS_Track_Trace_POC
{
    public static class App
    {
        public static AndroidDriver<AndroidElement> _driver;
        private static Uri testServerAddress = new Uri("http://127.0.0.1:4723/wd/hub");
        private static TimeSpan INIT_TIMEOUT_SEC = TimeSpan.FromSeconds(180);
        private static TimeSpan IMPLICIT_TIMEOUT_SEC = TimeSpan.FromSeconds(10);
        public static ISearchContext Driver => _driver;

        public static void Initialize()
        {
            AppiumOptions options = new AppiumOptions();
            options.PlatformName = "Android";
            options.AddAdditionalCapability("deviceName", "Pixel_2_API_30");
            //options.AddAdditionalCapability("deviceName", "Andriod Device");
            options.AddAdditionalCapability("appPackage", "uk.nhs.covid19.production");
            options.AddAdditionalCapability("appActivity", "uk.nhs.nhsx.covid19.android.app.MainActivity");
            Uri url = new Uri("http://127.0.0.1:4723/wd/hub");
            //_driver = new AndroidDriver<AndroidElement>(testServerAddress, options, INIT_TIMEOUT_SEC);
            _driver = new AndroidDriver<AndroidElement>(testServerAddress, options, INIT_TIMEOUT_SEC);
            _driver.Manage().Timeouts().ImplicitWait = IMPLICIT_TIMEOUT_SEC;
            
        }

        public static void Quit()
        {
            
            _driver.Quit();
        }

        public static void Goto(string page)
        {
           _driver.FindElementByAndroidUIAutomator("UiSelector().text(" + "\"" + page + "\")").Click();
        }

        public static string TakeScreenShot(string file)
        {
            DateTime date = DateTime.Now;
            string str= date.ToString().Replace("/", "").Replace(":","").Replace("{","").Replace("}","").Replace(" ","");

            Screenshot TSSScreenshot = ((ITakesScreenshot)App._driver).GetScreenshot();
            string Path = file + "\\Test_Execution_Reports\\" + str + ".png";
            TSSScreenshot.SaveAsFile(Path, ScreenshotImageFormat.Png);
            return Path;
        }

        public static void ClearFolder(string FolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                ClearFolder(di.FullName);
                di.Delete();
            }
        }
    }

}