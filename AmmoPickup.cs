using System.Drawing;
using System.Windows.Forms;

namespace DinoGame
{
    public class AmmoPickup
    {
        public PictureBox PickupBox { get; private set; }
        public bool IsCollected { get; set; }

        public AmmoPickup(Form form)
        {
            PickupBox = new PictureBox
            {
                Width = 20,
                Height = 20,
                BackColor = Color.Yellow,
                Location = new Point(form.Width, form.Height - 100)
            };
            IsCollected = false;
            form.Controls.Add(PickupBox);
        }

        public void Move(int speed)
        {
            PickupBox.Left -= speed;
        }

        public bool IsOutOfBounds()
        {
            return PickupBox.Right < 0;
        }

        public void Collect()
        {
            if (PickupBox.Parent != null)
            {
                PickupBox.Parent.Controls.Remove(PickupBox);
            }
            IsCollected = true;
        }
    }
} 