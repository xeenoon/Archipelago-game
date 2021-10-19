using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Archipelago
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }
        private void TitlePaint(object sender, PaintEventArgs e)
        {
            using (Font myFont = new Font("Old English Text MT", 50))
            {
                e.Graphics.DrawString("Battle for the archipelago!", myFont, Brushes.Black, new Point(50, 0));
            }
        }
        private void FFA_Paint(object sender, PaintEventArgs e)
        {
            using (Font myFont = new Font("Modern .no 20", 30))
            {
                e.Graphics.DrawString("Start FFA match", myFont, Brushes.Black, new Point(15, 0));
            }
        }
        private void Campaign_Paint(object sender, PaintEventArgs e)
        {
            using (Font myFont = new Font("Modern .no 20", 30))
            {
                e.Graphics.DrawString("Campaign", myFont, Brushes.Black, new Point(70, 0));
            }
        }

        private void Training_Paint(object sender, PaintEventArgs e)
        {
            using (Font myFont = new Font("Modern .no 20", 30))
            {
                e.Graphics.DrawString("Training", myFont, Brushes.Black, new Point(90, 0));
            }
        }

        private void StartFFAMatch(object sender, EventArgs e)
        {
            roundPictureBox1.Visible = false;
            roundPictureBox2.Visible = false;
            roundPictureBox3.Visible = false;


        }
    }
}
