
using System.Windows.Forms;
using System;

namespace EasyOR.Parser.View
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.webBrowserMain = new System.Windows.Forms.WebBrowser();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cDRBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panelWebBrowser = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.joueurToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rechercheCDRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chercherUnJoueurToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.administrateurToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rechercherLesJoueursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miseÀJourDesQuêtesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.miseÀJourDesNomsDesJoueursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cDRBindingSource)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playerBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // webBrowserMain
            // 
            this.webBrowserMain.Location = new System.Drawing.Point(12, 71);
            this.webBrowserMain.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserMain.Name = "webBrowserMain";
            this.webBrowserMain.Size = new System.Drawing.Size(1062, 779);
            this.webBrowserMain.TabIndex = 0;
            this.webBrowserMain.TabStop = false;
            this.webBrowserMain.WebBrowserShortcutsEnabled = false;
            this.webBrowserMain.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowserMain_DocumentCompleted);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.DataSource = this.cDRBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(1080, 55);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(420, 797);
            this.dataGridView1.TabIndex = 6;
            // 
            // panelWebBrowser
            // 
            this.panelWebBrowser.BackColor = System.Drawing.Color.Transparent;
            this.panelWebBrowser.Location = new System.Drawing.Point(321, 226);
            this.panelWebBrowser.Name = "panelWebBrowser";
            this.panelWebBrowser.Size = new System.Drawing.Size(448, 242);
            this.panelWebBrowser.TabIndex = 7;
            this.panelWebBrowser.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(321, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.joueurToolStripMenuItem,
            this.administrateurToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1509, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // joueurToolStripMenuItem
            // 
            this.joueurToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rechercheCDRToolStripMenuItem,
            this.chercherUnJoueurToolStripMenuItem});
            this.joueurToolStripMenuItem.Name = "joueurToolStripMenuItem";
            this.joueurToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.joueurToolStripMenuItem.Text = "Joueur";
            // 
            // rechercheCDRToolStripMenuItem
            // 
            this.rechercheCDRToolStripMenuItem.Name = "rechercheCDRToolStripMenuItem";
            this.rechercheCDRToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.rechercheCDRToolStripMenuItem.Text = "Rechercher des CDR";
            this.rechercheCDRToolStripMenuItem.Click += new System.EventHandler(this.rechercheCDRToolStripMenuItem_Click);
            // 
            // chercherUnJoueurToolStripMenuItem
            // 
            this.chercherUnJoueurToolStripMenuItem.Name = "chercherUnJoueurToolStripMenuItem";
            this.chercherUnJoueurToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.chercherUnJoueurToolStripMenuItem.Text = "Chercher un joueur";
            this.chercherUnJoueurToolStripMenuItem.Click += new System.EventHandler(this.chercherUnJoueurToolStripMenuItem_Click);
            // 
            // administrateurToolStripMenuItem
            // 
            this.administrateurToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rechercherLesJoueursToolStripMenuItem,
            this.miseÀJourDesQuêtesToolStripMenuItem,
            this.miseÀJourDesNomsDesJoueursToolStripMenuItem});
            this.administrateurToolStripMenuItem.Name = "administrateurToolStripMenuItem";
            this.administrateurToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.administrateurToolStripMenuItem.Text = "Administrateur";
            // 
            // rechercherLesJoueursToolStripMenuItem
            // 
            this.rechercherLesJoueursToolStripMenuItem.Name = "rechercherLesJoueursToolStripMenuItem";
            this.rechercherLesJoueursToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.rechercherLesJoueursToolStripMenuItem.Text = "Rechercher les joueurs";
            this.rechercherLesJoueursToolStripMenuItem.Click += new System.EventHandler(this.GetAllPlayerToolStripMenuItem_Click);
            // 
            // miseÀJourDesQuêtesToolStripMenuItem
            // 
            this.miseÀJourDesQuêtesToolStripMenuItem.Name = "miseÀJourDesQuêtesToolStripMenuItem";
            this.miseÀJourDesQuêtesToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.miseÀJourDesQuêtesToolStripMenuItem.Text = "Mise à jour des quêtes";
            this.miseÀJourDesQuêtesToolStripMenuItem.Click += new System.EventHandler(this.MAJQuestToolStripMenuItem_Click);
            // 
            // miseÀJourDesNomsDesJoueursToolStripMenuItem
            // 
            this.miseÀJourDesNomsDesJoueursToolStripMenuItem.Name = "miseÀJourDesNomsDesJoueursToolStripMenuItem";
            this.miseÀJourDesNomsDesJoueursToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.miseÀJourDesNomsDesJoueursToolStripMenuItem.Text = "Mise à jour des noms des joueurs";
            this.miseÀJourDesNomsDesJoueursToolStripMenuItem.Click += new System.EventHandler(this.miseÀJourDesNomsDesJoueursToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1509, 862);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panelWebBrowser);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.webBrowserMain);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cDRBindingSource)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playerBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.WebBrowser webBrowserMain;
        private System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.BindingSource cDRBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn numberInSystemDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn galaxyDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn systemDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn Position;
        private DataGridViewTextBoxColumn ferDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn oRDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn cristalDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn totalDataGridViewTextBoxColumn;
        private Panel panelWebBrowser;
        private Button button1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem joueurToolStripMenuItem;
        private ToolStripMenuItem rechercheCDRToolStripMenuItem;
        private ToolStripMenuItem administrateurToolStripMenuItem;
        private ToolStripMenuItem rechercherLesJoueursToolStripMenuItem;
        private BindingSource playerBindingSource;
        private ToolStripMenuItem chercherUnJoueurToolStripMenuItem;
        private ToolStripMenuItem miseÀJourDesQuêtesToolStripMenuItem;
        private ToolStripMenuItem miseÀJourDesNomsDesJoueursToolStripMenuItem;
    }
}

