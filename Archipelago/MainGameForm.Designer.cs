
namespace Archipelago
{
    partial class MainGameForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainGameForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ShipsList = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ManufactureLabel = new System.Windows.Forms.Label();
            this.ManufactureHeavy = new System.Windows.Forms.ComboBox();
            this.ManufactureMedium = new System.Windows.Forms.ComboBox();
            this.ManufactureFast = new System.Windows.Forms.ComboBox();
            this.ManufactureVeryFast = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button6 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.TeamLabel = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.WoodResourceLabel = new System.Windows.Forms.Label();
            this.MetalResourceLabel = new System.Windows.Forms.Label();
            this.ClothResourceLabel = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button8 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(172, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(762, 528);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(950, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Square data";
            // 
            // ShipsList
            // 
            this.ShipsList.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ShipsList.AutoSize = true;
            this.ShipsList.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShipsList.Location = new System.Drawing.Point(937, 62);
            this.ShipsList.Name = "ShipsList";
            this.ShipsList.Size = new System.Drawing.Size(48, 18);
            this.ShipsList.TabIndex = 2;
            this.ShipsList.Text = "Ships:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(937, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 18);
            this.label3.TabIndex = 3;
            this.label3.Text = "Cargo:";
            // 
            // ManufactureLabel
            // 
            this.ManufactureLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ManufactureLabel.AutoSize = true;
            this.ManufactureLabel.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ManufactureLabel.Location = new System.Drawing.Point(937, 261);
            this.ManufactureLabel.Name = "ManufactureLabel";
            this.ManufactureLabel.Size = new System.Drawing.Size(94, 18);
            this.ManufactureLabel.TabIndex = 4;
            this.ManufactureLabel.Text = "Manufacture:";
            // 
            // ManufactureHeavy
            // 
            this.ManufactureHeavy.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ManufactureHeavy.FormattingEnabled = true;
            this.ManufactureHeavy.Items.AddRange(new object[] {
            "Brig",
            "Rigger",
            "Carrack",
            "Galleon",
            "4th rate",
            "3rd rate",
            "2nd rate",
            "1st rate"});
            this.ManufactureHeavy.Location = new System.Drawing.Point(1020, 293);
            this.ManufactureHeavy.Name = "ManufactureHeavy";
            this.ManufactureHeavy.Size = new System.Drawing.Size(19, 21);
            this.ManufactureHeavy.TabIndex = 9;
            this.ManufactureHeavy.SelectedIndexChanged += new System.EventHandler(this.BuildShip);
            // 
            // ManufactureMedium
            // 
            this.ManufactureMedium.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ManufactureMedium.FormattingEnabled = true;
            this.ManufactureMedium.Items.AddRange(new object[] {
            "Sloop",
            "Schooner",
            "Cutter",
            "Ketch",
            "Pinnance",
            "Sloop of war",
            "Snow",
            "War Galleon"});
            this.ManufactureMedium.Location = new System.Drawing.Point(1020, 324);
            this.ManufactureMedium.Name = "ManufactureMedium";
            this.ManufactureMedium.Size = new System.Drawing.Size(19, 21);
            this.ManufactureMedium.TabIndex = 10;
            this.ManufactureMedium.SelectedIndexChanged += new System.EventHandler(this.BuildShip);
            // 
            // ManufactureFast
            // 
            this.ManufactureFast.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ManufactureFast.FormattingEnabled = true;
            this.ManufactureFast.Items.AddRange(new object[] {
            "Brigantine",
            "Frigate",
            "Galley",
            "Corvette",
            "Xebec",
            "Man O War"});
            this.ManufactureFast.Location = new System.Drawing.Point(1020, 355);
            this.ManufactureFast.Name = "ManufactureFast";
            this.ManufactureFast.Size = new System.Drawing.Size(19, 21);
            this.ManufactureFast.TabIndex = 11;
            this.ManufactureFast.SelectedIndexChanged += new System.EventHandler(this.BuildShip);
            // 
            // ManufactureVeryFast
            // 
            this.ManufactureVeryFast.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ManufactureVeryFast.FormattingEnabled = true;
            this.ManufactureVeryFast.Items.AddRange(new object[] {
            "Steam Corvette",
            "Clipper"});
            this.ManufactureVeryFast.Location = new System.Drawing.Point(1020, 386);
            this.ManufactureVeryFast.Name = "ManufactureVeryFast";
            this.ManufactureVeryFast.Size = new System.Drawing.Size(19, 21);
            this.ManufactureVeryFast.TabIndex = 12;
            this.ManufactureVeryFast.SelectedIndexChanged += new System.EventHandler(this.BuildShip);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(961, 296);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 18);
            this.label4.TabIndex = 13;
            this.label4.Text = "Heavy";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(948, 327);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 18);
            this.label5.TabIndex = 14;
            this.label5.Text = "Medium";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(973, 358);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 18);
            this.label6.TabIndex = 15;
            this.label6.Text = "Fast";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(942, 389);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 18);
            this.label7.TabIndex = 16;
            this.label7.Text = "Very fast";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button1.AutoEllipsis = true;
            this.button1.BackColor = System.Drawing.Color.Goldenrod;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Location = new System.Drawing.Point(940, 230);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "Move";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(244, 56);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(69, 31);
            this.button3.TabIndex = 22;
            this.button3.Text = "Cancel";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.Cancel);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(65, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 17);
            this.label2.TabIndex = 21;
            this.label2.Text = "Do you want these ships to:";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(141, 56);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 31);
            this.button2.TabIndex = 20;
            this.button2.Text = "Move closest";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.MoveClosest);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(16, 56);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(119, 30);
            this.button4.TabIndex = 19;
            this.button4.Text = "Stay in square";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.StayInSquare);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(13, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(288, 17);
            this.label8.TabIndex = 18;
            this.label8.Text = "Some ships cannot move the specified distance.";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Location = new System.Drawing.Point(410, 216);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(328, 98);
            this.panel1.TabIndex = 23;
            // 
            // button5
            // 
            this.button5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button5.AutoEllipsis = true;
            this.button5.BackColor = System.Drawing.Color.Goldenrod;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button5.Location = new System.Drawing.Point(1021, 230);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(86, 23);
            this.button5.TabIndex = 24;
            this.button5.Text = "Move specific";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Steam corvette",
            "Steam corvette",
            "Steam corvette",
            "Steam corvette",
            "Steam corvette"});
            this.checkedListBox1.Location = new System.Drawing.Point(4, 28);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(100, 77);
            this.checkedListBox1.TabIndex = 25;
            this.checkedListBox1.ThreeDCheckBoxes = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button6);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.checkedListBox1);
            this.panel2.Location = new System.Drawing.Point(520, 196);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(107, 138);
            this.panel2.TabIndex = 26;
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(32, 108);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(41, 27);
            this.button6.TabIndex = 27;
            this.button6.Text = "Ok";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(16, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 17);
            this.label9.TabIndex = 26;
            this.label9.Text = "Select ships";
            // 
            // button7
            // 
            this.button7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button7.AutoEllipsis = true;
            this.button7.BackColor = System.Drawing.Color.LimeGreen;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button7.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button7.Location = new System.Drawing.Point(976, 481);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(99, 33);
            this.button7.TabIndex = 27;
            this.button7.Text = "End turn";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(937, 39);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 18);
            this.label10.TabIndex = 28;
            this.label10.Text = "Team:";
            // 
            // TeamLabel
            // 
            this.TeamLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.TeamLabel.AutoSize = true;
            this.TeamLabel.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeamLabel.Location = new System.Drawing.Point(978, 39);
            this.TeamLabel.Name = "TeamLabel";
            this.TeamLabel.Size = new System.Drawing.Size(0, 18);
            this.TeamLabel.TabIndex = 29;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Modern No. 20", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(12, 7);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(108, 25);
            this.label11.TabIndex = 30;
            this.label11.Text = "Resources";
            // 
            // WoodResourceLabel
            // 
            this.WoodResourceLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.WoodResourceLabel.AutoSize = true;
            this.WoodResourceLabel.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WoodResourceLabel.Location = new System.Drawing.Point(12, 48);
            this.WoodResourceLabel.Name = "WoodResourceLabel";
            this.WoodResourceLabel.Size = new System.Drawing.Size(54, 18);
            this.WoodResourceLabel.TabIndex = 31;
            this.WoodResourceLabel.Text = "Wood: ";
            // 
            // MetalResourceLabel
            // 
            this.MetalResourceLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.MetalResourceLabel.AutoSize = true;
            this.MetalResourceLabel.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MetalResourceLabel.Location = new System.Drawing.Point(12, 76);
            this.MetalResourceLabel.Name = "MetalResourceLabel";
            this.MetalResourceLabel.Size = new System.Drawing.Size(49, 18);
            this.MetalResourceLabel.TabIndex = 32;
            this.MetalResourceLabel.Text = "Metal:";
            // 
            // ClothResourceLabel
            // 
            this.ClothResourceLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ClothResourceLabel.AutoSize = true;
            this.ClothResourceLabel.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClothResourceLabel.Location = new System.Drawing.Point(12, 104);
            this.ClothResourceLabel.Name = "ClothResourceLabel";
            this.ClothResourceLabel.Size = new System.Drawing.Size(47, 18);
            this.ClothResourceLabel.TabIndex = 33;
            this.ClothResourceLabel.Text = "Cloth:";
            // 
            // listBox1
            // 
            this.listBox1.AllowDrop = true;
            this.listBox1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.listBox1.BackColor = System.Drawing.SystemColors.Control;
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 17;
            this.listBox1.Location = new System.Drawing.Point(940, 83);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox1.Size = new System.Drawing.Size(167, 68);
            this.listBox1.TabIndex = 34;
            // 
            // button8
            // 
            this.button8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button8.AutoEllipsis = true;
            this.button8.BackColor = System.Drawing.Color.Cyan;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button8.Location = new System.Drawing.Point(976, 437);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(99, 23);
            this.button8.TabIndex = 35;
            this.button8.Text = "Build port";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.BuildPortButtonClick);
            // 
            // MainGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 528);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.ClothResourceLabel);
            this.Controls.Add(this.MetalResourceLabel);
            this.Controls.Add(this.WoodResourceLabel);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.TeamLabel);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ManufactureVeryFast);
            this.Controls.Add(this.ManufactureFast);
            this.Controls.Add(this.ManufactureMedium);
            this.Controls.Add(this.ManufactureHeavy);
            this.Controls.Add(this.ManufactureLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ShipsList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "MainGameForm";
            this.Text = "Archipelago";
            this.Shown += new System.EventHandler(this.Form2_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ShipsList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label ManufactureLabel;
        private System.Windows.Forms.ComboBox ManufactureHeavy;
        private System.Windows.Forms.ComboBox ManufactureMedium;
        private System.Windows.Forms.ComboBox ManufactureFast;
        private System.Windows.Forms.ComboBox ManufactureVeryFast;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label TeamLabel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label WoodResourceLabel;
        private System.Windows.Forms.Label MetalResourceLabel;
        private System.Windows.Forms.Label ClothResourceLabel;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button8;
    }
}