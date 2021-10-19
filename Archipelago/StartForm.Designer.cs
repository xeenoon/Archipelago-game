
namespace Archipelago
{
    partial class StartForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.roundPictureBox3 = new Archipelago.RoundPictureBox();
            this.roundPictureBox2 = new Archipelago.RoundPictureBox();
            this.roundPictureBox1 = new Archipelago.RoundPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.roundPictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.roundPictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.roundPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-8, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(818, 457);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.TitlePaint);
            // 
            // roundPictureBox3
            // 
            this.roundPictureBox3.BackColor = System.Drawing.Color.DarkGray;
            this.roundPictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("roundPictureBox3.Image")));
            this.roundPictureBox3.Location = new System.Drawing.Point(242, 272);
            this.roundPictureBox3.Name = "roundPictureBox3";
            this.roundPictureBox3.Size = new System.Drawing.Size(334, 50);
            this.roundPictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.roundPictureBox3.TabIndex = 4;
            this.roundPictureBox3.TabStop = false;
            this.roundPictureBox3.Paint += new System.Windows.Forms.PaintEventHandler(this.Training_Paint);
            // 
            // roundPictureBox2
            // 
            this.roundPictureBox2.BackColor = System.Drawing.Color.DarkGray;
            this.roundPictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("roundPictureBox2.Image")));
            this.roundPictureBox2.Location = new System.Drawing.Point(242, 202);
            this.roundPictureBox2.Name = "roundPictureBox2";
            this.roundPictureBox2.Size = new System.Drawing.Size(334, 50);
            this.roundPictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.roundPictureBox2.TabIndex = 3;
            this.roundPictureBox2.TabStop = false;
            this.roundPictureBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.Campaign_Paint);
            // 
            // roundPictureBox1
            // 
            this.roundPictureBox1.BackColor = System.Drawing.Color.DarkGray;
            this.roundPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("roundPictureBox1.Image")));
            this.roundPictureBox1.Location = new System.Drawing.Point(242, 132);
            this.roundPictureBox1.Name = "roundPictureBox1";
            this.roundPictureBox1.Size = new System.Drawing.Size(334, 50);
            this.roundPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.roundPictureBox1.TabIndex = 2;
            this.roundPictureBox1.TabStop = false;
            this.roundPictureBox1.Click += new System.EventHandler(this.StartFFAMatch);
            this.roundPictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.FFA_Paint);
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.roundPictureBox3);
            this.Controls.Add(this.roundPictureBox2);
            this.Controls.Add(this.roundPictureBox1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "StartForm";
            this.Text = "Archipelago";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.roundPictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.roundPictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.roundPictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private RoundPictureBox roundPictureBox1;
        private RoundPictureBox roundPictureBox2;
        private RoundPictureBox roundPictureBox3;
    }
}

