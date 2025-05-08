using System.Drawing;
using System.Windows.Forms;

namespace DinoGame
{
    public class Obstacle
    {
        public PictureBox ObstacleBox { get; private set; }
        public bool IsDestroyed { get; set; }

        public Obstacle(Form form)
        {
            ObstacleBox = new PictureBox
            {
                Width = 30,
                Height = 50,
                BackColor = Color.Red,
                Location = new Point(form.Width, form.Height - 100)
            };
            IsDestroyed = false;
            form.Controls.Add(ObstacleBox);
        }

        public void Move(int speed)
        {
            ObstacleBox.Left -= speed;
        }

        public bool IsOutOfBounds()
        {
            return ObstacleBox.Right < 0;
        }

        public void Destroy()
        {
            if (ObstacleBox.Parent != null)
            {
                ObstacleBox.Parent.Controls.Remove(ObstacleBox);
            }
            IsDestroyed = true;
        }
    }
} 