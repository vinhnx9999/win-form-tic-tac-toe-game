namespace WinTicTacToe
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
            this.pnShow = new System.Windows.Forms.Panel();
            this.lbRule = new System.Windows.Forms.Label();
            this.ptbPayer = new System.Windows.Forms.PictureBox();
            this.psbCooldownTime = new System.Windows.Forms.ProgressBar();
            this.txtNamePlayer = new System.Windows.Forms.TextBox();
            this.imgAvata = new System.Windows.Forms.PictureBox();
            this.tmCountDown = new System.Windows.Forms.Timer(this.components);
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.menuGame = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.player1VsPlayer2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.pnMain = new System.Windows.Forms.Panel();
            this.pnShow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbPayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgAvata)).BeginInit();
            this.mainMenu.SuspendLayout();
            this.pnMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnShow
            // 
            this.pnShow.BackColor = System.Drawing.Color.BurlyWood;
            this.pnShow.Controls.Add(this.lbRule);
            this.pnShow.Controls.Add(this.ptbPayer);
            this.pnShow.Controls.Add(this.psbCooldownTime);
            this.pnShow.Controls.Add(this.txtNamePlayer);
            this.pnShow.Controls.Add(this.imgAvata);
            this.pnShow.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnShow.Location = new System.Drawing.Point(674, 0);
            this.pnShow.Name = "pnShow";
            this.pnShow.Size = new System.Drawing.Size(234, 519);
            this.pnShow.TabIndex = 4;
            // 
            // lbRule
            // 
            this.lbRule.AutoSize = true;
            this.lbRule.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbRule.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRule.Location = new System.Drawing.Point(3, 469);
            this.lbRule.Name = "lbRule";
            this.lbRule.Size = new System.Drawing.Size(190, 29);
            this.lbRule.TabIndex = 4;
            this.lbRule.Text = " 5 in a line to win";
            // 
            // ptbPayer
            // 
            this.ptbPayer.BackColor = System.Drawing.Color.Gray;
            this.ptbPayer.Location = new System.Drawing.Point(151, 286);
            this.ptbPayer.Name = "ptbPayer";
            this.ptbPayer.Size = new System.Drawing.Size(80, 86);
            this.ptbPayer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptbPayer.TabIndex = 3;
            this.ptbPayer.TabStop = false;
            // 
            // psbCooldownTime
            // 
            this.psbCooldownTime.Location = new System.Drawing.Point(6, 335);
            this.psbCooldownTime.Maximum = 20000;
            this.psbCooldownTime.Name = "psbCooldownTime";
            this.psbCooldownTime.Size = new System.Drawing.Size(139, 37);
            this.psbCooldownTime.Step = 100;
            this.psbCooldownTime.TabIndex = 1;
            // 
            // txtNamePlayer
            // 
            this.txtNamePlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNamePlayer.Location = new System.Drawing.Point(6, 286);
            this.txtNamePlayer.Multiline = true;
            this.txtNamePlayer.Name = "txtNamePlayer";
            this.txtNamePlayer.ReadOnly = true;
            this.txtNamePlayer.Size = new System.Drawing.Size(139, 37);
            this.txtNamePlayer.TabIndex = 10;
            this.txtNamePlayer.Text = "Player";
            // 
            // imgAvata
            // 
            this.imgAvata.ErrorImage = global::WinTicTacToe.Properties.Resources.Tic_tac_toe;
            this.imgAvata.Image = global::WinTicTacToe.Properties.Resources.Tic_tac_toe;
            this.imgAvata.InitialImage = global::WinTicTacToe.Properties.Resources.Tic_tac_toe;
            this.imgAvata.Location = new System.Drawing.Point(3, 3);
            this.imgAvata.Name = "imgAvata";
            this.imgAvata.Size = new System.Drawing.Size(229, 221);
            this.imgAvata.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgAvata.TabIndex = 0;
            this.imgAvata.TabStop = false;
            // 
            // tmCountDown
            // 
            this.tmCountDown.Interval = 1000;
            this.tmCountDown.Tick += new System.EventHandler(this.TmCountDown_Tick);
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuGame});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(908, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            // 
            // menuGame
            // 
            this.menuGame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNewGame,
            this.menuUndo,
            this.menuQuit});
            this.menuGame.Name = "menuGame";
            this.menuGame.Size = new System.Drawing.Size(50, 20);
            this.menuGame.Text = "Menu";
            // 
            // menuNewGame
            // 
            this.menuNewGame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playToolStripMenuItem,
            this.player1VsPlayer2ToolStripMenuItem});
            this.menuNewGame.Name = "menuNewGame";
            this.menuNewGame.Size = new System.Drawing.Size(144, 22);
            this.menuNewGame.Text = "New game";
            // 
            // playToolStripMenuItem
            // 
            this.playToolStripMenuItem.Name = "playToolStripMenuItem";
            this.playToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.playToolStripMenuItem.Text = "Play vs Computer";
            // 
            // player1VsPlayer2ToolStripMenuItem
            // 
            this.player1VsPlayer2ToolStripMenuItem.Name = "player1VsPlayer2ToolStripMenuItem";
            this.player1VsPlayer2ToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.player1VsPlayer2ToolStripMenuItem.Text = "Player1 vs Player2";
            // 
            // menuUndo
            // 
            this.menuUndo.Name = "menuUndo";
            this.menuUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.menuUndo.Size = new System.Drawing.Size(144, 22);
            this.menuUndo.Text = "Undo";
            // 
            // menuQuit
            // 
            this.menuQuit.Name = "menuQuit";
            this.menuQuit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.menuQuit.Size = new System.Drawing.Size(180, 22);
            this.menuQuit.Text = "Quit";
            this.menuQuit.Click += new System.EventHandler(this.menuQuit_Click);
            // 
            // pnMain
            // 
            this.pnMain.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnMain.Controls.Add(this.mainMenu);
            this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMain.Location = new System.Drawing.Point(0, 0);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(908, 519);
            this.pnMain.TabIndex = 5;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 519);
            this.Controls.Add(this.pnShow);
            this.Controls.Add(this.pnMain);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.Text = ".:: Tic tac toe ::.";
            this.Load += new System.EventHandler(this.Main_Load);
            this.pnShow.ResumeLayout(false);
            this.pnShow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbPayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgAvata)).EndInit();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.pnMain.ResumeLayout(false);
            this.pnMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnShow;
        private System.Windows.Forms.Label lbRule;
        private System.Windows.Forms.PictureBox ptbPayer;
        private System.Windows.Forms.ProgressBar psbCooldownTime;
        private System.Windows.Forms.TextBox txtNamePlayer;
        private System.Windows.Forms.PictureBox imgAvata;
        private System.Windows.Forms.Timer tmCountDown;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem menuGame;
        private System.Windows.Forms.ToolStripMenuItem menuNewGame;
        private System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem player1VsPlayer2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuUndo;
        private System.Windows.Forms.ToolStripMenuItem menuQuit;
        private System.Windows.Forms.Panel pnMain;
    }
}

