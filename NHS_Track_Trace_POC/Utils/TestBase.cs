using NHS_Track_Trace_POC;
using NUnit.Framework;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using System;
using System.IO;
using System.Reflection;
using AventStack.ExtentReports.Reporter.Configuration;

namespace NHS_Track_Trace_POC
{
    [TestFixture]
    public class TestBase
    {
        public AventStack.ExtentReports.ExtentReports extent= null;
        public ExtentV3HtmlReporter reporter;
        public ExtentTest test;
        public string dir;
        public void ExtentReportsHelper()
        {
            extent = new AventStack.ExtentReports.ExtentReports();
            reporter = new ExtentV3HtmlReporter(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ExtentReports.html"));
            reporter.Config.DocumentTitle = "Automation Testing Report";
            reporter.Config.ReportName = "Regression Testing";
            reporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            extent.AttachReporter(reporter);
            extent.AddSystemInfo("Application Under Test", "nop Commerce Demo");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("Machine", Environment.MachineName);
            extent.AddSystemInfo("OS", Environment.OSVersion.VersionString);
        }

        [OneTimeSetUp]
        public void BeforeClass()
        {
            // create a test report directory and attach reporter
            extent = new AventStack.ExtentReports.ExtentReports();
            
            dir = Directory.GetCurrentDirectory().Replace("\\bin\\Debug\\net5.0", "");
            Environment.CurrentDirectory = dir;
            if(Directory.Exists(dir + "\\Test_Execution_Reports"))
                App.ClearFolder(dir + "\\Test_Execution_Reports");
            
            DirectoryInfo di = Directory.CreateDirectory(dir + "\\Test_Execution_Reports");
            var htmlReporter = new ExtentHtmlReporter(dir + "\\Test_Execution_Reports\\Automation_Report.html");
            
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Application Under Test", "nop Commerce Demo");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("Machine", Environment.MachineName);
            extent.AddSystemInfo("OS", Environment.OSVersion.VersionString);
            htmlReporter.Config.DocumentTitle = "NHS Covid-19 Track and Trace";
            htmlReporter.Config.ReportName = "Automation Report for NHS Track & Trace APP";
            htmlReporter.Config.Reporter.Start();
            htmlReporter.Config.Theme = Theme.Standard;
        }

    


        [SetUp]
        public static void Initialize()
        {
            App.Initialize();
        }

        [TearDown]
        public void Cleanup()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = "" + TestContext.CurrentContext.Result.StackTrace + "";
            var errorMessage = TestContext.CurrentContext.Result.Message;
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    test.Log(logstatus, "Test ended with" + logstatus + "-" + errorMessage);
                    test.Log(logstatus, "screenshot");
                    string PathOfScreenshot= App.TakeScreenShot(dir);
                    test.Fail(errorMessage, MediaEntityBuilder.CreateScreenCaptureFromPath(PathOfScreenshot).Build());
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }
            App.Quit();
        }
        [OneTimeTearDown]
        public void AfterClass()
        {
            extent.Flush();
           
        }
    }
}
