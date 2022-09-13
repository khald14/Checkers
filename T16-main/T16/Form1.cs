using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Threading;



using T16.Model;
using System.Net.Http;

namespace T16
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();

        private HttpClient client = new HttpClient();
        public class Info
        {
            public PictureBox availableWhitePiece = new PictureBox();
            public List<string> pathsAvailable = new List<string>();

            public Info(PictureBox p, List<string> paths)
            {
                availableWhitePiece = p;
                pathsAvailable = paths;
            }
        }
        public Form1()
        {
            InitializeComponent();
        }
        int n;
        int id = 0;
        PictureBox[,] P, Pk, Ck;

        List<Info> whitePiecesInfo = new List<Info>();
        Game game = new Game();
        User user = new User();

        int serverMove1 = 0;
        int serverMove2 = 0;

        int hh = 0, mm = 0, ss = 0;
        string color = "b", me = "", path1 = "", path2 = "", path3 = "", path4 = "", kill1 = "", kill2 = "", kill3 = "", kill4 = "";
        int xp1 = -1, yp1 = -1, xp2 = -1, yp2 = -1, xp3 = -1, yp3 = -1, xp4 = -1, yp4 = -1;
        bool justkill = false;
        bool decideToKill = false;
        bool justBeKing = false;
        int PlayerCounteri = 0, PlayerCounterj = 0;
        int ComputerCounteri = 0, ComputerCounterj = 0;


        private async void playAgainLabel_Click(object sender, EventArgs e)
        {
            Win.Visible = false;
            white = 0;              black = 0;

            whitePiecesInfo = new List<Info>();

            IEnumerable<Game> allGames = await GetAllGamesAsync("api/Games");
            game.Id = allGames.Count();
            game.GameStartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            serverMove1 = 0;
            serverMove2 = 0;

            hh = 0; mm = 0;  ss = 0;
            color = "b"; me = "";
            EraseKills(); ErasePaths();
            xp1 = -1; yp1 = -1; xp2 = -1; yp2 = -1; xp3 = -1; yp3 = -1; xp4 = -1; yp4 = -1;
             justkill = false;
             decideToKill = false;
             justBeKing = false;
            PlayerCounteri = 0; PlayerCounterj = 0;
            ComputerCounteri = 0; ComputerCounterj = 0;
            n = 8;
            P = new PictureBox[n, n]; //make the table nxn
            Pk = new PictureBox[n, 2];
            Ck = new PictureBox[n, 2];

            CreateKills();

            int left = 20, top = 20;  // padding from the left and top
            for (int h = 0; h < n; h++)
                for (int l = 0; l < n; l++)
                {
                    P[h, l].BackColor = Color.Transparent;
                    P[h, l].Location = new Point(left, top); // where to begin
                    P[h, l].Size = new Size(60, 60); // the size of the box
                    left += 57; // at every box we do +57 to make the other box
                    P[h, l].Name = h + " " + l; //the name of the box is the indexes of i j, for example: 0 0 , 0 1, 0 2 ....
                    if (h < (n / 2) - 1 && l % 2 == 0 && h % 2 == 0) // create the white pieces on the table; on row 0 2 on the black boxes.
                    {
                        P[h, l].Image = Properties.Resources.w;
                        P[h, l].Name += " w";  // appeand the color character to the box name
                    }
                    else if (h > (n / 2) && l % 2 == 0 && h % 2 == 0)// create the black pieces on the table; on row 6 on the black boxes.
                    {
                        P[h, l].Image = Properties.Resources.b;
                        P[h, l].Name += " b"; // appeand the color character to the box name
                    }

                    if (h < (n / 2) - 1 && l % 2 == 1 && h % 2 == 1) // create the white pieces on the table; on row 1 on the black boxes.
                    {
                        P[h, l].Image = Properties.Resources.w;
                        P[h, l].Name += " w";// appeand the color character to the box name
                    }
                    else if (h > (n / 2) && l % 2 == 1 && h % 2 == 1)// create the black pieces on the table; on row 5 7 on the black boxes.
                    {
                        P[h, l].Image = Properties.Resources.b;
                        P[h, l].Name += " b"; // appeand the color character to the box name
                    }
                    P[h, l].SizeMode = PictureBoxSizeMode.CenterImage;
                }

        }


        int black = 0, white = 0;


        private async void play_Click(object sender, EventArgs e)
        {
            //check if the entered player id is not string 
            if (Regex.IsMatch(this.playerId.Text, @"^\d+$"))
            {

                id = Convert.ToInt32(this.playerId.Text);
                //get the users with this id, 1 if found and 0 if not found.
                IEnumerable<User> u = await GetUserAsync("api/Users/id/" + id);
                //get all user games.
                IEnumerable<Game> games = await GetGamesAsync("api/Games/id/" + id);

                if (u.Count() > 0)
                {
                    user = u.ElementAt(0);
                    PlayerNameLabel.Text = PlayerNameLabel.Text + user.Name;
                    PlayerIdLabel.Text = PlayerIdLabel.Text + user.Id + "";
                    PlayerPhoneLabel.Text = PlayerPhoneLabel.Text + user.PhoneNumber;
                    PlayerGamesLabel.Text = PlayerGamesLabel.Text + games.Count() + "";
                    playerLabel.Text = user.Name;

                    checkers.Visible = false;
                    errorMessage.Visible = false;
                    playerId.Visible = false;
                    play.Visible = false;
                    PlayerInformation.Visible = true;
                    PlayerInformation.BringToFront();

                    startButton.Click += async (sender3, e3) =>
                    {
                        playPanel.Visible = false;
                        PlayerInformation.Visible = false;
                        IEnumerable<Game> allGames = await GetAllGamesAsync("api/Games");

                        game.Id = allGames.Count()+1;
                        game.UserId = user.Id;
                        game.GameStartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); ;
                    };
                }
                else
                    errorMessage.Text = "User Not Found, SignUp First!";
            }
            else
                errorMessage.Text = "Enter numeric id";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            client.BaseAddress = new Uri("https://localhost:44359/");

            t.Interval = 1000;  //in milliseconds
            t.Tick += new EventHandler(this.t_Tick);
            //start timer when form loads
            t.Start();  //this will use t_Tick() method

            //make play panel visable when start the form.
            playPanel.Parent = this;
            playPanel.BringToFront();
            playerId.Click += (sender3, e3) =>
            {
                this.playerId.Text = "";
            };

            // when mouse hover above the play label, make label black and background green.
            play.MouseHover += (sender3, e3) =>
           {
               Label l = sender3 as Label;
               l.ForeColor = Color.Black;
           };
            // when  mouse leave hover above the play label, return defaults.
            play.MouseLeave += (sender3, e3) =>
            {
                Label l = sender3 as Label;
                l.ForeColor = Color.White;
            };

            n = 8;
            P = new PictureBox[n, n]; //make the table nxn
            Pk = new PictureBox[n, 2];
            Ck = new PictureBox[n, 2];

            CreateKills();

            int left = 20, top = 20;  // padding from the left and top

            for (int i = 0; i < n; i++)            // For each row i
            {
                left = 20;
                for (int j = 0; j < n; j++)     // For each column j
                {
                    P[i, j] = new PictureBox(); // picture box at i,j
                    P[i, j].BackColor = Color.Transparent;
                    P[i, j].Location = new Point(left, top); // where to begin
                    P[i, j].Size = new Size(60, 60); // the size of the box
                    left += 57; // at every box we do +57 to make the other box
                    P[i, j].Name = i + " " + j; //the name of the box is the indexes of i j, for example: 0 0 , 0 1, 0 2 ....
                    if (i < (n / 2) - 1 && j % 2 == 0 && i % 2 == 0) // create the white pieces on the table; on row 0 2 on the black boxes.
                    {
                        P[i, j].Image = Properties.Resources.w;
                        P[i, j].Name += " w";  // appeand the color character to the box name
                    }
                    else if (i > (n / 2) && j % 2 == 0 && i % 2 == 0)// create the black pieces on the table; on row 6 on the black boxes.
                    {
                        P[i, j].Image = Properties.Resources.b;
                        P[i, j].Name += " b"; // appeand the color character to the box name
                    }

                    if (i < (n / 2) - 1 && j % 2 == 1 && i % 2 == 1) // create the white pieces on the table; on row 1 on the black boxes.
                    {
                        P[i, j].Image = Properties.Resources.w;
                        P[i, j].Name += " w";// appeand the color character to the box name
                    }
                    else if (i > (n / 2) && j % 2 == 1 && i % 2 == 1)// create the black pieces on the table; on row 5 7 on the black boxes.
                    {
                        P[i, j].Image = Properties.Resources.b;
                        P[i, j].Name += " b"; // appeand the color character to the box name
                    }
                    P[i, j].SizeMode = PictureBoxSizeMode.CenterImage;


                    //-----------------------------------------MouseHover-----------------------------------
                    P[i, j].MouseHover += (sender2, e2) =>
                    {
                        PictureBox p = sender2 as PictureBox;
                        if (p.Image != null)
                        {
                            if (p.Name.Split(' ')[2] == "w")
                                p.Image = Properties.Resources.gw;
                            else if (p.Name.Split(' ')[2] == "b")
                                p.Image = Properties.Resources.gb;
                        }
                    };
                    P[i, j].MouseLeave += (sender2, e2) =>
                    {
                        PictureBox p = sender2 as PictureBox;
                        if (p.Image != null)
                        {
                            if (p.Name.Split(' ')[2] == "w")
                                p.Image = Properties.Resources.w;
                            else if (p.Name.Split(' ')[2] == "b")
                                p.Image = Properties.Resources.b;
                        }
                    };


                    //-------------------------------------------------Box Clicked-------------------------------------------
                    P[i, j].Click += async (sender3, e3) =>
                    {
                        decideToKill = false;
                        justBeKing = false;
                        if (kill1 == "" && kill2 == "" && kill3 == "" && kill4 == "")
                            EnableAll();

                        // if (Player1.ReadOnly && Player2.ReadOnly)
                        //{
                        PictureBox p = sender3 as PictureBox;
                        if (p.Image != null) // if the clicked box does not have an image (white or black piece)
                        {
                            int x, y;
                            F(); //

                            //clicked gray-white or black-white
                            if (p.Name.Split(' ')[2] == "g") // if the clicked box is one of the two moves
                            {

                                x = Convert.ToInt32(me.Split(' ')[0]);
                                y = Convert.ToInt32(me.Split(' ')[1]);

                                if (me.Split(' ')[2] == "w")
                                {
                                    if (Convert.ToInt32(p.Name.Split(' ')[0]) == 7)
                                    {
                                        p.Image = Properties.Resources.kw;
                                        p.Name = p.Name.Replace("g", "q");
                                        justBeKing = true;
                                    }
                                    else
                                    {
                                        p.Image = Properties.Resources.w;
                                        p.Name = p.Name.Replace("g", "w");
                                    }
                                }

                                else
                                if (me.Split(' ')[2] == "b")
                                {
                                    if (Convert.ToInt32(p.Name.Split(' ')[0]) == 0)
                                    {
                                        p.Image = Properties.Resources.kb;
                                        p.Name = p.Name.Replace("g", "k");
                                        justBeKing = true;
                                    }
                                    else
                                    {
                                        p.Image = Properties.Resources.b;
                                        p.Name = p.Name.Replace("g", "b");
                                    }
                                }
                                else if (me.Split(' ')[2] == "k")
                                {
                                    p.Image = Properties.Resources.kb;
                                    p.Name = p.Name.Replace("g", "k");
                                }
                                else if (me.Split(' ')[2] == "q")
                                {
                                    p.Image = Properties.Resources.kw;
                                    p.Name = p.Name.Replace("g", "q");
                                }

                                P[x, y].Image = null;



                                PerformKill(p, x, y);
                                await GetServerMove1And2();
                                ServerTurn();
                                ChangeTurn();


                            }

                            //clicked black or white 
                            else
                            {
                                if (p.Name.Split(' ')[2] == color)
                                {//if the color we pressed on matches "color" (its the player turn). or king color.
                                    if (color == "b")
                                        GetKillsAndPaths(p);
                                }
                                else
                                {
                                    if (p.Name.Split(' ')[2] == "k" && color == "b")
                                        GetKillsAndPathsForKings(p);

                                }
                            }
                        }
                    };
                    Game.Controls.Add(P[i, j]);
                }
                top += 57;
            }
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormIsClosing);

        }

        public void GetKillsAndPaths(PictureBox p)
        {
            path1 = ""; path2 = ""; kill1 = ""; kill2 = "";
            int x = Convert.ToInt32(p.Name.Split(' ')[0]); //x = row pos
            int y = Convert.ToInt32(p.Name.Split(' ')[1]); // y = col pos
            int turn;
            me = p.Name; // the name of the box
            if (p.Name.Split(' ')[2] == "w") turn = 1; else turn = -1; //if its computer turn then turn = 1, else keep it -1 ( player turn )
            try
            {
                if (P[x + turn, y + 1].Image == null) //check the way for current box; if there are embty box near it we mark it as gray
                {
                    if (turn == 1) // depending on the turn we add the image resource.
                        P[x + turn, y + 1].Image = Properties.Resources.gw;
                    else
                        P[x + turn, y + 1].Image = Properties.Resources.gb;

                    P[x + turn, y + 1].Name = (x + turn) + " " + (y + 1) + " g"; // give it the name + g
                    path1 = (x + turn) + " " + (y + 1); //path1 saves the pos of row and col 
                }
                else
                    if (P[x + turn, y + 1].Name.Split(' ')[2] != p.Name.Split(' ')[2] && P[x + (turn * 2), y + 2].Image == null)
                {// if there are kills....

                    if (turn == 1) // depending on the turn we add the image resource.
                        P[x + (turn * 2), y + 2].Image = Properties.Resources.gw; //we add to the kill pos the gray image resource
                    else
                        P[x + (turn * 2), y + 2].Image = Properties.Resources.gb;  //we add to the kill pos the gray image resource

                    P[x + (turn * 2), y + 2].Name = (x + (turn * 2)) + " " + (y + 2) + " g"; //adjust the name of the kill box 
                    path1 = (x + (turn * 2)) + " " + (y + 2); // the path for this box (i,j) for the end of the kill
                    kill1 = (x + turn) + " " + (y + 1) + " " + P[x + turn, y + 1].Name.Split(' ')[2]; // the killed piece location
                }
            }

            // we do the same thing as above but in the other way (the second column)
            catch { }
            try
            {
                if (P[x + turn, y - 1].Image == null)//check the other way for current box; if there are embty box near it we mark it as blue
                {
                    if (turn == 1)
                        P[x + turn, y - 1].Image = Properties.Resources.gw;
                    else
                        P[x + turn, y - 1].Image = Properties.Resources.gb;

                    P[x + turn, y - 1].Name = (x + turn) + " " + (y - 1) + " g";
                    path2 = (x + turn) + " " + (y - 1);
                }
                else
                    if (P[x + turn, y - 1].Name.Split(' ')[2] != p.Name.Split(' ')[2] && P[x + (turn * 2), y - 2].Image == null)
                {//if there are kills...

                    if (turn == 1)
                        P[x + (turn * 2), y - 2].Image = Properties.Resources.gw;
                    else
                        P[x + (turn * 2), y - 2].Image = Properties.Resources.gb;

                    P[x + (turn * 2), y - 2].Name = (x + (turn * 2)) + " " + (y - 2) + " g";
                    path2 = (x + (turn * 2)) + " " + (y - 2);
                    kill2 = (x + turn) + " " + (y - 1) + " " + P[x + turn, y - 1].Name.Split(' ')[2];
                }
            }
            catch { }


            if (justkill)
            {
                EnableAll();
                if (kill1 == "" && path1 != "")
                {
                    P[x + turn, y + 1].Image = null;
                    P[x + turn, y + 1].Name = (x + turn) + " " + (y - 1);
                    path1 = "";

                }
                if (kill2 == "" && path2 != "")
                {
                    P[x + turn, y - 1].Image = null;
                    P[x + turn, y - 1].Name = (x + turn) + " " + (y - 1);
                    path2 = "";
                }
                justkill = false;
            }

        }
        public void PerformKill(PictureBox p, int x, int y)
        {
            int xk = Convert.ToInt32(p.Name.Split(' ')[0]); // the kill position
            int yk = Convert.ToInt32(p.Name.Split(' ')[1]);

            GetPathsPoss();

            if (kill1 != "" || kill2 != "" || kill3 != "" || kill4 != "")
            {
                if (xk == xp1 && yk == yp1) //if the user picked kill1 
                {
                    if (kill1 != "" && p.Name.Split(' ')[0] != kill1.Split(' ')[0])
                    {
                        decideToKill = true;
                        //kill1
                        x = Convert.ToInt32(kill1.Split(' ')[0]);
                        y = Convert.ToInt32(kill1.Split(' ')[1]);
                        P[x, y].Image = null;
                        UpdateKillsAndCheckContinuesKills(kill1, path1, p);
                    }
                }

                if (xk == xp2 && yk == yp2)//if the user picked kill2
                {
                    //kill2
                    if (kill2 != "" && p.Name.Split(' ')[0] != kill2.Split(' ')[0])
                    {
                        decideToKill = true;
                        x = Convert.ToInt32(kill2.Split(' ')[0]);
                        y = Convert.ToInt32(kill2.Split(' ')[1]);
                        P[x, y].Image = null;
                        UpdateKillsAndCheckContinuesKills(kill2, path2, p);
                    }
                }

                if (xk == xp3 && yk == yp3) //if the user picked kill3
                {
                    if (kill3 != "" && p.Name.Split(' ')[0] != kill3.Split(' ')[0])
                    {
                        decideToKill = true;
                        //kill3
                        x = Convert.ToInt32(kill3.Split(' ')[0]);
                        y = Convert.ToInt32(kill3.Split(' ')[1]);
                        P[x, y].Image = null;
                        UpdateKillsAndCheckContinuesKills(kill3, path3, p);
                    }
                }
                if (xk == xp4 && yk == yp4) //if the user picked kill4
                {
                    if (kill4 != "" && p.Name.Split(' ')[0] != kill4.Split(' ')[0])
                    {
                        decideToKill = true;
                        //kill4
                        x = Convert.ToInt32(kill4.Split(' ')[0]);
                        y = Convert.ToInt32(kill4.Split(' ')[1]);
                        P[x, y].Image = null;
                        UpdateKillsAndCheckContinuesKills(kill4, path4, p);
                    }
                }
            }

            if (!decideToKill)
            {
                EraseKills();
                ErasePaths();
                ChangeTurn();
            }

        }
        public void GetKillsAndPathsForKings(PictureBox p)
        {
            path1 = ""; path2 = ""; path3 = ""; path4 = ""; kill1 = ""; kill2 = ""; kill3 = ""; kill4 = "";
            int x = Convert.ToInt32(p.Name.Split(' ')[0]); //x = row pos
            int y = Convert.ToInt32(p.Name.Split(' ')[1]); // y = col pos
            int turn;
            me = p.Name; // the name of the box
            if (p.Name.Split(' ')[2] == "q") turn = 1; else turn = -1; //if its computer turn then turn = 1, else keep it -1 ( player turn )

            try
            {
                if (P[x + turn, y + 1].Image == null) //check the way for current box; if there are embty box near it we mark it as gray
                {
                    if (turn == 1) // depending on the turn we add the image resource.
                        P[x + turn, y + 1].Image = Properties.Resources.kwg;
                    else
                        P[x + turn, y + 1].Image = Properties.Resources.kbg;

                    P[x + turn, y + 1].Name = (x + turn) + " " + (y + 1) + " g"; // give it the name + g
                    path1 = (x + turn) + " " + (y + 1); //path1 saves the pos of row and col 
                }
                else if (((p.Name.Split(' ')[2] == "k") && (P[x + turn, y + 1].Name.Split(' ')[2] == "q" || P[x + turn, y + 1].Name.Split(' ')[2] == "w") ||
                    (p.Name.Split(' ')[2] == "q") && (P[x + turn, y + 1].Name.Split(' ')[2] == "b" || P[x + turn, y + 1].Name.Split(' ')[2] == "k")) && P[x + (turn * 2), y + 2].Image == null)

                {// if there are kills....

                    if (turn == 1) // depending on the turn we add the image resource.
                        P[x + (turn * 2), y + 2].Image = Properties.Resources.kwg; //we add to the kill pos the gray image resource
                    else
                        P[x + (turn * 2), y + 2].Image = Properties.Resources.kbg;  //we add to the kill pos the gray image resource

                    P[x + (turn * 2), y + 2].Name = (x + (turn * 2)) + " " + (y + 2) + " g"; //adjust the name of the kill box 
                    path1 = (x + (turn * 2)) + " " + (y + 2); // the path for this box (i,j) for the end of the kill
                    kill1 = (x + turn) + " " + (y + 1) + " " + P[x + turn, y + 1].Name.Split(' ')[2]; // the killed piece location

                }
            }

            // we do the same thing as above but in the other way (the second column)
            catch { }
            try
            {
                if (P[x + turn, y - 1].Image == null)//check the other way for current box; if there are embty box near it we mark it as blue
                {
                    if (turn == 1)
                        P[x + turn, y - 1].Image = Properties.Resources.kwg;
                    else
                        P[x + turn, y - 1].Image = Properties.Resources.kbg;

                    P[x + turn, y - 1].Name = (x + turn) + " " + (y - 1) + " g";
                    path2 = (x + turn) + " " + (y - 1);
                }
                else if (((p.Name.Split(' ')[2] == "k") && (P[x + turn, y - 1].Name.Split(' ')[2] == "q" || P[x + turn, y - 1].Name.Split(' ')[2] == "w") ||
                                   (p.Name.Split(' ')[2] == "q") && (P[x + turn, y - 1].Name.Split(' ')[2] == "b" || P[x + turn, y - 1].Name.Split(' ')[2] == "k")) && P[x + (turn * 2), y - 2].Image == null)
                {//if there are kills...

                    if (turn == 1)
                        P[x + (turn * 2), y - 2].Image = Properties.Resources.kwg;
                    else
                        P[x + (turn * 2), y - 2].Image = Properties.Resources.kbg;

                    P[x + (turn * 2), y - 2].Name = (x + (turn * 2)) + " " + (y - 2) + " g";
                    path2 = (x + (turn * 2)) + " " + (y - 2);
                    kill2 = (x + turn) + " " + (y - 1) + " " + P[x + turn, y - 1].Name.Split(' ')[2];
                }
            }
            catch { }

            try
            {
                if (P[x - turn, y + 1].Image == null) //check the way for current box; if there are embty box near it we mark it as gray
                {
                    if (turn == 1) // depending on the turn we add the image resource.
                        P[x - turn, y + 1].Image = Properties.Resources.kwg;
                    else
                        P[x - turn, y + 1].Image = Properties.Resources.kbg;

                    P[x - turn, y + 1].Name = (x - turn) + " " + (y + 1) + " g"; // give it the name + g
                    path3 = (x - turn) + " " + (y + 1); //path1 saves the pos of row and col 
                }
                else if (((p.Name.Split(' ')[2] == "k") && (P[x - turn, y + 1].Name.Split(' ')[2] == "q" || P[x - turn, y + 1].Name.Split(' ')[2] == "w") ||
                    (p.Name.Split(' ')[2] == "q") && (P[x - turn, y + 1].Name.Split(' ')[2] == "b" || P[x - turn, y + 1].Name.Split(' ')[2] == "k")) && P[x - (turn * 2), y + 2].Image == null)

                {// if there are kills....

                    if (turn == 1) // depending on the turn we add the image resource.
                        P[x - (turn * 2), y + 2].Image = Properties.Resources.kwg; //we add to the kill pos the gray image resource
                    else
                        P[x - (turn * 2), y + 2].Image = Properties.Resources.kbg;  //we add to the kill pos the gray image resource

                    P[x - (turn * 2), y + 2].Name = (x - (turn * 2)) + " " + (y + 2) + " g"; //adjust the name of the kill box 
                    path3 = (x - (turn * 2)) + " " + (y + 2); // the path for this box (i,j) for the end of the kill
                    kill3 = (x - turn) + " " + (y + 1) + " " + P[x - turn, y + 1].Name.Split(' ')[2]; // the killed piece location

                }
            }

            // we do the same thing as above but in the other way (the second column)
            catch { }
            try
            {
                if (P[x - turn, y - 1].Image == null)//check the other way for current box; if there are embty box near it we mark it as blue
                {
                    if (turn == 1)
                        P[x - turn, y - 1].Image = Properties.Resources.kwg;
                    else
                        P[x - turn, y - 1].Image = Properties.Resources.kbg;

                    P[x - turn, y - 1].Name = (x - turn) + " " + (y - 1) + " g";
                    path4 = (x - turn) + " " + (y - 1);
                }
                else if (((p.Name.Split(' ')[2] == "k") && (P[x - turn, y - 1].Name.Split(' ')[2] == "q" || P[x - turn, y - 1].Name.Split(' ')[2] == "w") ||
                                   (p.Name.Split(' ')[2] == "q") && (P[x - turn, y - 1].Name.Split(' ')[2] == "b" || P[x - turn, y - 1].Name.Split(' ')[2] == "k")) && P[x - (turn * 2), y - 2].Image == null)
                {//if there are kills...

                    if (turn == 1)
                        P[x - (turn * 2), y - 2].Image = Properties.Resources.kwg;
                    else
                        P[x - (turn * 2), y - 2].Image = Properties.Resources.kbg;

                    P[x - (turn * 2), y - 2].Name = (x - (turn * 2)) + " " + (y - 2) + " g";
                    path4 = (x - (turn * 2)) + " " + (y - 2);
                    kill4 = (x - turn) + " " + (y - 1) + " " + P[x - turn, y - 1].Name.Split(' ')[2];
                }
            }
            catch { }


            if (justkill)
            {
                EnableAll();
                if (kill1 == "" && path1 != "")
                {
                    P[x + turn, y + 1].Image = null;
                    P[x + turn, y + 1].Name = (x + turn) + " " + (y + 1);
                    path1 = "";

                }
                if (kill2 == "" && path2 != "")
                {
                    P[x + turn, y - 1].Image = null;
                    P[x + turn, y - 1].Name = (x + turn) + " " + (y - 1);
                    path2 = "";
                }
                if (kill3 == "" && path3 != "")
                {
                    P[x - turn, y + 1].Image = null;
                    P[x - turn, y + 1].Name = (x - turn) + " " + (y + 1);
                    path3 = "";
                }
                if (kill4 == "" && path4 != "")
                {
                    P[x - turn, y - 1].Image = null;
                    P[x - turn, y - 1].Name = (x - turn) + " " + (y - 1);
                    path4 = "";
                }
                justkill = false;
            }
        }
        public void DisableAllNotGrays()
        {
            for (int k = 0; k < n; k++)
            {
                for (int l = 0; l < n; l++)
                {
                    try
                    {
                        if (P[k, l].Name.Split(' ')[2] != "g")
                            P[k, l].Enabled = false;
                    }
                    catch { }
                }
            }
        }
        public void RemoveGrays()
        {
            for (int k = 0; k < n; k++)
            {
                for (int l = 0; l < n; l++)
                {
                    try
                    {
                        if (P[k, l].Name.Split(' ')[2] == "g")
                        {
                            P[k, l].Name = P[k, l].Name.Substring(0, 3);
                            P[k, l].Image = null;
                        }
                    }
                    catch { }
                }
            }
        }
        public void EnableAll()
        {
            for (int k = 0; k < n; k++)
            {
                for (int l = 0; l < n; l++)
                {
                    try
                    {
                        P[k, l].Enabled = true;
                    }
                    catch { }
                }
            }
        }
        public void F()
        {
            if (path1 != "")
            {
                int x, y;
                x = Convert.ToInt32(path1.Split(' ')[0]);
                y = Convert.ToInt32(path1.Split(' ')[1]);
                P[x, y].Image = null;
            }
            if (path2 != "")
            {
                int x, y;
                x = Convert.ToInt32(path2.Split(' ')[0]);
                y = Convert.ToInt32(path2.Split(' ')[1]);
                P[x, y].Image = null;
            }
            if (path3 != "")
            {
                int x, y;
                x = Convert.ToInt32(path3.Split(' ')[0]);
                y = Convert.ToInt32(path3.Split(' ')[1]);
                P[x, y].Image = null;
            }
            if (path4 != "")
            {
                int x, y;
                x = Convert.ToInt32(path4.Split(' ')[0]);
                y = Convert.ToInt32(path4.Split(' ')[1]);
                P[x, y].Image = null;
            }
        }
        public void CreateKills()
        {
            int leftPlayerKills = 0, topPlayerKills = 0;
            int leftComputerKills = 0, topComputerKills = 0;
            for (int i = 0; i < n; i++)
            {
                leftPlayerKills = 0;
                leftComputerKills = 0;
                for (int j = 0; j < 2; j++)
                {
                    Pk[i, j] = new PictureBox
                    {
                        // Image = Properties.Resources.w,
                        Location = new Point(leftPlayerKills, topPlayerKills),
                        Size = new Size(60, 60),
                        BackColor = Color.Transparent,
                        SizeMode = PictureBoxSizeMode.CenterImage
                    };
                    playerKills.Controls.Add(Pk[i, j]);

                    Ck[i, j] = new PictureBox
                    {
                        //Image = Properties.Resources.b,
                        Location = new Point(leftComputerKills, topComputerKills),
                        Size = new Size(60, 60),
                        BackColor = Color.Transparent,
                        SizeMode = PictureBoxSizeMode.CenterImage
                    };
                    computerKills.Controls.Add(Ck[i, j]);

                    leftPlayerKills += 65;
                    leftComputerKills += 65;
                }
                topComputerKills += 65;
                topPlayerKills += 65;
            }
        }
        public void GetPathsPoss()
        {
            if (path1 != "") // path1 position
            {
                xp1 = Convert.ToInt32(path1.Split(' ')[0]);
                yp1 = Convert.ToInt32(path1.Split(' ')[1]);
            }
            if (path2 != "")// path2 position 
            {
                xp2 = Convert.ToInt32(path2.Split(' ')[0]);
                yp2 = Convert.ToInt32(path2.Split(' ')[1]);
            }
            if (path3 != "") // path3 position
            {
                xp3 = Convert.ToInt32(path3.Split(' ')[0]);
                yp3 = Convert.ToInt32(path3.Split(' ')[1]);
            }
            if (path4 != "")// path4 position 
            {
                xp4 = Convert.ToInt32(path4.Split(' ')[0]);
                yp4 = Convert.ToInt32(path4.Split(' ')[1]);
            }
        }
        public void ErasePaths()
        {
            path1 = "";
            path2 = "";
            path3 = "";
            path4 = "";
        }

        public void CountPaths(int count)
        {
            if (path1 != "")
                whitePiecesInfo.ElementAt(count).pathsAvailable.Add(path1);
            if (path2 != "")
                whitePiecesInfo.ElementAt(count).pathsAvailable.Add(path2);
            if (path3 != "")
                whitePiecesInfo.ElementAt(count).pathsAvailable.Add(path3);
            if (path4 != "")
                whitePiecesInfo.ElementAt(count).pathsAvailable.Add(path4);
        }

        public void EraseKills()
        {
            kill1 = "";
            kill2 = "";
            kill3 = "";
            kill4 = "";
        }
        public void ChangeTurn()
        {
            if (color == "w") //which color have the turn.
                color = "b";
            else
                color = "w";
        }
        public void UpdateWhiteKills()
        {
            white++;
            Pk[PlayerCounteri++, PlayerCounterj].Image = Properties.Resources.w;
            if (PlayerCounteri == 6)
            {
                PlayerCounterj = 1;
                PlayerCounteri = 0;
            }
        }
        public void UpdateBlackKills()
        {
            black++;
            Ck[ComputerCounteri++, ComputerCounterj].Image = Properties.Resources.b;
            if (ComputerCounteri == 6)
            {
                ComputerCounterj = 1;
                ComputerCounteri = 0;
            }
        }
        public void CheckGameEnded()
        {
            if (white >= 12)
            {
                winLabel.Text = user.Name + " Wins";
                Win.Visible = true;
                game.GameDurationTime = hh + ":" + mm + ":" + ss;
            }
            else if (black >= 12)
            {
                winLabel.Text = "Computer Wins";
                Win.Visible = true;
                game.GameDurationTime = hh + ":" + mm + ":" + ss;
            }
        }
        public void CheckContinuesKills()
        {
            if (kill1 != "" || kill2 != "" || kill3 != "" || kill4 != "")
            {
                DisableAllNotGrays();
                P[Convert.ToInt32(me.Split(' ')[0]), Convert.ToInt32(me.Split(' ')[1])].Enabled = true;
            }
            else
            {
                RemoveGrays();
                ChangeTurn();
                EnableAll();

            }

        }
        public void UpdateKillsAndCheckContinuesKills(string kill, string path, PictureBox p)
        {
            if (kill.Split(' ')[2] == "w" || kill.Split(' ')[2] == "q")
                UpdateWhiteKills();
            else
                UpdateBlackKills();

            CheckGameEnded();
            EraseKills();

            if (!justBeKing)
            {
                justkill = true;
                if (p.Name.Split(' ')[2] == "k" || p.Name.Split(' ')[2] == "q")
                    GetKillsAndPathsForKings(P[Convert.ToInt32(path.Split(' ')[0]), Convert.ToInt32(path.Split(' ')[1])]);
                else
                    GetKillsAndPaths(P[Convert.ToInt32(path.Split(' ')[0]), Convert.ToInt32(path.Split(' ')[1])]);
                CheckContinuesKills();
            }
            else
            {
                RemoveGrays();
                ErasePaths();
                ChangeTurn();
                EnableAll();
            }

        }

        public async Task GetServerMove1And2()
        {
            whitePiecesInfo = new List<Info>();
            int count = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i % 2 == 0 && j % 2 == 1)
                        continue;
                    if (i % 2 == 1 && j % 2 == 0)
                        continue;

                    try
                    {
                        if (P[i, j].Name.Split(' ')[2] == "w")
                        {
                            GetKillsAndPaths(P[i, j]);
                            if (path1 != "" || path2 != "" || path3 != "" || path4 != "")
                            {
                                whitePiecesInfo.Add(new Info(new PictureBox(), new List<string>()));
                                whitePiecesInfo.ElementAt(count).availableWhitePiece = P[i, j];
                                CountPaths(count);
                                count++;
                            }
                            EraseKills();
                            ErasePaths();
                            RemoveGrays();
                        }
                    }
                    catch { }

                    try
                    {
                        if (P[i, j].Name.Split(' ')[2] == "q")
                        {
                            GetKillsAndPathsForKings(P[i, j]);
                            if (path1 != "" || path2 != "" || path3 != "" || path4 != "")
                            {
                                whitePiecesInfo.Add(new Info(new PictureBox(), new List<string>()));
                                whitePiecesInfo.ElementAt(count).availableWhitePiece = P[i, j];
                                CountPaths(count);
                                count++;
                            }
                            EraseKills();
                            ErasePaths();
                            RemoveGrays();
                        }
                    }
                    catch { }
                }
            }
            int v = whitePiecesInfo.Count;
            serverMove1 = await GetNumberAsync(v);//pick pic
            int s = whitePiecesInfo.ElementAt(serverMove1).pathsAvailable.Count;
            serverMove2 = await GetNumberAsync(s); //pick path
        }

        public void ServerTurn()
        {

            PictureBox p = whitePiecesInfo.ElementAt(serverMove1).availableWhitePiece;
            string path = whitePiecesInfo.ElementAt(serverMove1).pathsAvailable.ElementAt(serverMove2);

            int posi, posj, pathPosi, pathPosj;
            posi = Convert.ToInt32(p.Name.Split(' ')[0]);
            posj = Convert.ToInt32(p.Name.Split(' ')[1]);

            pathPosi = Convert.ToInt32(path.Split(' ')[0]);
            pathPosj = Convert.ToInt32(path.Split(' ')[1]);
            if (p.Name.Split(' ')[2] == "w")
            {//if the piece is not the king piece

                //just move the piece... the server decided not to kill
                if (pathPosi == posi + 1)
                {
                    P[posi, posj].Image = null;
                    P[posi, posj].Name = posi + " " + posj;
                    if (pathPosi == 7)
                    {
                        P[pathPosi, pathPosj].Image = Properties.Resources.kw;
                        P[pathPosi, pathPosj].Name = pathPosi + " " + pathPosj + " q";
                    }
                    else
                    {
                        P[pathPosi, pathPosj].Image = Properties.Resources.w;
                        P[pathPosi, pathPosj].Name = pathPosi + " " + pathPosj + " w";
                    }
                }
                //kill... the server decided to kill
                else
                {

                    while (pathPosi == posi + 2)//while there are more kills
                    {

                        // ------------------left kill--------------------
                        if (posj == pathPosj + 2)
                        {
                            //perform the left kill and check for more
                            P[posi, posj].Image = null;
                            P[posi, posj].Name = posi + " " + posj;

                            P[posi + 1, posj - 1].Name = (posi + 1) + " " + (posj - 1);
                            P[posi + 1, posj - 1].Image = null;
                            if (pathPosi == 7)
                            {
                                P[pathPosi, pathPosj].Image = Properties.Resources.kw;
                                P[pathPosi, pathPosj].Name = pathPosi + " " + pathPosj + " q";
                            }
                            else
                            {
                                P[pathPosi, pathPosj].Image = Properties.Resources.w;
                                P[pathPosi, pathPosj].Name = pathPosi + " " + pathPosj + " w";
                            }
                            UpdateBlackKills();

                            //check and perform the next kill if found
                            posi = pathPosi; posj = pathPosj;

                            try
                            {
                                if (P[posi + 1, posj - 1].Image != null || P[posi + 1, posj + 1].Image != null)
                                {

                                    if (P[posi + 1, posj - 1].Image != null)//kill left
                                    {
                                        try
                                        {
                                            if ((P[posi + 1, posj - 1].Name.Split(' ')[2] == "b" || P[posi + 1, posj - 1].Name.Split(' ')[2] == "k") && P[posi + 2, posj - 2].Image == null)
                                            {
                                                pathPosi = posi + 2;
                                                pathPosj = posj - 2;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (P[posi + 1, posj + 1].Image != null)//kill right
                                    {
                                        try
                                        {
                                            if ((P[posi + 1, posj + 1].Name.Split(' ')[2] == "b" || P[posi + 1, posj + 1].Name.Split(' ')[2] == "k") && P[posi + 2, posj + 2].Image == null)
                                            {
                                                pathPosi = posi + 2;
                                                pathPosj = posj + 2;
                                            }
                                        }
                                        catch { }
                                    }
                                }
                                else
                                    posi = pathPosi;
                            }
                            catch { posi = pathPosi; }
                        }
                        // ------------------left kill--------------------

                        // ------------------right kill--------------------
                        else if (posj == pathPosj - 2)
                        {
                            P[posi, posj].Image = null;
                            P[posi, posj].Name = posi + " " + posj;

                            P[posi + 1, posj + 1].Name = (posi + 1) + " " + (posj - 1);
                            P[posi + 1, posj + 1].Image = null;

                            if (pathPosi == 7)
                            {
                                P[pathPosi, pathPosj].Image = Properties.Resources.kw;
                                P[pathPosi, pathPosj].Name = pathPosi + " " + pathPosj + " q";
                            }
                            else
                            {
                                P[pathPosi, pathPosj].Image = Properties.Resources.w;
                                P[pathPosi, pathPosj].Name = pathPosi + " " + pathPosj + " w";
                            }
                            UpdateBlackKills();

                            //check and perform the next kill if found
                            posi = pathPosi; posj = pathPosj;

                            try
                            {
                                if (P[posi + 1, posj - 1].Image != null || P[posi + 1, posj + 1].Image != null)
                                {

                                    if (P[posi + 1, posj - 1].Image != null)//kill left
                                    {
                                        try
                                        {
                                            if ((P[posi + 1, posj - 1].Name.Split(' ')[2] == "b" || P[posi + 1, posj - 1].Name.Split(' ')[2] == "k") && P[posi + 2, posj - 2].Image == null)
                                            {
                                                pathPosi = posi + 2;
                                                pathPosj = posj - 2;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (P[posi + 1, posj + 1].Image != null)//kill left
                                    {
                                        try
                                        {
                                            if ((P[posi + 1, posj + 1].Name.Split(' ')[2] == "b" || P[posi + 1, posj + 1].Name.Split(' ')[2] == "k") && P[posi + 2, posj + 2].Image == null)
                                            {
                                                pathPosi = posi + 2;
                                                pathPosj = posj + 2;
                                            }
                                        }
                                        catch { }
                                    }
                                }
                                else
                                    posi = pathPosi;
                            }
                            catch { posi = pathPosi; }
                        }
                        // ------------------right kill--------------------
                    }
                }
            }

            else
            { // name = q , king white

                //just move the king... the server decided not to kill.
                if (pathPosi == posi + 1 || pathPosi == posi - 1)
                {
                    P[posi, posj].Image = null;
                    P[posi, posj].Name = posi + " " + posj;

                    P[pathPosi, pathPosj].Image = Properties.Resources.kw;
                    P[pathPosi, pathPosj].Name = pathPosi + " " + pathPosj + " q";

                }
                //kill... the server decided to kill
                else
                {

                    if (pathPosi == posi + 2)
                    {
                        while (pathPosi == posi + 2)//while there are more kills down 
                        {

                            // ------------------left kill--------------------
                            if (posj == pathPosj + 2)
                            {
                                //perform the left kill and check for more
                                P[posi, posj].Image = null;
                                P[posi, posj].Name = posi + " " + posj;

                                P[posi + 1, posj - 1].Name = (posi + 1) + " " + (posj - 1);
                                P[posi + 1, posj - 1].Image = null;

                                P[pathPosi, pathPosj].Image = Properties.Resources.kw;
                                P[pathPosi, pathPosj].Name = pathPosi + " " + pathPosj + " q";

                                UpdateBlackKills();

                                //check and perform the next kill if found
                                posi = pathPosi; posj = pathPosj;

                                try
                                {
                                    if (P[posi + 1, posj - 1].Image != null || P[posi + 1, posj + 1].Image != null)
                                    {

                                        if (P[posi + 1, posj - 1].Image != null)//kill left
                                        {
                                            try
                                            {
                                                if ((P[posi + 1, posj - 1].Name.Split(' ')[2] == "b" || P[posi + 1, posj - 1].Name.Split(' ')[2] == "k") && P[posi + 2, posj - 2].Image == null)
                                                {
                                                    pathPosi = posi + 2;
                                                    pathPosj = posj - 2;
                                                }
                                            }
                                            catch { }
                                        }
                                        else if (P[posi + 1, posj + 1].Image != null)//kill right
                                        {
                                            try
                                            {
                                                if ((P[posi + 1, posj + 1].Name.Split(' ')[2] == "b" || P[posi + 1, posj + 1].Name.Split(' ')[2] == "k") && P[posi + 2, posj + 2].Image == null)
                                                {
                                                    pathPosi = posi + 2;
                                                    pathPosj = posj + 2;
                                                }
                                            }
                                            catch { }
                                        }
                                    }
                                    else
                                        posi = pathPosi;
                                }
                                catch { posi = pathPosi; }
                            }
                            // ------------------left kill--------------------

                            // ------------------right kill--------------------
                            else if (posj == pathPosj - 2)
                            {
                                P[posi, posj].Image = null;
                                P[posi, posj].Name = posi + " " + posj;

                                P[posi + 1, posj + 1].Name = (posi + 1) + " " + (posj - 1);
                                P[posi + 1, posj + 1].Image = null;

                                P[pathPosi, pathPosj].Image = Properties.Resources.kw;
                                P[pathPosi, pathPosj].Name = pathPosi + " " + pathPosj + " q";

                                UpdateBlackKills();

                                //check and perform the next kill if found
                                posi = pathPosi; posj = pathPosj;

                                try
                                {
                                    if (P[posi + 1, posj - 1].Image != null || P[posi + 1, posj + 1].Image != null)
                                    {

                                        if (P[posi + 1, posj - 1].Image != null)//kill left
                                        {
                                            try
                                            {
                                                if ((P[posi + 1, posj - 1].Name.Split(' ')[2] == "b" || P[posi + 1, posj - 1].Name.Split(' ')[2] == "k") && P[posi + 2, posj - 2].Image == null)
                                                {
                                                    pathPosi = posi + 2;
                                                    pathPosj = posj - 2;
                                                }
                                            }
                                            catch { }
                                        }
                                        else if (P[posi + 1, posj + 1].Image != null)//kill left
                                        {
                                            try
                                            {
                                                if ((P[posi + 1, posj + 1].Name.Split(' ')[2] == "b" || P[posi + 1, posj + 1].Name.Split(' ')[2] == "k") && P[posi + 2, posj + 2].Image == null)
                                                {
                                                    pathPosi = posi + 2;
                                                    pathPosj = posj + 2;
                                                }
                                            }
                                            catch { }
                                        }
                                    }
                                    else
                                        posi = pathPosi;
                                }
                                catch { posi = pathPosi; }
                            }
                            // ------------------right kill--------------------
                        }
                    }
                    else
                    {
                        while (pathPosi == posi - 2)//while there are more kills up
                        {

                            // ------------------left kill--------------------
                            if (posj == pathPosj + 2)
                            {
                                //perform the left kill and check for more
                                P[posi, posj].Image = null;
                                P[posi, posj].Name = posi + " " + posj;

                                P[posi - 1, posj - 1].Name = (posi - 1) + " " + (posj - 1);
                                P[posi - 1, posj - 1].Image = null;

                                P[pathPosi, pathPosj].Image = Properties.Resources.kw;
                                P[pathPosi, pathPosj].Name = pathPosi + " " + pathPosj + " q";

                                UpdateBlackKills();

                                //check and perform the next kill if found
                                posi = pathPosi; posj = pathPosj;

                                try
                                {
                                    if (P[posi - 1, posj - 1].Image != null || P[posi - 1, posj + 1].Image != null)
                                    {

                                        if (P[posi - 1, posj - 1].Image != null)//kill left
                                        {
                                            try
                                            {
                                                if ((P[posi - 1, posj - 1].Name.Split(' ')[2] == "b" || P[posi - 1, posj - 1].Name.Split(' ')[2] == "k") && P[posi - 2, posj - 2].Image == null)
                                                {
                                                    pathPosi = posi - 2;
                                                    pathPosj = posj - 2;
                                                }
                                            }
                                            catch { }
                                        }
                                        else if (P[posi - 1, posj + 1].Image != null)//kill right
                                        {
                                            try
                                            {
                                                if ((P[posi - 1, posj + 1].Name.Split(' ')[2] == "b" || P[posi - 1, posj + 1].Name.Split(' ')[2] == "k") && P[posi - 2, posj + 2].Image == null)
                                                {
                                                    pathPosi = posi - 2;
                                                    pathPosj = posj + 2;
                                                }
                                            }
                                            catch { }
                                        }
                                    }
                                    else
                                        posi = pathPosi;
                                }
                                catch { posi = pathPosi; }
                            }
                            // ------------------left kill--------------------

                            // ------------------right kill--------------------
                            else if (posj == pathPosj - 2)
                            {
                                P[posi, posj].Image = null;
                                P[posi, posj].Name = posi + " " + posj;

                                P[posi - 1, posj + 1].Name = (posi - 1) + " " + (posj - 1);
                                P[posi - 1, posj + 1].Image = null;

                                P[pathPosi, pathPosj].Image = Properties.Resources.kw;
                                P[pathPosi, pathPosj].Name = pathPosi + " " + pathPosj + " q";

                                UpdateBlackKills();

                                //check and perform the next kill if found
                                posi = pathPosi; posj = pathPosj;

                                try
                                {
                                    if (P[posi - 1, posj - 1].Image != null || P[posi - 1, posj + 1].Image != null)
                                    {

                                        if (P[posi - 1, posj - 1].Image != null)//kill left
                                        {
                                            try
                                            {
                                                if ((P[posi - 1, posj - 1].Name.Split(' ')[2] == "b" || P[posi - 1, posj - 1].Name.Split(' ')[2] == "k") && P[posi - 2, posj - 2].Image == null)
                                                {
                                                    pathPosi = posi - 2;
                                                    pathPosj = posj - 2;
                                                }
                                            }
                                            catch { }
                                        }
                                        else if (P[posi - 1, posj + 1].Image != null)//kill left
                                        {
                                            try
                                            {
                                                if ((P[posi - 1, posj + 1].Name.Split(' ')[2] == "b" || P[posi - 1, posj + 1].Name.Split(' ')[2] == "k") && P[posi - 2, posj + 2].Image == null)
                                                {
                                                    pathPosi = posi - 2;
                                                    pathPosj = posj + 2;
                                                }
                                            }
                                            catch { }
                                        }
                                    }
                                    else
                                        posi = pathPosi;
                                }
                                catch { posi = pathPosi; }
                            }
                            // ------------------right kill--------------------
                        }
                    }
                }
            }
        }
        //timer eventhandler
        private void t_Tick(object sender, EventArgs e)
        {
            if (ss == 59)
            {
                ss = 0;
                if (mm == 59)
                {
                    mm = 0;
                    hh++;
                }
                mm++;
            }
            ss++;


            //get current time
            //int hh = DateTime.Now.Hour;
            //int mm = DateTime.Now.Minute;
            //int ss = DateTime.Now.Second;

            //time
            string time = "";

            //padding leading zero
            if (hh < 10)
            {
                time += "0" + hh;
            }
            else
            {
                time += hh;
            }
            time += ":";

            if (mm < 10)
            {
                time += "0" + mm;
            }
            else
            {
                time += mm;
            }
            time += ":";

            if (ss < 10)
            {
                time += "0" + ss;
            }
            else
            {
                time += ss;
            }

            //update label
            TimerLabel.Text = time;
        }


        //apis
        async Task<IEnumerable<User>> GetUserAsync(string path)
        {
            IEnumerable<User> user = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadAsAsync<IEnumerable<User>>();
            }
            return user;
        }

        async Task<IEnumerable<Game>> GetGamesAsync(string path)
        {
            IEnumerable<Game> games = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                games = await response.Content.ReadAsAsync<IEnumerable<Game>>();
            }
            return games;
        }

        async Task<IEnumerable<Game>> GetAllGamesAsync(string path)
        {
            IEnumerable<Game> games = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                games = await response.Content.ReadAsAsync<IEnumerable<Game>>();
            }
            return games;
        }

        async Task<int> GetNumberAsync(int range)
        {
            HttpResponseMessage response = await client.GetAsync("api/Users/random/" + range);
            response.EnsureSuccessStatusCode();
            int k = await response.Content.ReadAsAsync<int>();
            // return URI of the created resource.
            return k;
        }


        async Task<Game> CreateGameAsync()
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/Games", game);
            response.EnsureSuccessStatusCode();
            Game  k = await response.Content.ReadAsAsync<Game>();

            // return URI of the created resource.
            return k;
        }
        private async void FormIsClosing(object sender, FormClosingEventArgs e)
        {
            game.GameDurationTime = hh + ":" + mm + ":" + ss;
            game = await CreateGameAsync();

 


        }
    }

}

