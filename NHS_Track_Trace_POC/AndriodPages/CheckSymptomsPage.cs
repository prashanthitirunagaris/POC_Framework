using System;
using NHS_Track_Trace_POC.Libraries;


namespace NHS_Track_Trace_POC.AndroidPages
{
    public class CheckSymptomsPage
    {
        private string CheckSymptomsTextView = "//android.widget.TextView[@text='Check symptoms']";
        private string SymptomFever = "//android.widget.CheckBox[@text='A high temperature (fever)']";
        private string SymptomCough = "//android.widget.CheckBox[@text='A new continuous cough']";
        private string SymptomLossOfTasteAndSmell = "//android.widget.CheckBox[@text='A change to your sense of smell or taste']";
        private string SymptomsContinue = "//android.widget.Button[@text='Continue']";
        //private string SymptomsStartDate=
        private string SymptomsDontRememberDate = "//android.widget.CheckBox[@text='I do not remember the date']";
        private string SymptomsSubmit = "//android.widget.Button[@text='Submit']";

        public void CheckSymptoms()
        {
            AndriodGeneric.GetWebElement(CheckSymptomsTextView, AndriodGeneric.Locator.XPath).Click();
            AndriodGeneric.GetWebElement(SymptomFever, AndriodGeneric.Locator.XPath).Click();
            AndriodGeneric.GetWebElement(SymptomCough, AndriodGeneric.Locator.XPath).Click();
            AndriodGeneric.ScrollByCoOrdinates(8, -360);
            
            AndriodGeneric.GetWebElement(SymptomLossOfTasteAndSmell, AndriodGeneric.Locator.XPath).Click();
            AndriodGeneric.GetWebElement(SymptomsContinue, AndriodGeneric.Locator.XPath).Click();

            AndriodGeneric.GetWebElement(SymptomsDontRememberDate, AndriodGeneric.Locator.XPath).Click();

            AndriodGeneric.GetWebElement(SymptomsSubmit, AndriodGeneric.Locator.XPath).Click();

            AndriodGeneric.ScrollByCoOrdinates(8, -360);
            AndriodGeneric.ScrollByCoOrdinates(8, -360);
            //AndriodGeneric.GetWebElement("//android.widget.Button[@text='Book a free test']").Click();
        }
       
    }
}
