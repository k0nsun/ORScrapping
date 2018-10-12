using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace EasyOR.Parser.Controleur
{
    public class Navigation
    {
        public static DateTime lastLoadTime;
        public bool charge = false;
        private const int limitTimeCharge = 500;


        public bool InvokeForm(string url, WebBrowser webbrowser, string nameMethod, object[] paramMethod)
        {
            if (NavigationPage(webbrowser, url))
            {
                limitnavigateTime();
                HtmlDocument element = (HtmlDocument)webbrowser.Document;
                element.InvokeScript(nameMethod, paramMethod);
                 return true;
            }
            return false;
        }

        private bool Init(WebBrowser webbrowser, string url)
        {
            if (webbrowser.Url.OriginalString != url)
            {
                webbrowser.Navigate(url);
                return false;
            }
            return true;
        }


        public bool NavigationPage(WebBrowser webbrowser, string url)
        {
            limitnavigateTime();
            return Init(webbrowser, url);
        }

        public bool ReloadPage(WebBrowser webbrowser)
        {
            limitnavigateTime();
            webbrowser.Navigate(webbrowser.Url.OriginalString);
            return true;
        }

        private void limitnavigateTime()
        {
            if ((DateTime.Now - Navigation.lastLoadTime).TotalMilliseconds < limitTimeCharge)
            {
                int timeNeed = Convert.ToInt32(limitTimeCharge - ((DateTime.Now - Navigation.lastLoadTime).TotalMilliseconds));
                Thread.Sleep(timeNeed);
            }
            Navigation.lastLoadTime = DateTime.Now;
        }


    }
}
