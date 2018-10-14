using EasyOR.DTO;
using EasyOR.Parser.View;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EasyOR.Parser.Controleur
{
    public class Spy
    {
        private const string urlSpySonde = "http://universphoenix.origins-return.fr/flotte_espionner.php";

        private Navigation nav = new Navigation();
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
            if (ListPlanete.Count == 0)
            {
                // Wait 5 minutes
                System.Threading.Thread.Sleep(5 * 60 * 1000);
                mainForm.action = Action.updatePlayerNameStep3;
                return;
            }

            if (mainForm.action == Action.UNKNOW)
                mainForm.action = Action.Spy;


            if (nav.NavigationPage(mainForm.webBrowserMain, urlSpySonde))
            {
                HtmlDocument myDoc = mainForm.webBrowserMain.Document;
                if (!myDoc.Body.InnerText.Contains("Rapport Intermédiaire"))
                {
                    if (lastPlanete != ListPlanete[0])
                    {
                        new Navigation().InvokeForm(urlSpySonde, mainForm.webBrowserMain, "Raccourci", new object[] { ListPlanete[0].Galaxy, ListPlanete[0].System, ListPlanete[0].Position });
                        lastPlanete = ListPlanete[0];
                    }

                    if (myDoc.Body.InnerText.Contains("Vous avez") || myDoc.Body.InnerText.Contains("vacance") || myDoc.Body.InnerText.Contains("Vous n'avez pas") || myDoc.Body.InnerText.Contains("aucune"))
                        ListPlanete.RemoveAt(0);
                }
                else
                {
                    ListPlanete.RemoveAt(0);
                }

                foreach (HtmlElement element in myDoc.All)
                {
                    if (element.Children.Count == 0)
                    {
                        if (element.OuterHtml.Contains("espionner1.gif"))
                        {
                            if (nav.NavigationPage(mainForm.webBrowserMain, urlSpySonde))
                                element.InvokeMember("click");
                        }
                    }
                }
            }
        }
    }
}
