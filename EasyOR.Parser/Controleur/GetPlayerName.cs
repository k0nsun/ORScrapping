using EasyOR.DataAccess.SqlServer;
using EasyOR.DTO;
using EasyOR.Parser.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyOR.Parser.Controleur
{
    public class GetPlayerName
    {
        private const string urlImpulseur = "http://universphoenix.origins-return.fr/impulseur.php";


        private Navigation nav = new Navigation();
        public List<Player> ListPlayer = new List<Player>();
        private PlayerAction playerMapp = new PlayerAction();
        private PlanetAction planeteMapp = new PlanetAction();
        private bool StateImpulseur = false;
        private bool StateSpy = false;
        private bool initSpy = false;
        private Player lastPlayer;
        private Spy spy;
        public GetPlayerName()
        {
            // on recupere tout les joueurs n'ayant pas de nom dans la base de données
            GetAllPlayerWithoutName();
        }

        private void GetAllPlayerWithoutName()
        {
            ListPlayer = new PlayerAction().GetPlayerWithoutName().ToList();
        }

        private Main MainForm;
        public void GetName(Main mainForm)
        {
            
            MainForm = mainForm;
            MainForm.action = "getNamePlayer";
            StateImpulseur = false;
            if (!StateImpulseur)
            {
                GetNameByImpulseur();
            }
            else
            {
                getNameBySpyinit();
                getNameBySpy();
            }
        }

        private void GetNameByImpulseur()
        {
            // on essaye une simulation de lancement d'attaque
            if (nav.navigationPage(MainForm.webBrowserMain, urlImpulseur))
            {
                HtmlDocument myDoc = (HtmlDocument)MainForm.webBrowserMain.Document;
                if (!myDoc.Body.InnerText.Contains("Rapport Intermédiaire"))
                {
                    if (myDoc.GetElementById("galaxi").GetAttribute("value") == "")
                    {
                        lastPlayer = ListPlayer[0];
                        myDoc.GetElementById("galaxi").SetAttribute("value", ListPlayer[0].Planets[0].Galaxy.ToString());
                        myDoc.GetElementById("system").SetAttribute("value", ListPlayer[0].Planets[0].System.ToString());
                        myDoc.GetElementById("position").SetAttribute("value", ListPlayer[0].Planets[0].Position.ToString());

                        foreach (HtmlElement element in myDoc.All)
                        {
                            if (element.Children.Count == 0)
                            {
                                if (element.OuterHtml.Contains("attaquer1.gif"))
                                {
                                    if (nav.navigationPage(MainForm.webBrowserMain, urlImpulseur))
                                        element.InvokeMember("click");

                                }
                            }
                        }
                    }
                    else
                    {
                        if (lastPlayer == ListPlayer[0])
                        {
                            //erreur
                            // s'il contient des erreurs alors
                            if (myDoc.Body.InnerText.Contains("quête"))
                            {
                                // on ajoute le joueur à la BDD
                                Player player = ListPlayer[0];
                                player.IsQuestPlayer = true;
                                playerMapp.UpdatePlayer(player);
                                getNameComplete();
                                nav.reloadPage(MainForm.webBrowserMain);
                            }
                            else if (myDoc.Body.InnerText.Contains("Vous avez") || myDoc.Body.InnerText.Contains("vacances") || myDoc.Body.InnerText.Contains("pacte") || myDoc.Body.InnerText.Contains("faible") || myDoc.Body.InnerText.Contains("bloqué") || myDoc.Body.InnerText.Contains("de votre alliance.") || myDoc.Body.InnerText.Contains("Vous ne pouvez pas attaquer une planète de votre empire."))
                            {
                                if (myDoc.Body.InnerText.Contains("vacances"))
                                {
                                    Player player = ListPlayer[0];
                                    player.IsVacation = true;
                                    player.IsAFK = false;
                                    playerMapp.UpdatePlayer(player);
                                }
                                getNameComplete();
                                nav.reloadPage(MainForm.webBrowserMain);
                            }
                            else
                            {
                                foreach (HtmlElement element in myDoc.All)
                                {
                                    if (element.Children.Count == 0)
                                    {
                                        if (element.OuterHtml.Contains("attaquer1.gif"))
                                        {
                                            if (nav.navigationPage(MainForm.webBrowserMain, urlImpulseur))
                                                element.InvokeMember("click");
                                            // MainForm.action = "";
                                        }
                                    }
                                }
                            }
                        }

                        // si ça ne passe pas on récupère les informations suivantes: joueur quetes

                    }
                }
                else
                {
                    if (lastPlayer == ListPlayer[0])
                    {
                        foreach (HtmlElement element in myDoc.All)
                        {
                            if (element.Children.Count == 0)
                            {
                                if (element.OuterHtml.Contains("Joueur concerné :"))
                                {
                                    string result = element.NextSibling.InnerText;
                                    if (result.Contains("["))
                                        result = result.Remove(result.IndexOf('['));
                                    result = result.TrimStart();
                                    result = result.TrimEnd();
                                    // on ajoute le joueur à la BDD
                                    Player player = ListPlayer[0];
                                    player.Name = result;
                                    playerMapp.UpdatePlayer(player);
                                    
                                    getNameComplete();
                                    nav.reloadPage(MainForm.webBrowserMain);
                                }
                            }
                        }
                    }
                }
            }

            // si ça passe on récupère le nom du joueur et on le supprime de la liste

        }

        private void getNameComplete()
        {
            ListPlayer.RemoveAt(0);

            if (ListPlayer.Count == 0)
            {
                StateImpulseur = true;
                GetAllPlayerWithoutName();
            }
        }


        private void getNameBySpyinit()
        {
            if (!StateSpy)
            {
                List<Planet> listPlanete = new List<Planet>();

                foreach (Player player in ListPlayer)
                {
                    if (player.Planets.Count != 0)
                    {
                        listPlanete.Add(player.Planets[0]);
                    }
                }

                spy = new Spy(listPlanete);
                StateSpy = true;
            }
        }
        private void getNameBySpy()
        {
            // on essaye d'espionner les gens qui n'on pas de nom

            spy.SpyPlayer(MainForm);
        }
    }
}
