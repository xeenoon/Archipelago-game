﻿using System;
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
            button1.Visible  = false;
            button2.Visible  = false;
            button3.Visible  = false;
            button4.Visible  = false;
            button5.Visible  = false;
            button6.Visible  = false;
            button7.Visible  = false;
            button8.Visible  = false;
            button9.Visible  = false;
            button10.Visible = false;
        }
        private void TitlePaint(object sender, PaintEventArgs e)
        {
            using (Font myFont = new Font("Old English Text MT", 50))
            {
                e.Graphics.DrawString("Battle For The Archipelago!", myFont, Brushes.Black, new Point(5, 15));
            }
            if (button1.Visible == true) //Are we showing the campaign mode
            {
                using (Font myFont = new Font("Old English Text MT", 40))
                {
                    e.Graphics.DrawString("Campaign mode", myFont, Brushes.BlueViolet, new Point(250, 80));
                }
            }
        }
        private void FFA_Paint(object sender, PaintEventArgs e)
        {
            using (Font myFont = new Font("Modern No. 20", 30))
            {
                e.Graphics.DrawString("Start Match", myFont, Brushes.Black, new Point(65, 5));
            }
        }
        private void Campaign_Paint(object sender, PaintEventArgs e)
        {
            using (Font myFont = new Font("Modern No. 20", 30))
            {
                e.Graphics.DrawString("Campaign", myFont, Brushes.Black, new Point(80, 0));
            }
        }

        private void Training_Paint(object sender, PaintEventArgs e)
        {
            using (Font myFont = new Font("Modern No. 20", 30))
            {
                e.Graphics.DrawString("Training", myFont, Brushes.Black, new Point(90, 0));
            }
        }

        private void StartFFAMatch(object sender, EventArgs e)
        {
            this.Hide();
            AIChooserForm aIChooserForm = new AIChooserForm();
            aIChooserForm.ShowDialog();
        }

        private void roundPictureBox2_Click(object sender, EventArgs e)
        {
            roundPictureBox1.Visible = false;
            roundPictureBox2.Visible = false;
            roundPictureBox3.Visible = false;

            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            button5.Visible = true;
            button6.Visible = true;
            button7.Visible = true;
            button8.Visible = true;
            button9.Visible = true;
            button10.Visible = true;
        }
    }
}
