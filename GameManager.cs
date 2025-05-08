using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace DinoGame
{
    public class GameManager
    {
        private readonly Form gameForm;
        private readonly Dino dino;
        private readonly List<Obstacle> obstacles;
        private readonly List<Bullet> bullets;
        private readonly List<AmmoPickup> ammoPickups;
        private readonly System.Windows.Forms.Timer gameTimer;
        private readonly Random random;
        private readonly Label scoreLabel;
        private readonly Label ammoLabel;
        private SoundPlayer backgroundMusic;
        private SoundPlayer shootSound;

        private int score;
        private int gameSpeed;

        public GameManager(Form form)
        {
            gameForm = form;

            backgroundMusic = new SoundPlayer("Resources/background.wav");
            shootSound = new SoundPlayer("Resources/shoot.wav");

            try
            {
                backgroundMusic.PlayLooping(); // tumutugtog habang naglalaro
            }
            catch (Exception ex)
            {
                MessageBox.Show("Background music failed: " + ex.Message);
            }
            dino = new Dino(form);
            obstacles = new List<Obstacle>();
            bullets = new List<Bullet>();
            ammoPickups = new List<AmmoPickup>();
            random = new Random();
            score = 0;
            gameSpeed = 10;

            // Setup labels
            scoreLabel = new Label
            {
                Text = "Score: 0",
                Location = new Point(10, 10),
                AutoSize = true
            };
            form.Controls.Add(scoreLabel);

            ammoLabel = new Label
            {
                Text = "Ammo: 3",
                Location = new Point(10, 30),
                AutoSize = true
            };
            form.Controls.Add(ammoLabel);

            // Setup timer
            gameTimer = new System.Windows.Forms.Timer
            {
                Interval = 20
            };
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            UpdateGame();
            CheckCollisions();
            SpawnRandomObjects();
            UpdateLabels();
        }

        private void UpdateGame()
        {
            dino.UpdateJump();

            // Update obstacles
            for (int i = obstacles.Count - 1; i >= 0; i--)
            {
                obstacles[i].Move(gameSpeed);
                if (obstacles[i].IsOutOfBounds())
                {
                    obstacles[i].Destroy();
                    obstacles.RemoveAt(i);
                    score++;
                }
            }

            // Update bullets
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                bullets[i].Move();
                if (bullets[i].IsOutOfBounds())
                {
                    bullets[i].Destroy();
                    bullets.RemoveAt(i);
                }
            }

            // Update ammo pickups
            for (int i = ammoPickups.Count - 1; i >= 0; i--)
            {
                ammoPickups[i].Move(gameSpeed);
                if (ammoPickups[i].IsOutOfBounds())
                {
                    ammoPickups[i].Collect();
                    ammoPickups.RemoveAt(i);
                }
            }

            // Increase game speed
            if (score % 10 == 0 && score > 0)
            {
                gameSpeed++;
            }
        }

        private void CheckCollisions()
        {
            // Check bullet collisions with obstacles
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                for (int j = obstacles.Count - 1; j >= 0; j--)
                {
                    if (bullets[i].BulletBox.Bounds.IntersectsWith(obstacles[j].ObstacleBox.Bounds))
                    {
                        bullets[i].Destroy();
                        obstacles[j].Destroy();
                        bullets.RemoveAt(i);
                        obstacles.RemoveAt(j);
                        score += 2;
                        break;
                    }
                }
            }

            // Check dino collisions with obstacles
            foreach (Obstacle obstacle in obstacles)
            {
                if (dino.DinoBox.Bounds.IntersectsWith(obstacle.ObstacleBox.Bounds))
                {
                    GameOver();
                    return;
                }
            }

            // Check dino collisions with ammo pickups
            for (int i = ammoPickups.Count - 1; i >= 0; i--)
            {
                if (dino.DinoBox.Bounds.IntersectsWith(ammoPickups[i].PickupBox.Bounds))
                {
                    ammoPickups[i].Collect();
                    ammoPickups.RemoveAt(i);
                    dino.AddAmmo();
                }
            }
        }

        private void SpawnRandomObjects()
        {
            // Spawn obstacles
            if (random.Next(100) < 2)
            {
                obstacles.Add(new Obstacle(gameForm));
            }

            // Spawn ammo pickups
            if (random.Next(200) < 1)
            {
                ammoPickups.Add(new AmmoPickup(gameForm));
            }
        }

        private void UpdateLabels()
        {
            scoreLabel.Text = $"Score: {score}";
            ammoLabel.Text = $"Ammo: {dino.Ammo}";
        }

        public void HandleKeyPress(Keys keyCode)
        {
            switch (keyCode)
            {
                case Keys.Space:
                    dino.Jump();
                    break;
                case Keys.X:
                    if (dino.UseAmmo())
                    {
                        bullets.Add(new Bullet(gameForm, new Point(dino.DinoBox.Right, dino.DinoBox.Top + 25)));
                        shootSound.Play(); // ← Gun sound
                    }
                    break;
            }
        }

        private void GameOver()
        {
            gameTimer.Stop();
            MessageBox.Show($"Game Over! Score: {score}", "Game Over");
            backgroundMusic.Stop();
            Application.Exit();
        }
    }
} 