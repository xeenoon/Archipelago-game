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

        private void roundPictureBox1_Paint(object sender, PaintEventArgs e)
        {
            using (Font myFont = new Font("Old English Text MT", 30))
            {
                e.Graphics.DrawString("Start", myFont, Brushes.Black, new Point(70, 0));
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            using (Font myFont = new Font("Old English Text MT", 70))
            {
                e.Graphics.DrawString("Archipelago", myFont, Brushes.Black, new Point(170, 0));
            }
        }
    }
}
