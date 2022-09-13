namespace T16
{
    partial class Form1
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
            this.Game = new System.Windows.Forms.Panel();
            this.PlayerInformation = new System.Windows.Forms.Panel();
            this.playerKills = new System.Windows.Forms.Panel();
            this.computerKills = new System.Windows.Forms.Panel();
            this.playerLabel = new System.Windows.Forms.Label();
            this.PlayerNameLabel = new System.Windows.Forms.Label();
            this.PlayerIdLabel = new System.Windows.Forms.Label();
            this.PlayerPhoneLabel = new System.Windows.Forms.Label();
            this.PlayerGamesLabel = new System.Windows.Forms.Label();
            this.computerLabel = new System.Windows.Forms.Label();
            this.playerId = new System.Windows.Forms.TextBox();
            this.errorMessage = new System.Windows.Forms.Label();
            this.Win = new System.Windows.Forms.Panel();
            this.TimerLabel = new System.Windows.Forms.Label();
            this.playAgainLabel = new System.Windows.Forms.Label();
            this.winLabel = new System.Windows.Forms.Label();
            this.playPanel = new System.Windows.Forms.Panel();
            this.play = new System.Windows.Forms.Label();
            this.checkers = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();

            this.Win.SuspendLayout();
            this.playPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Game
            // 
            this.Game.BackgroundImage = global::T16.Properties.Resources.checkers;
            this.Game.BackColor = System.Drawing.Color.Transparent;
            this.Game.Location = new System.Drawing.Point(300, 30);
            this.Game.Name = "G";
            this.Game.Size = new System.Drawing.Size(500, 500);
            this.Game.TabIndex = 0;


            this.TimerLabel.AutoSize = true;
            this.TimerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.TimerLabel.ForeColor = System.Drawing.Color.Black;
            this.TimerLabel.Location = new System.Drawing.Point(500, 550);
            this.TimerLabel.Name = "timer";
            this.TimerLabel.BackColor = System.Drawing.Color.Transparent;
            this.TimerLabel.Size = new System.Drawing.Size(100, 30);
            this.TimerLabel.TabIndex = 5;
            this.TimerLabel.Text = "00:00:00";

            this.PlayerInformation.BackColor = System.Drawing.Color.White;
            this.PlayerInformation.Location = new System.Drawing.Point(300, 30);
            this.PlayerInformation.Name = "PlayerInformation";
            this.PlayerInformation.Size = new System.Drawing.Size(500, 500);
            this.PlayerInformation.TabIndex = 0;
            this.PlayerInformation.Controls.Add(this.PlayerNameLabel);
            this.PlayerInformation.Controls.Add(this.PlayerIdLabel);
            this.PlayerInformation.Controls.Add(this.PlayerPhoneLabel);
            this.PlayerInformation.Controls.Add(this.PlayerGamesLabel);
            this.PlayerInformation.Controls.Add(this.startButton);


            this.PlayerNameLabel.AutoSize = true;
            this.PlayerNameLabel.Location = new System.Drawing.Point(130, 50);
            this.PlayerNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.PlayerNameLabel.ForeColor = System.Drawing.Color.Black;
            this.PlayerNameLabel.Name = "PlayerNameLabel";
            this.PlayerNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.PlayerNameLabel.Size = new System.Drawing.Size(110, 30);
            this.PlayerNameLabel.TabIndex = 0;
            this.PlayerNameLabel.Text = "Player Name : ";

            this.PlayerIdLabel.AutoSize = true;
            this.PlayerIdLabel.Location = new System.Drawing.Point(130, 150);
            this.PlayerIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.PlayerIdLabel.ForeColor = System.Drawing.Color.Black;
            this.PlayerIdLabel.Name = "PlayerIdLabel";
            this.PlayerIdLabel.BackColor = System.Drawing.Color.Transparent;
            this.PlayerIdLabel.Size = new System.Drawing.Size(110, 30);
            this.PlayerIdLabel.TabIndex = 0;
            this.PlayerIdLabel.Text = "Player Id : ";


            this.PlayerPhoneLabel.AutoSize = true;
            this.PlayerPhoneLabel.Location = new System.Drawing.Point(130, 250);
            this.PlayerPhoneLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.PlayerPhoneLabel.ForeColor = System.Drawing.Color.Black;
            this.PlayerPhoneLabel.Name = "PlayerPhoneLabel";
            this.PlayerPhoneLabel.BackColor = System.Drawing.Color.Transparent;
            this.PlayerPhoneLabel.Size = new System.Drawing.Size(110, 30);
            this.PlayerPhoneLabel.TabIndex = 0;
            this.PlayerPhoneLabel.Text = "Phone Number : ";

            this.PlayerGamesLabel.AutoSize = true;
            this.PlayerGamesLabel.Location = new System.Drawing.Point(130, 350);
            this.PlayerGamesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.PlayerGamesLabel.ForeColor = System.Drawing.Color.Black;
            this.PlayerGamesLabel.Name = "PlayerGamesLabel";
            this.PlayerGamesLabel.BackColor = System.Drawing.Color.Transparent;
            this.PlayerGamesLabel.Size = new System.Drawing.Size(110, 30);
            this.PlayerGamesLabel.TabIndex = 0;
            this.PlayerGamesLabel.Text = "Number Of Games : ";

            this.startButton.AutoSize = true;
            this.startButton.Location = new System.Drawing.Point(200, 450);
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.startButton.ForeColor = System.Drawing.Color.Black;
            this.startButton.Name = "startButton";
            this.startButton.BackColor = System.Drawing.Color.Gray;
            this.startButton.Size = new System.Drawing.Size(100, 40);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start";

            this.playerKills.BackColor = System.Drawing.Color.Transparent;
            this.playerKills.Location = new System.Drawing.Point(900, 100);
            this.playerKills.Name = "p";
            this.playerKills.Size = new System.Drawing.Size(130, 390);
            this.playerKills.TabIndex = 0;

            this.computerKills.BackColor = System.Drawing.Color.Transparent;
            this.computerKills.Location = new System.Drawing.Point(100, 100);
            this.computerKills.Name = "c";
            this.computerKills.Size = new System.Drawing.Size(130, 390);
            this.computerKills.TabIndex = 0;

            // 
            // label1
            // 
            this.playerLabel.AutoSize = true;
            this.playerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
            this.playerLabel.ForeColor = System.Drawing.Color.Black;
            this.playerLabel.Location = new System.Drawing.Point(900, 30);
            this.playerLabel.Name = "label1";
            this.playerLabel.BackColor = System.Drawing.Color.Transparent;
            this.playerLabel.Size = new System.Drawing.Size(110, 30);
            this.playerLabel.TabIndex = 5;
            this.playerLabel.Text = "Player";
            // 
            // label2
            // 
            this.computerLabel.AutoSize = true;
            this.computerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
            this.computerLabel.ForeColor = System.Drawing.Color.Black;
            this.computerLabel.Location = new System.Drawing.Point(100, 30);
            this.computerLabel.Name = "label2";
            this.computerLabel.BackColor = System.Drawing.Color.Transparent;
            this.computerLabel.Size = new System.Drawing.Size(110, 30);
            this.computerLabel.TabIndex = 6;
            this.computerLabel.Text = "Computer";

            //
            //win panel
            //
            this.Win.BackColor = System.Drawing.Color.Gold;
            this.Win.Controls.Add(this.playPanel);
            this.Win.Controls.Add(this.playAgainLabel);
            this.Win.Controls.Add(this.winLabel);
            this.Win.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Win.Location = new System.Drawing.Point(0, 0);
            this.Win.Name = "W";
            this.Win.Size = new System.Drawing.Size(800, 517);
            this.Win.TabIndex = 0;
            this.Win.Visible = false;
            // 
            // Play Again Label
            // 
            this.playAgainLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.playAgainLabel.ForeColor = System.Drawing.Color.White;
            this.playAgainLabel.Location = new System.Drawing.Point(317, 252);
            this.playAgainLabel.Name = "label4";
            this.playAgainLabel.Size = new System.Drawing.Size(162, 51);
            this.playAgainLabel.TabIndex = 1;
            this.playAgainLabel.Text = "Play again";
            this.playAgainLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.playAgainLabel.Click += new System.EventHandler(this.playAgainLabel_Click);
            // 
            // Win label
            // 
            this.winLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Bold);
            this.winLabel.ForeColor = System.Drawing.Color.White;
            this.winLabel.Location = new System.Drawing.Point(99, 153);
            this.winLabel.Name = "labelw";
            this.winLabel.Size = new System.Drawing.Size(607, 111);
            this.winLabel.TabIndex = 0;
            this.winLabel.Text = "You win";
            this.winLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // playPanel
            // 
            this.playPanel.BackColor = System.Drawing.Color.Transparent;
            this.playPanel.Controls.Add(this.play);
            this.playPanel.Controls.Add(this.playerId);
            this.playPanel.Controls.Add(this.errorMessage);
            this.playPanel.Controls.Add(this.checkers);
            this.playPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playPanel.Location = new System.Drawing.Point(0, 0);
            this.playPanel.Name = "panel1";
            this.playPanel.Size = new System.Drawing.Size(1100, 600);
            this.playPanel.TabIndex = 2;
            // 
            // play label
            // 
            this.play.AutoSize = true;
            this.play.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.play.ForeColor = System.Drawing.Color.White;
            this.play.Location = new System.Drawing.Point(500, 275);
            this.play.Name = "play";
            this.play.Size = new System.Drawing.Size(100, 50);
            this.play.TabIndex = 0;
            this.play.Click += new System.EventHandler(this.play_Click);
            this.play.Text = "Play";

            this.playerId.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.playerId.Location = new System.Drawing.Point(400, 200);
            this.playerId.Name = "PlayerId";
            this.playerId.Size = new System.Drawing.Size(300, 30);
            this.playerId.TabIndex = 7;
            this.playerId.Text = "Enter Your ID : (1-1000)";

            this.errorMessage.AutoSize = true;
            this.errorMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorMessage.ForeColor = System.Drawing.Color.Red;
            this.errorMessage.Location = new System.Drawing.Point(400, 240);
            this.errorMessage.Name = "errorMessage";
            this.errorMessage.Size = new System.Drawing.Size(20, 10);
            this.errorMessage.TabIndex = 0;
            this.errorMessage.Text = "";

            //checkers label
            this.checkers.AutoSize = true;
            this.checkers.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkers.ForeColor = System.Drawing.Color.White;
            this.checkers.Location = new System.Drawing.Point(430, 100);
            this.checkers.Name = "checkers";
            this.checkers.Size = new System.Drawing.Size(200, 100);
            this.checkers.TabIndex = 0;
            this.checkers.Text = "CHECKERS";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::T16.Properties.Resources.wood_texture;
            this.ClientSize = new System.Drawing.Size(1100, 600);
            this.Controls.Add(this.Win);
            this.Controls.Add(this.computerLabel);
            this.Controls.Add(this.playerLabel);
            this.Controls.Add(this.Game);
            this.Controls.Add(this.TimerLabel);
            this.Controls.Add(this.PlayerInformation);
            this.Controls.Add(this.playerKills);
            this.Controls.Add(this.computerKills);

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Checkers";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Win.ResumeLayout(false);
            this.playPanel.ResumeLayout(false);
            this.playPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Game;
        private System.Windows.Forms.Panel PlayerInformation;
        private System.Windows.Forms.Panel playerKills;
        private System.Windows.Forms.Panel computerKills;
        private System.Windows.Forms.Label playerLabel;
        private System.Windows.Forms.Label PlayerNameLabel;
        private System.Windows.Forms.Label PlayerIdLabel;
        private System.Windows.Forms.Label PlayerPhoneLabel;
        private System.Windows.Forms.Label PlayerGamesLabel;
        private System.Windows.Forms.Label TimerLabel;
        private System.Windows.Forms.Label computerLabel;
        private System.Windows.Forms.Label errorMessage;
        private System.Windows.Forms.TextBox playerId;
        private System.Windows.Forms.Panel Win;
        private System.Windows.Forms.Label playAgainLabel;
        private System.Windows.Forms.Label winLabel;
        private System.Windows.Forms.Panel playPanel;
        private System.Windows.Forms.Label play;
        private System.Windows.Forms.Label checkers;
        private System.Windows.Forms.Button startButton;

    }
}

