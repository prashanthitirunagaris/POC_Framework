using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using OpenQA.Selenium.Appium.MultiTouch;

namespace NHS_Track_Trace_POC.Libraries
{
    public class AndriodGeneric
    {
        public static TouchAction ts = new TouchAction(App._driver);
        public enum Locator
        {
            XPath,
            Id,
            Name,
            ClassName,
            PartialLinkText,
            LinkText,
            CssSelector,
            TagName,
            PartialId,
            PartialName,
            PartialClass
        }

        public static string gbReturnValue;
        public static bool flag = false;

        public static bool WaitForElement(By by)
        {

            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(App._driver);
            fluentWait.Timeout = TimeSpan.FromMinutes(10);
            fluentWait.PollingInterval = TimeSpan.FromSeconds(2);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            IWebElement searchResult = null;
            try
            {
                searchResult = fluentWait.Until(x =>
                {
                    IWebElement webElement = null;
                    try
                    {
                        webElement = x.FindElement(by);
                    }
                    catch (NoSuchElementException) //makes sure NoSuchElement does not stop the execution before Until takes the hand
                    {

                    }
                    catch (UnhandledAlertException)
                    {
                    }
                    return webElement;
                });
            }
            catch (WebDriverTimeoutException) //refresh because sometimes bootstrap upgrade causes error 503 in a page, refresh is required
            {
                App._driver.Navigate().Refresh();
            }
            try
            {
                searchResult = App._driver.FindElement(by);
                return searchResult.Displayed;
            }
            catch (Exception ex) when (
                            ex is StaleElementReferenceException
                            || ex is NoSuchElementException)
            {
                System.Threading.Thread.Sleep(1000);
                searchResult = App._driver.FindElement(by);
                return searchResult.Displayed;
            }
            catch (UnhandledAlertException)
            {
                return false;
            }
        }

        public static bool IsTestTextPresent(string text)
        {
            try
            {
                App._driver.FindElement(By.XPath("//*[contains(text(), " + text + "')]"));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static IWebElement GetWebElement(string element, Locator locator)
        {
            try
            {
                return App._driver.FindElement(SetbyElement(locator, element));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static void ScrollByCoOrdinates(int x, int y)
        {
            try
            {
                
                ts.Press(207, 710).MoveTo(x, y).Release().Perform();
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static By SetbyElement(Locator LocatorType, String LocatorText)
        {

            switch (LocatorType)
            { //determine which locator item we are interested in
                case Locator.XPath:
                    return By.XPath(LocatorText);
                case Locator.Id:
                    return By.Id(LocatorText);
                case Locator.PartialId:
                    return By.XPath("//*[contains(@id, '" + LocatorText + "')]");
                case Locator.Name:
                    return By.Name(LocatorText);
                case Locator.PartialName:
                    return By.XPath("//*[contains(@name, '" + LocatorText + "')]");
                case Locator.ClassName:
                    return By.ClassName(LocatorText);
                case Locator.PartialClass:
                    return By.XPath("//*[contains(@class, '" + LocatorText + "')]");
                case Locator.LinkText:
                    return By.LinkText(LocatorText);
                case Locator.PartialLinkText:
                    return By.PartialLinkText(LocatorText);
                case Locator.TagName:
                    return By.TagName(LocatorText);
                case Locator.CssSelector:
                    return By.CssSelector(LocatorText);
                default:
                    throw new Exception("No Locator Found");

            }

        }

       
        public static Boolean ObjectButton(string element, Locator Locator, string FieldName, string Action)
        {
            IWebElement elementObj = AndriodGeneric.GetWebElement(element, Locator);

            //Perform the action
            switch (Action)
            {
                case "Click":
                    try
                    {
                        elementObj.Click();
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                case "ObjectExist":
                    if (elementObj.Displayed)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "ObjectEnabled":
                    if (elementObj.Enabled)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }

        public static Boolean ObjectEdit(string element, Locator Locator, string FieldName, string Action, string ActionData)
        {

            IWebElement elementObj = AndriodGeneric.GetWebElement(element, Locator);

            //Perform the action
            switch (Action)
            {
                case "EnterValue":
                    try
                    {
                        elementObj.Clear();
                        elementObj.SendKeys(ActionData);
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                case "ClearValue":
                    try
                    {
                        elementObj.Clear();
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                case "ReadValue":
                    try
                    {
                        gbReturnValue = elementObj.GetAttribute("value");
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                case "VerifyValue":
                    gbReturnValue = elementObj.GetAttribute("value");
                    if (gbReturnValue.Equals(ActionData))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "ObjectExist":
                    if (elementObj.Displayed)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "ObjectEnabled":
                    if (elementObj.Enabled)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }

        public static Boolean ObjectChkbox(string element, Locator Locator, string FieldName, string Action, string CheckValue)
        {
            IWebElement elementObj = AndriodGeneric.GetWebElement(element, Locator);

            //Perform the action
            switch (Action)
            {
                case "SelectValue":
                    try
                    {
                        elementObj.Click();
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                case "ReadValue":
                    try
                    {
                        flag = elementObj.Selected;
                        return flag;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                case "VerifyValue":
                    if (elementObj.Selected.Equals(CheckValue))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "ObjectExist":
                    if (elementObj.Displayed)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "ObjectEnabled":
                    if (elementObj.Enabled)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }

        public static Boolean ObjectImage(string element, Locator Locator, string FieldName, string Action)
        {
            IWebElement elementObj = AndriodGeneric.GetWebElement(element, Locator);

            //Perform the action
            switch (Action)
            {
                case "Click":
                    try
                    {
                        elementObj.Click();
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                case "ObjectExist":
                    if (elementObj.Displayed)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "ObjectEnabled":
                    if (elementObj.Enabled)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }

        public static Boolean ObjectRadio(string element, Locator Locator, string FieldName, string Action, string CheckValue)
        {
            IWebElement elementObj = AndriodGeneric.GetWebElement(element, Locator);

            //Perform the action
            switch (Action)
            {
                case "SelectValue":
                    try
                    {
                        elementObj.Click();
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                case "ReadValue":
                    try
                    {
                        flag = elementObj.Selected;
                        return flag;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                case "VerifyValue":
                    if (elementObj.Selected.Equals(CheckValue))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "ObjectExist":
                    if (elementObj.Displayed)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "ObjectEnabled":
                    if (elementObj.Enabled)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }

        public static Boolean ObjectDropdown(string element, Locator Locator, string FieldName, string Action, string ActionData)
        {
            IWebElement elementObj = AndriodGeneric.GetWebElement(element, Locator);

            if (ActionData.Contains("/"))
            {
                ActionData = ActionData.Replace('/', ',');
            }
            var selectElement = new SelectElement(elementObj);
            switch (Action)
            {
                case "SelectValue":
                    try
                    {
                        selectElement.SelectByText(ActionData);
                        //selectElement.SelectedOption.Click();
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                case "ReadValue":
                    try
                    {
                        gbReturnValue = selectElement.SelectedOption.Text;
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                case "VerifyValue":
                    gbReturnValue = selectElement.SelectedOption.Text;
                    if (gbReturnValue.Equals(ActionData))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "ObjectExist":
                    if (elementObj.Displayed)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;


            }
        }

        public static Boolean ObjectLink(string element, Locator Locator, string FieldName, string Action, string ActionData)
        {
            IWebElement elementObj = AndriodGeneric.GetWebElement(element, Locator);
            switch (Action)
            {
                case "Click":
                    try
                    {
                        elementObj.Click();
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                case "ObjectExist":
                    if (elementObj.Displayed)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "ReadValue":
                    try
                    {
                        gbReturnValue = elementObj.GetAttribute("value");
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }

                default:
                    return false;


            }
        }

        public static Boolean ObjectText(string element, Locator Locator, string FieldName, string Action,string ActionData)
        {
            IWebElement elementObj = AndriodGeneric.GetWebElement(element, Locator);

            //Perform the action
            switch (Action)
            {
                case "ReadValue":
                    try
                    {
                        gbReturnValue = elementObj.Text.Trim();
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }

                case "ObjectExist":
                    if (elementObj.Displayed)
                    {
                        return true;
                    }
                    else
                    {
                       return false;
                    }
                case "ObjectEnabled":
                    if (elementObj.Enabled)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "VerifyValue":
                    try
                    {
                        if (elementObj.Text.Trim().Equals(ActionData)) { }
                            return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }

        public static bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public static Image DownloadImageFromUrl(string imageUrl)
        {
            Image image = null;

            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
                HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(imageUrl);
                webRequest.AllowWriteStreamBuffering = true;
                webRequest.Timeout = 30000;

                WebResponse webResponse = webRequest.GetResponse();

                Stream stream = webResponse.GetResponseStream();

                image = Image.FromStream(stream);

                webResponse.Close();
            }
            catch (Exception ex)
            {
                return null;
            }

            return image;
        }

        public static bool CompareImages(string Image1Location, string Image2Location)
        {
            bool flag = true;
            int count1Pix = 0;
            int count2Pix = 0;
            //First Image files
            //string Image1Location = "Test.jpg";
            //Second Image File
            //string Image2Location = @"TSSAutomation\" + WindowsPlatform + ".jpg";
            try
            {

                string imageRef1, imageRef2;
                //First Image BitMap
                Bitmap img1Bitmap = new Bitmap(Image1Location);
                //Second Image BitMap
                Bitmap img2Bitmap = new Bitmap(Image2Location);


                //Compare by Width
                if (img1Bitmap.Width == img2Bitmap.Width)
                {
                    //Compare by Height
                    if (img1Bitmap.Height == img2Bitmap.Height)
                    {
                        for (int i = 0; i < img1Bitmap.Width; i++)
                        {
                            for (int j = 0; j < img1Bitmap.Height; j++)
                            {
                                imageRef1 = img1Bitmap.GetPixel(i, j).ToString();
                                imageRef2 = img2Bitmap.GetPixel(i, j).ToString();
                                if (imageRef1 != imageRef2)
                                {

                                    count2Pix++;
                                    if (count2Pix > 50)
                                    {
                                        flag = false;
                                    }
                                    break;

                                }
                                count1Pix++;
                            }
                        ;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Images are of Different Height");
                        flag = false;
                    }
                }
                else
                {
                    Console.WriteLine("Images are of Different Width");
                    flag = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            if (flag == false)
            {
                return flag;
            }
            else
            {
               
            }
            return flag;
        }

    }
}