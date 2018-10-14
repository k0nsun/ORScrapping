using EasyOR.DataAccess.SqlServer;
using EasyOR.Parser.Controleur.ObjectData;
using EasyOR.Parser.View;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EasyOR.Parser.Controleur
{
    public class SearchPlayerCapteur : Navigation
    {
        #region membres
        private int GalaxyMax;
        private int m_galaxyNavigation;
        private int GalaxyNavigation
        {
            set
            {
                m_galaxyNavigation = value;
            }
            get
            {
                return m_galaxyNavigation;
            }
        }
        private int m_systemNavigation;
        private int SystemNavigation
        {
            set
            {
                if (value > 100)
                {
                    m_galaxyNavigation++;
                    m_systemNavigation = 1;
                }
                else
                {
                    m_systemNavigation = value;
                }
            }
            get
            {
                return m_systemNavigation;
            }
        }
        private bool ProcessInit = false;
        private const string capteurInterstellaireURL = @"http://universphoenix.origins-return.fr/galaxie.php";
        #endregion

        public SearchPlayerCapteur(int galaxyStart, int systemStart, int galaxyMax)
        {
            SystemNavigation = systemStart;
            GalaxyNavigation = galaxyStart;
            GalaxyMax = galaxyMax;
            ProcessInit = false;
        }  

        public bool Initialisation(Main mainForm)
        {
            if (new Navigation().InvokeForm(capteurInterstellaireURL, mainForm.webBrowserMain, "galaxi_envoi",new object[] { GalaxyNavigation, SystemNavigation } ))
            {
                ProcessInit = true;
            }
            return false;
        }

        public void Search(Main mainForm)
        {
            // on regarde initialise la page au debut
            if (!ProcessInit)
            {
                mainForm.action = Action.updatePlayerIdUnique;
                Initialisation(mainForm);
                return;
            }
            if (GalaxyNavigation <= GalaxyMax)
            {
                // on est actuellement au coordonnées suivantes
                string galaxieActual = mainForm.webBrowserMain.Document.GetElementById("galaxi").GetAttribute("value");
                string systemActual = mainForm.webBrowserMain.Document.GetElementById("system").GetAttribute("value");

                // on recupere l'ID parent le plus proche des planetes
                HtmlElement pageSystemSolaire = mainForm.webBrowserMain.Document.GetElementById("galaxiform");
                foreach (HtmlElement elementTemp in pageSystemSolaire.Children)
                {
                    // on recupere le conteneur de l'ensemble des planetes
                    if (elementTemp.GetAttribute("className").Contains("orcp2m"))
                    {
                        // mainForm.searchCDRUserView.progressChanged(this.GalaxyStart, this.SystemStart, this.GalaxyMax, 100, this.GalaxyNavigation, this.SystemNavigation, base.lastLoadTime);
                        int position = 0;
                        foreach (HtmlElement elementTemp2 in elementTemp.Children)
                        {
                            // Parcours de chaques lignes de planetes (pas de différence en les zones sombres et claires)
                            if (elementTemp2.GetAttribute("className").Contains("orcp2mlg"))
                            {
                                position++;
                                Player player;
                                Planete planete = null;
                                foreach (HtmlElement elementTemp3 in elementTemp2.Children)
                                {

                                    // on recupère les informations de la planète
                                    if (elementTemp3.GetAttribute("className").Contains("orcp2mlgc"))
                                    {
                                        string planeteName = elementTemp3.InnerText;
                                        if (!string.IsNullOrEmpty(planeteName))
                                        {
                                            planete = new Planete();
                                            planete.Galaxy = GalaxyNavigation;
                                            planete.System = SystemNavigation;
                                            planete.Position = position;
                                            planete.Name = planeteName;
                                        }
                                    }

                                    if (elementTemp3.GetAttribute("className").Contains("orcp2mlgf"))
                                    {
                                        if ((elementTemp3.FirstChild != null) && (planete != null))
                                        {
                                            string elementClick = elementTemp3.FirstChild.OuterHtml;
                                            if (elementClick.Contains("messagerie_ecrire"))
                                            {

                                                List<long> allInt = GeneralFunction.getNumberFromText(elementClick);

                                                int UniquePUniqueNumberContact = Convert.ToInt32(allInt[0]);
                                                player = new Player();
                                                player.UniqueNumberContact = UniquePUniqueNumberContact;


                                                EasyOR.DataAccess.SqlServer.PlayerAction playerDB = new DataAccess.SqlServer.PlayerAction();
                                                var playerFound = playerDB.GetUserByInternalIdOR(player.UniqueNumberContact);
                                                if (playerFound == null)
                                                {
                                                    // Add player
                                                    playerDB.AddPlayer(new EasyOR.DTO.Player() { Name = null, IsAFK = null, IsVacation = null, InternalIdOR = player.UniqueNumberContact });

                                                    playerFound = playerDB.GetUserByInternalIdOR(player.UniqueNumberContact);
                                                    // add planet
                                                    new PlanetAction().AddPlanet(new DTO.Planet() { Galaxy = (Int16)planete.Galaxy, System = (Int16)planete.System, Position = (Int16)planete.Position, PlayerId = playerFound.PlayerId, Name = planete.Name });
                                                }
                                                else
                                                {
                                                    var planet = new PlanetAction().GetByCoordinatesAndUserInternalID((Int16)planete.Galaxy, (Int16)planete.System, (Int16)planete.Position, player.UniqueNumberContact);
                                                    if (planet == null)
                                                    {
                                                        new PlanetAction().AddPlanet(new DTO.Planet() { Galaxy = (Int16)planete.Galaxy, System = (Int16)planete.System, Position = (Int16)planete.Position, PlayerId = playerFound.PlayerId, Name = planete.Name });
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                SystemNavigation++;
                new Navigation().InvokeForm(capteurInterstellaireURL, mainForm.webBrowserMain, "galaxi_envoi", new object[] { GalaxyNavigation, SystemNavigation });
            }
            else
            {
                mainForm.action = Action.updatePlayerNameStep1;
                mainForm.getPlayerName = new GetPlayerName();
                mainForm.getPlayerName.GetName(mainForm);
                mainForm.searchPlayer = null;
            }
        }
    }
}
