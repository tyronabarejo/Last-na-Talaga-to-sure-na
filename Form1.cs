using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Media;


namespace DinoGame
{
    public partial class Form1 : Form
    {
        private GameManager gameManager;

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            // Form settings
            this.Width = 800;
            this.Height = 400;
            this.BackColor = Color.White;
            this.DoubleBuffered = true;

            // Initialize game manager
            gameManager = new GameManager(this);

            // Add KeyDown event
            this.KeyDown += Form1_KeyDown;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            gameManager.HandleKeyPress(e.KeyCode);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // You can add any initialization code here if needed
        }
    }
} 