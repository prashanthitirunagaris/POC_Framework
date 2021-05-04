using SeleniumExtras.PageObjects;

namespace NHS_Track_Trace_POC.AndroidPages
{
    
    public static class Pages
    {
        private static T GetPage<T>() where T : new()
        {
            var page = new T();
            PageFactory.InitElements(App.Driver, page);
            return page;

        }
        //Application page definitions
        
        public static NHSStartUpPageNavigation NHSAppNavigation => GetPage<NHSStartUpPageNavigation>();

        public static CheckSymptomsPage CheckSymptoms => GetPage<CheckSymptomsPage>();
    }
}