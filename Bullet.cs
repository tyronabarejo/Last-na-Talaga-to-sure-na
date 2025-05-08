using System.Drawing;
using System.Windows.Forms;

namespace DinoGame
{
    public class Bullet
    {
        public PictureBox BulletBox { get; private set; }
        public bool IsDestroyed { get; set; }
        private readonly int speed = 15;

        public Bullet(Form form, Point startPosition)
        {
            BulletBox = new PictureBox
            {
                Width = 10,
                Height = 5,
                BackColor = Color.Blue,
                Location = startPosition
            };
            IsDestroyed = false;
            form.Controls.Add(BulletBox);
        }

        public void Move()
        {
            BulletBox.Left += speed;
        }

        public bool IsOutOfBounds()
        {
            return BulletBox.Left > BulletBox.Parent.Width;
        }

        public void Destroy()
        {
            if (BulletBox.Parent != null)
            {
                BulletBox.Parent.Controls.Remove(BulletBox);
            }
            IsDestroyed = true;
        }
    }
} 