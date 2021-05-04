using System;
using NHS_Track_Trace_POC.Libraries;

namespace NHS_Track_Trace_POC.AndroidPages
{
    public class NHSStartUpPageNavigation
    {
        private string GetNotifiedContinue = "//android.widget.Button[@text='Continue']";
        public string IAmOver16 = "//android.widget.Button[@text='I AM 16 OR OVER']";
        private string IAmUnder16 ="//android.widget.Button[@text='I AM UNDER 16']";
        private string AuthenticationIAgree = "//android.widget.Button[@text='I agree']";
        private string PostCode = "//android.widget.EditText[@resource-id='uk.nhs.covid19.production:id/postCodeEditText']";
        private string PostCodeContinue = "//android.widget.Button[@text='Continue']";
        private string LocalAuthority = "//android.widget.RadioButton[@index=0]";
        private string LocalAuthorityConfirm = "//android.widget.Button[@text='Confirm']";
        private string ContactTracingContinue = "//android.widget.Button[@text='Continue']";
        private string NotificationTurnOn ="//android.widget.Button[@text='Turn on']";
        


        public bool TrackTraceInitialNavigation(Boolean AreYou16OrOver, String PostCode)
        {
           // App._driver.FindElementByXPath(GetNotifiedContinue).Click();
            AndriodGeneric.GetWebElement(GetNotifiedContinue, AndriodGeneric.Locator.XPath).Click();

            if (AreYou16OrOver)
                AndriodGeneric.GetWebElement(IAmOver16, AndriodGeneric.Locator.XPath).Click();
            else
                AndriodGeneric.GetWebElement(IAmUnder16, AndriodGeneric.Locator.XPath).Click();


            AndriodGeneric.ScrollByCoOrdinates(8, -360);
            AndriodGeneric.ScrollByCoOrdinates(8, -360);
            AndriodGeneric.GetWebElement(AuthenticationIAgree, AndriodGeneric.Locator.XPath).Click();

            AndriodGeneric.GetWebElement(this.PostCode, AndriodGeneric.Locator.XPath).SendKeys(PostCode);
            AndriodGeneric.GetWebElement(PostCodeContinue, AndriodGeneric.Locator.XPath).Click();

            AndriodGeneric.ScrollByCoOrdinates(8, -200);

            AndriodGeneric.GetWebElement(LocalAuthority, AndriodGeneric.Locator.XPath).Click();
            AndriodGeneric.GetWebElement(LocalAuthorityConfirm, AndriodGeneric.Locator.XPath).Click();

            AndriodGeneric.GetWebElement(ContactTracingContinue, AndriodGeneric.Locator.XPath).Click();

            AndriodGeneric.GetWebElement(NotificationTurnOn, AndriodGeneric.Locator.XPath).Click();


            return true;
        }
    }
}
