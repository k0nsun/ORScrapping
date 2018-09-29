using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyOR.DTO;
using EasyOR.Parser.View;

namespace EasyOR.Parser.Controleur
{
    public class Spy
    {
        private const string urlSpySonde = "http://universphoenix.origins-return.fr/flotte_espionner.php";

        private Navigation nav = new Navigation();
        public bool State = false;
        public List<Planet> ListPlanete = new List<Planet>();
        private Planet lastPlanete;
        public Spy(Planet planete)
        {
            ListPlanete.Add(planete);
        }

        public Spy(List<Planet> listPlanete)
        {
            ListPlanete = listPlanete;
        }

        public void SpyPlayer(Main mainForm)
        {
            if (nav.navigationPage(mainForm.webBrowserMain, urlSpySonde))
            {
                if (mainForm.action == string.Empty)
                    mainForm.action = "spy";

                HtmlDocument myDoc = (HtmlDocument)mainForm.webBrowserMain.Document;

                if (!myDoc.Body.InnerText.Contains("Rapport Intermédiaire"))
                {
                    if (lastPlanete != ListPlanete[0])
                    {
                        myDoc.InvokeScript("Raccourci", new object[] { ListPlanete[0].Galaxy, ListPlanete[0].System, ListPlanete[0].Position });
                        lastPlanete = ListPlanete[0];
                    }
                    if (myDoc.Body.InnerText.Contains("Vous avez") || myDoc.Body.InnerText.Contains("vacance") || myDoc.Body.InnerText.Contains("Vous n'avez pas"))
                        spyComplete();
                }
                else
                {
                    spyComplete();
                }
                //State = true;
                foreach (HtmlElement element in myDoc.All)
                {
                    if (element.Children.Count == 0)
                    {
                        if (element.OuterHtml.Contains("espionner1.gif"))
                        {
                            if (nav.navigationPage(mainForm.webBrowserMain, urlSpySonde))
                                element.InvokeMember("click");
                        }
                    }
                }
            }
        }

        private void spyComplete()
        {
            ListPlanete.Remove(lastPlanete);
            if (ListPlanete.Count == 0)
                State = true;
        }
    }
}
