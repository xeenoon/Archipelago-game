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
                e.Graphics.DrawString("Campaign", myFont, Brushes.Black, new Point(80, 5));
            }
        }

        private void LoadSave_Paint(object sender, PaintEventArgs e)
        {
            using (Font myFont = new Font("Modern No. 20", 30))
            {
                e.Graphics.DrawString("Load save", myFont, Brushes.Black, new Point(82, 5));
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

        private void OpenFileExplorer(object sender, EventArgs e)
        {
            OpenFileDialog saveFileDialog1 = new OpenFileDialog
            {
                Filter = "agame files (*.agame)|*.agame", //Only let users choose file of type '.agame'
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (saveFileDialog1.ShowDialog() == DialogResult.OK) //Open file explorer to allow user to choose gamesave fle
            {
                Hide();
                MainGameForm m = new MainGameForm(saveFileDialog1.FileName);
                m.ShowDialog();
            }
        }

        private void StartForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            MainGameForm m = new MainGameForm(@"C:\Program Files\Archipelago\1.agame");
            m.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            MainGameForm m = new MainGameForm(@"C:\Program Files\Archipelago\2.agame");
            m.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            MainGameForm m = new MainGameForm(@"C:\Program Files\Archipelago\3.agame");
            m.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hide();
            MainGameForm m = new MainGameForm(@"C:\Program Files\Archipelago\4.agame");
            m.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Hide();
            MainGameForm m = new MainGameForm(@"C:\Program Files\Archipelago\5.agame");
            m.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Hide();
            MainGameForm m = new MainGameForm(@"C:\Program Files\Archipelago\6.agame");
            m.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Hide();
            MainGameForm m = new MainGameForm(@"C:\Program Files\Archipelago\7.agame");
            m.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Hide();
            MainGameForm m = new MainGameForm(@"C:\Program Files\Archipelago\8.agame");
            m.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Hide();
            MainGameForm m = new MainGameForm(@"C:\Program Files\Archipelago\9.agame");
            m.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Hide();
            MainGameForm m = new MainGameForm(@"C:\Program Files\Archipelago\10.agame");
            m.ShowDialog();
        }
    }
}
