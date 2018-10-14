using EasyOR.DataAccess.SqlServer;
using EasyOR.DTO;
using EasyOR.Parser.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EasyOR.Parser.Controleur
{
    public class GetPlayerName
    {
        private const string urlImpulseur = "http://universphoenix.origins-return.fr/impulseur.php";
        private const string urlMessageSpy = "http://universphoenix.origins-return.fr/messagerie.php?cat=Espio";
        private const string urlBase = "http://universphoenix.origins-return.fr/";
        private Navigation nav = new Navigation();
        public List<Player> ListPlayer = new List<Player>();

        //Impulseur
        private PlayerAction playerMapp = new PlayerAction();
        private PlanetAction planeteMapp = new PlanetAction();
        private Player lastPlayer;

        //Spy
        private bool InitSpy = false;
        private Spy spy;

        //Message
        private bool InitMessage = false;
        private bool messageRead = false;


        public GetPlayerName()
        {
            // on recupere tout les joueurs n'ayant pas de nom dans la base de données
            GetAllPlayerWithoutName();
        }

        private void GetAllPlayerWithoutName()
        {
           var ListPlayerDb = new PlayerAction().GetPlayerWithoutName();
            ListPlayer = ListPlayerDb.Where(x => x.IsVacation == null && x.IsQuestPlayer == null).ToList();
        }

        public void GetName(Main mainForm)
        {
            switch (mainForm.action)
            {
                case Action.updatePlayerNameStep1:
                    GetNameByImpulseur(mainForm);
                    break;
                case Action.updatePlayerNameStep2:
                    GetNameBySpyinit();
                    GetNameBySpy(mainForm);
                    break;
                case Action.updatePlayerNameStep3:                    
                    GetNameByMessageInit();
                    GetNameByMessage(mainForm);
                    break;
                default:
                    break;
            }
        }

        private void GetNameByImpulseur(Main mainForm)
        {
            // on essaye une simulation de lancement d'attaque
            if (nav.NavigationPage(mainForm.webBrowserMain, urlImpulseur))
            {
                HtmlDocument myDoc = mainForm.webBrowserMain.Document;
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
                                    if (nav.NavigationPage(mainForm.webBrowserMain, urlImpulseur))
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
                                GetNameByImpulseurComplete(mainForm);
                                nav.ReloadPage(mainForm.webBrowserMain);
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
                                GetNameByImpulseurComplete(mainForm);
                                nav.ReloadPage(mainForm.webBrowserMain);
                            }
                            else
                            {
                                foreach (HtmlElement element in myDoc.All)
                                {
                                    if (element.Children.Count == 0)
                                    {
                                        if (element.OuterHtml.Contains("attaquer1.gif"))
                                        {
                                            if (nav.NavigationPage(mainForm.webBrowserMain, urlImpulseur))
                                                element.InvokeMember("click");
                                        }
                                    }
                                }
                            }
                        }
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

                                    GetNameByImpulseurComplete(mainForm);
                                    nav.ReloadPage(mainForm.webBrowserMain);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void GetNameByImpulseurComplete(Main mainForm)
        {
            ListPlayer.RemoveAt(0);
            if (ListPlayer.Count == 0)
            {
                mainForm.action = Action.updatePlayerNameStep2;
                GetAllPlayerWithoutName();
            }
        }

        private void GetNameBySpyinit()
        {
            if (!InitSpy)
            {
                // Clear memomry
                playerMapp = null;
                planeteMapp = null;
                lastPlayer = null;

                // Get planet wihtout name and quest
                List<Planet> listPlanete = new List<Planet>();
                IEnumerable<Player> listPlayerQuest = new PlayerAction().GetPlayerQuestWithoutName();
                foreach (Player player in listPlayerQuest)
                {
                    if (player.Planets.Count != 0)
                    {
                        listPlanete.Add(player.Planets[0]);
                    }
                }
                spy = new Spy(listPlanete);
                InitSpy = true;
            }
        }

        private void GetNameBySpy(Main mainForm)
        {
            // on essaye d'espionner les gens qui n'on pas de nom
            spy.SpyPlayer(mainForm);
        }

        public void GetNameByMessageInit()
        {
            if (!InitMessage)
            {               
                spy = null;
                InitSpy = false;
            }
        }
        public void GetNameByMessage(Main mainForm)
        {
            if (nav.NavigationPage(mainForm.webBrowserMain, urlMessageSpy))
            {
                if (!messageRead)
                {
                    HtmlDocument htmlDoc = mainForm.webBrowserMain.Document;
                    HtmlElementCollection listTable = htmlDoc.GetElementsByTagName("table");

                    // drop start and end
                    int nbMessage = listTable[2].Children[0].Children.Count - 2;

                    Dictionary<string, object> messageIds = new Dictionary<string, object>();
                    //Start at 1 (0 is header)
                    if (nbMessage == 1)
                    {
                        mainForm.action = Action.UNKNOW;
                        nav.NavigationPage(mainForm.webBrowserMain, urlMessageSpy);
                        return;
                    }

                    for (int i = 1; i <= nbMessage; i++)
                    {

                        listTable[2].Children[0].Children[i].Children[0].Children[0].SetAttribute("checked", "checked");

                        // Get player by planet
                        Planet planet = null;


                        var text = listTable[2].Children[0].Children[i].InnerText;
                        var namePlayer = Regex.Match(text, @"\(([^)]*)\)").Groups[1].Value;
                        var positionPlayer = Regex.Match(text, @"\[([^)]*)\]").Groups[1].Value;
                        var positionSeparateString = positionPlayer.Split(':');
                        int[] positionSeparate = Array.ConvertAll(positionSeparateString, int.Parse);
                        
                        //update player                        
                        var messageId = listTable[2].Children[0].Children[i].Children[0].Children[0].GetAttribute("name");
                        messageIds.Add(messageId, "on");
                        planet = new PlanetAction().GetPlanetByPosition(positionSeparate[0], positionSeparate[1], positionSeparate[2]);
                        PlayerAction playerAction = new PlayerAction();
                        var player = playerAction.GetPlayerById(planet.PlayerId);
                        player.Name = namePlayer;
                        playerAction.UpdatePlayer(player);
                    }

                    // Click "Voir les messages
                    var listElement = htmlDoc.GetElementsByTagName("input");
                    foreach (HtmlElement item in listElement)
                    {
                        bool boutonReadFound = item.OuterHtml.Contains("value=Lire");
                        if (boutonReadFound)
                        {
                            nav.InvokeMember(item, "click");
                            messageRead = true;
                        }
                    }
                }
                else
                {
                    messageRead = false;
                    nav.NavigationPage(mainForm.webBrowserMain, urlBase + "messagerie_delester.php?cat=Espio");
                }
            }
        }
    }
}
