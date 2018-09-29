using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyOR.DataAccess.SqlServer;
using EasyOR.Parser.Controleur.ObjectData;
using EasyOR.Parser.View;

namespace EasyOR.Parser.Controleur
{
    public class SearchPlayerCapteur : Navigation
    {
        #region membres
        public int GalaxyStart;
        public int SystemStart;


        public int GalaxyMax;
        public List<Player> playerFind;

        public int m_galaxyNavigation;
        private int GalaxyNavigation
        {
            get
            {
                return m_galaxyNavigation;
            }
        }
        private int m_systemNavigation;
        public int SystemNavigation
        {
            get
            {
                return m_systemNavigation;
            }
        }
        private int SystemNavigationADD
        {
            get
            {
                return m_systemNavigation;
            }
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
        }

        public bool State;
        #endregion


        public SearchPlayerCapteur(int galaxyStart, int systemStart, int galaxyMax)
        {
            this.GalaxyStart = galaxyStart;
            this.SystemStart = systemStart;
            this.m_galaxyNavigation = galaxyStart;
            this.SystemNavigationADD = systemStart;
            this.GalaxyMax = galaxyMax;
            playerFind = new List<Player>();
        }

        private bool init = false;

        public void search(WebBrowser webbrowser, Main mainForm)
        {
            // on regarde initialise la page au debut
            if (!init)
            {
                if (base.changePosition(webbrowser, this.GalaxyNavigation, this.SystemNavigation))
                    init = true;
                return;
            }

            HtmlDocument myDoc = (HtmlDocument)webbrowser.Document;
            if (!charge)
            {
                charge = true;
                return;
            }
            else
            {
                charge = false;
            }

            if (this.GalaxyNavigation <= this.GalaxyMax)
            {
                this.State = false;
                // on est actuellement au coordonnées suivantes
                string galaxieActual = myDoc.GetElementById("galaxi2").GetAttribute("value");
                string systemActual = myDoc.GetElementById("system2").GetAttribute("value");

                // on recupere l'ID parent le plus proche des planetes
                HtmlElement pageSystemSolaire = myDoc.GetElementById("galaxiform");
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
                                            planete.Galaxy = this.GalaxyNavigation;
                                            planete.System = this.SystemNavigation;
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

                                                List<long> allInt = generalFunction.getNumberFromText(elementClick);

                                                int UniquePUniqueNumberContact = Convert.ToInt32(allInt[0]);
                                                player = new Player();
                                                player.UniqueNumberContact = UniquePUniqueNumberContact;


                                                EasyOR.DataAccess.SqlServer.PlayerAction playerDB = new DataAccess.SqlServer.PlayerAction();
                                                var playerFound = playerDB.GetUserByInternalIdOR(player.UniqueNumberContact);
                                                if (playerFound == null)
                                                {
                                                    // Add player
                                                    playerDB.AddPlayer(new EasyOR.DTO.Player() { Name = string.Empty, IsAFK = false, IsVacation = false, InternalIdOR = player.UniqueNumberContact });

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
                SystemNavigationADD++;
                base.changePosition(webbrowser, this.GalaxyNavigation, this.SystemNavigation);
            }
            else
            {
                this.State = true;
            }
        }


    }
}
