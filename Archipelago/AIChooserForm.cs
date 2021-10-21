using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Archipelago
{
    public partial class AIChooserForm : Form
    {
        MainGameForm.MapType mapType = MainGameForm.MapType.Continents;
        public AIChooserForm()
        {
            InitializeComponent();
            //      Brush b; //Define the brush
            //    switch (t) //Switch on colours to change the colour of the port
            //    {
            //        case Team.None:
            //            throw new Exception("Team is not allowed to be none");
            //        case Team.Red:
            //            b = Brushes.Red;
            //            break;
            //        case Team.Green:
            //            b = Brushes.Green;
            //            break;
            //        case Team.Black:
            //            b = Brushes.Black;
            //            break;
            //        case Team.Blue:
            //            b = Brushes.Blue;
            //            break;
            //        case Team.Pirate:
            //            b = Brushes.Brown;
            //            break;
            //        default:
            //            throw new Exception();
            //    }
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;

            pictureBox1.Image = Properties.Resources.Archipelago;
        }

        private void AIChooserForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.Bilinear;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            e.Graphics.FillEllipse(Brushes.Red  , new Rectangle(10, 40, 30, 30));
            e.Graphics.FillEllipse(Brushes.Green, new Rectangle(10, 80, 30, 30));
            e.Graphics.FillEllipse(Brushes.Black, new Rectangle(10, 120, 30, 30));
            e.Graphics.FillEllipse(Brushes.Blue , new Rectangle(10, 160, 30, 30));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Team playerTeams = Team.None;
            Team AITeams = Team.None;
            switch (comboBox1.SelectedItem)
            {
                case "Computer":
                    AITeams |= Team.Red;
                    break;
                case "Human":
                    playerTeams |= Team.Red;
                    break;
            }

            switch (comboBox2.SelectedItem)
            {
                case "Computer":
                    AITeams |= Team.Green;
                    break;
                case "Human":
                    playerTeams |= Team.Green;
                    break;
            }

            switch (comboBox3.SelectedItem)
            {
                case "Computer":
                    AITeams |= Team.Black;
                    break;
                case "Human":
                    playerTeams |= Team.Black;
                    break;
            }

            switch (comboBox4.SelectedItem)
            {
                case "Computer":
                    AITeams |= Team.Blue;
                    break;
                case "Human":
                    playerTeams |= Team.Blue;
                    break;
            }
            MainGameForm m = new MainGameForm(playerTeams, AITeams, mapType);
            m.ShowDialog();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (mapType == MainGameForm.MapType.Continents)
            {
                pictureBox1.Image = Properties.Resources.Archipelago2;
                mapType = MainGameForm.MapType.SmallIslands;
            }
            else
            {
                pictureBox1.Image = Properties.Resources.Archipelago;
                mapType = MainGameForm.MapType.Continents;
            }
            this.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            StartForm m = new StartForm();
            m.ShowDialog();
        }
    }
}
