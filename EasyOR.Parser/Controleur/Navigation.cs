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
        public DateTime lastLoadTime;
        public bool charge = false;
        private const int limitTimeCharge = 300;

        public const string capteurInterstellaire = @"http://universphoenix.origins-return.fr/galaxie.php";

        public bool changePosition(WebBrowser webbrowser, int galaxy, int system)
        {
            if (Init(webbrowser, capteurInterstellaire))
            {
                limitnavigateTime();
                HtmlDocument myDoc = (HtmlDocument)webbrowser.Document;
                myDoc.InvokeScript("galaxi_envoi", new object[] { galaxy, system });
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


        public bool navigationPage(WebBrowser webbrowser, string url)
        {
            limitnavigateTime();
            if (Init(webbrowser, url))
            {
                return true;
            }
            return false;
        }

        public bool reloadPage(WebBrowser webbrowser)
        {
            limitnavigateTime();
            webbrowser.Navigate(webbrowser.Url.OriginalString);
            return true;
        }

        private void limitnavigateTime()
        {
            if ((DateTime.Now - lastLoadTime).TotalMilliseconds < limitTimeCharge)
            {
                int timeNeed = Convert.ToInt32(limitTimeCharge - ((DateTime.Now - lastLoadTime).TotalMilliseconds));
                Thread.Sleep(timeNeed);
            }
            lastLoadTime = DateTime.Now;
        }


    }
}
