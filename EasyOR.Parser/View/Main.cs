using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyOR.Parser.Controleur;
using EasyOR.Parser.Controleur.ObjectData;

namespace EasyOR.Parser.View
{
    public partial class Main : Form
    {
        public string action = string.Empty;
        HtmlDocument myDoc2;

       // public SearchCDRCapteur searchCDR;
        public SearchPlayerCapteur searchPlayer;
        //public UserControlSearchCDR searchCDRUserView;
        //public Spy spyPlayer;
        public const string deconnexion = "http://universphoenix.origins-return.fr/serveur.php";
        public const string apercu = "http://universphoenix.origins-return.fr/apercu.php";

        //private SearchPlayer form;
        public Main()
        {
            InitializeComponent();
            this.webBrowserMain.ScriptErrorsSuppressed = true;

#if !_DEV
            button1.Visible = false;
            administrateurToolStripMenuItem.Visible = false;
#endif
            // on lance la navigation en version mobile (moi de données à charger)
            webBrowserMain.Navigate(@"http://www.origins-return.fr/", null, null, "User-Agent:Mozilla/5.0 (iPhone; U; CPU iPhone OS 4_1 like Mac OS X; en-us) AppleWebKit/532.9 (KHTML, like Gecko) Version/4.0.5 Mobile/8B117 Safari/6531.22.7");
        }

        public void webBrowserMain_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if ((deconnexion == webBrowserMain.Url.OriginalString) || (webBrowserMain.IsOffline))
            {
                this.action = "";
            }

            if (apercu == webBrowserMain.Url.OriginalString)
            {
                // verification du login du joueur
                // absorbtion des données du joueurs
            }
            HtmlDocument myDoc = (HtmlDocument)webBrowserMain.Document;
            myDoc2 = myDoc;

            switch (action)
            {
                case "searchCDR":
                    //if (searchCDR != null)
                    //    searchCDR.search(this);
                    //if (searchCDR.State)
                    //{
                    //    action = string.Empty;
                    //    MessageBox.Show("Recherche de CDR terminée");
                    //    panelWebBrowser.Visible = false;
                    //    panelWebBrowser.Controls.Clear();
                    //    searchCDRUserView.Dispose();
                    //}
                    //break;

                case "Findjoueur":
                    if (searchPlayer != null)
                        searchPlayer.search(this.webBrowserMain, this);
                    break;
                case "spy":
                    //if (spyPlayer != null)
                    //    spyPlayer.SpyPlayer(this);
                    //if (spyPlayer.State)
                    //{
                    //    action = string.Empty;
                    //}
                    //break;
                case "getNamePlayer":
                    if (getPlayerName != null)
                        getPlayerName.GetName(this);
                    break;
                default:
                    break;
            }
        }
        GetPlayerName getPlayerName;
        private void button1_Click(object sender, EventArgs e)
        {    
            getPlayerName = new GetPlayerName();
            if (getPlayerName.ListPlayer.Count == 0)
            {
                getPlayerName.GetName(this);
            }          
        }

        private void rechercherLesJoueursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            action = "Findjoueur";
            searchPlayer = new SearchPlayerCapteur(1, 1, 50);
            searchPlayer.search(this.webBrowserMain, this);
        }

        private void rechercheCDRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (action != "searchCDR")
            {
                //SearchCDRInit formSearchCDRInit = new SearchCDRInit(this);
                //if (formSearchCDRInit.ShowDialog() == DialogResult.OK)
                //{

                //    searchCDRUserView = new UserControlSearchCDR(this);
                //    panelWebBrowser.Controls.Add(searchCDRUserView);
                //    panelWebBrowser.Show();
                //    cDRBindingSource.Clear();
                //    action = "searchCDR";
                //}
            }
        }

        private void chercherUnJoueurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // vérification de l'instanciation de la form ou de la disponibilité
            //if ((form == null) || form.IsDisposed)
            //    form = new SearchPlayer(this);
            //form.Show();
            //form.Focus();
        }
    }
}