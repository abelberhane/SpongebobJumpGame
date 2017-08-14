using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpongebobJumpgame
{
    public partial class Form1 : Form
    {
        //Global Variables
        bool goLeft = false;
        bool goRight = false;
        bool jumping = false;

        int jumpSpeed = 10;
        int force = 8;
        int score = 0;

        //Setting up the SoundPlayer
        System.Media.SoundPlayer musicPlayer = new System.Media.SoundPlayer();
        
        public Form1()
        {
            InitializeComponent();

            //Play the Music
            musicPlayer.Stream = Properties.Resources.Theme;
            musicPlayer.Play();
        }

        //Controls for allowing Spongebob to go left, right, and jump.
        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;  
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Space && !jumping)
            {
                jumping = true;
            }
        }

        //This will make the settings go back to normal once the player lets go of the Jump button
        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (jumping)
            {
                jumping = false;
            }
        }

        //Simulate Gravity for the player. Determining Jump status, direction and force in that direction of the screen.
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Score
            lblScore.Text = "Score: " + score;

            player.Top += jumpSpeed;
            if (jumping && force < 0)
            {
                jumping = false;
            }
            if (goLeft)
            {
                player.Left -= 5;
            }
            if (goRight)
            {
                player.Left += 5;
            }
            if (jumping)
            {
                jumpSpeed = -12;
                force -= 1;
            }
            else
            {
                jumpSpeed = 12;
            }

            //Controls for Object Collision
            foreach (Control x in this.Controls)
            {
                //Logic for player landing on the platform
                if (x is PictureBox && x.Tag == "platform")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds) && !jumping)
                    {
                        force = 8;
                        player.Top = x.Top - player.Height;
                    }
                }

                //Logic for picking up the coins and scoring
                if (x is PictureBox && x.Tag == "coin")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds) && !jumping)
                    {
                        this.Controls.Remove(x);
                        score++;
                    }
                }
            }
                //Winning the game logic. If the player touches the door, they win.
                if (player.Bounds.IntersectsWith(door.Bounds))
            {
                timer1.Stop();
                MessageBox.Show("You WIN!");
            }
        }
    }
  }
