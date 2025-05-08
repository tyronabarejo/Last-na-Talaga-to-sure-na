using System.Drawing;
using System.Windows.Forms;

namespace DinoGame
{
    public class Dino
    {
        public PictureBox DinoBox { get; private set; }
        public int Ammo { get; private set; }
        public bool IsJumping { get; set; }
        public int JumpHeight { get; set; }
        private readonly int jumpForce = 15;
        private readonly int gravity = 5;

        public Dino(Form form)
        {
            DinoBox = new PictureBox
            {
                Width = 50,
                Height = 50,
                BackColor = Color.Green,
                Location = new Point(50, form.Height - 100)
            };
            Ammo = 3;
            IsJumping = false;
            JumpHeight = 0;
            form.Controls.Add(DinoBox);
        }

        public void Jump()
        {
            if (!IsJumping && DinoBox.Top >= DinoBox.Parent.Height - 100)
            {
                IsJumping = true;
                JumpHeight = 0;
            }
        }

        public void UpdateJump()
        {
            if (IsJumping)
            {
                if (JumpHeight < 100)
                {
                    DinoBox.Top -= jumpForce;
                    JumpHeight += jumpForce;
                }
                else
                {
                    IsJumping = false;
                }
            }
            else if (DinoBox.Top < DinoBox.Parent.Height - 100)
            {
                DinoBox.Top += gravity;
            }
        }

        public void AddAmmo()
        {
            Ammo++;
        }

        public bool UseAmmo()
        {
            if (Ammo > 0)
            {
                Ammo--;
                return true;
            }
            return false;
        }
    }
} 