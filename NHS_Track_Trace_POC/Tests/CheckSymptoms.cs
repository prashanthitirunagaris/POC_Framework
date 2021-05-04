using NHS_Track_Trace_POC.AndroidPages;
using NUnit.Framework;
using NUnit.Framework.Internal;
using NHS_Track_Trace_POC;


namespace Tests
{
    class CheckSymptoms : TestBase
    {
        [Test]
        
        public void CheckSymptomsTest()
        {
            //Start Report

            test= extent.CreateTest("CheckSymptomsTest").Info("Check COvid Symptoms");
            test.Log(AventStack.ExtentReports.Status.Info, "NHS Main screen Navigation");
            
            if (Pages.NHSAppNavigation.TrackTraceInitialNavigation(true, "TW14"))
            {
                test.Log(AventStack.ExtentReports.Status.Info, "CheckSmptoms");
                Pages.CheckSymptoms.CheckSymptoms();
                test.Log(AventStack.ExtentReports.Status.Pass, "Test Passed");
            }
        }

        [Test]
        public void TestDemo()
        {
            //Start Report

            test = extent.CreateTest("TestDemo").Info("DummyTest");
            test.Log(AventStack.ExtentReports.Status.Info, "DummyTest");
            Assert.False(true);
            /* if (Pages.NHSAppNavigation.TrackTraceInitialNavigation(true, "TW14"))
             {
                 test.Log(AventStack.ExtentReports.Status.Info, "CheckSmptoms");
                 Pages.CheckSymptoms.CheckSymptoms();
                 test.Log(AventStack.ExtentReports.Status.Pass, "Test Passed");
             }*/
        }
    }
}
