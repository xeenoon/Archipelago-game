
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
            this.MoveButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.moveSettings = new System.Windows.Forms.Panel();
            this.MoveSpecificButton = new System.Windows.Forms.Button();
            this.ShipSelectBox = new System.Windows.Forms.CheckedListBox();
            this.MoveSpecificMenu = new System.Windows.Forms.Panel();
            this.button6 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.TeamLabel = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.WoodResourceLabel = new System.Windows.Forms.Label();
            this.MetalResourceLabel = new System.Windows.Forms.Label();
            this.ClothResourceLabel = new System.Windows.Forms.Label();
            this.shipList = new System.Windows.Forms.ListBox();
            this.BuildPortButton = new System.Windows.Forms.Button();
            this.ShipCargoPopup = new System.Windows.Forms.Panel();
            this.button10 = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.LoadCargoMenu = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.button11 = new System.Windows.Forms.Button();
            this.maskedTextBox3 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox2 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.button12 = new System.Windows.Forms.Button();
            this.LevelText = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label19 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.moveSettings.SuspendLayout();
            this.MoveSpecificMenu.SuspendLayout();
            this.ShipCargoPopup.SuspendLayout();
            this.LoadCargoMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(172, 0);
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
            this.ShipsList.Location = new System.Drawing.Point(940, 62);
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
            this.label3.Location = new System.Drawing.Point(940, 150);
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
            this.ManufactureLabel.Location = new System.Drawing.Point(937, 278);
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
            this.ManufactureHeavy.Location = new System.Drawing.Point(1020, 305);
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
            this.ManufactureMedium.Location = new System.Drawing.Point(1020, 336);
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
            this.ManufactureFast.Location = new System.Drawing.Point(1020, 367);
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
            this.ManufactureVeryFast.Location = new System.Drawing.Point(1020, 398);
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
            this.label4.Location = new System.Drawing.Point(961, 308);
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
            this.label5.Location = new System.Drawing.Point(948, 339);
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
            this.label6.Location = new System.Drawing.Point(973, 370);
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
            this.label7.Location = new System.Drawing.Point(942, 401);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 18);
            this.label7.TabIndex = 16;
            this.label7.Text = "Very fast";
            // 
            // MoveButton
            // 
            this.MoveButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.MoveButton.AutoEllipsis = true;
            this.MoveButton.BackColor = System.Drawing.Color.Goldenrod;
            this.MoveButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.MoveButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.MoveButton.Location = new System.Drawing.Point(940, 227);
            this.MoveButton.Name = "MoveButton";
            this.MoveButton.Size = new System.Drawing.Size(75, 23);
            this.MoveButton.TabIndex = 17;
            this.MoveButton.Text = "Move";
            this.MoveButton.UseVisualStyleBackColor = false;
            this.MoveButton.Click += new System.EventHandler(this.MoveButtonClick);
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
            // moveSettings
            // 
            this.moveSettings.Controls.Add(this.label8);
            this.moveSettings.Controls.Add(this.button3);
            this.moveSettings.Controls.Add(this.button4);
            this.moveSettings.Controls.Add(this.label2);
            this.moveSettings.Controls.Add(this.button2);
            this.moveSettings.Location = new System.Drawing.Point(410, 216);
            this.moveSettings.Name = "moveSettings";
            this.moveSettings.Size = new System.Drawing.Size(328, 98);
            this.moveSettings.TabIndex = 23;
            // 
            // MoveSpecificButton
            // 
            this.MoveSpecificButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.MoveSpecificButton.AutoEllipsis = true;
            this.MoveSpecificButton.BackColor = System.Drawing.Color.Goldenrod;
            this.MoveSpecificButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.MoveSpecificButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.MoveSpecificButton.Location = new System.Drawing.Point(1021, 227);
            this.MoveSpecificButton.Name = "MoveSpecificButton";
            this.MoveSpecificButton.Size = new System.Drawing.Size(86, 23);
            this.MoveSpecificButton.TabIndex = 24;
            this.MoveSpecificButton.Text = "Move specific";
            this.MoveSpecificButton.UseVisualStyleBackColor = false;
            this.MoveSpecificButton.Click += new System.EventHandler(this.MoveSpecificSquareButtonClick);
            // 
            // ShipSelectBox
            // 
            this.ShipSelectBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ShipSelectBox.CheckOnClick = true;
            this.ShipSelectBox.FormattingEnabled = true;
            this.ShipSelectBox.Items.AddRange(new object[] {
            "Steam corvette",
            "Steam corvette",
            "Steam corvette",
            "Steam corvette",
            "Steam corvette"});
            this.ShipSelectBox.Location = new System.Drawing.Point(4, 28);
            this.ShipSelectBox.Name = "ShipSelectBox";
            this.ShipSelectBox.Size = new System.Drawing.Size(100, 77);
            this.ShipSelectBox.TabIndex = 25;
            this.ShipSelectBox.ThreeDCheckBoxes = true;
            // 
            // MoveSpecificMenu
            // 
            this.MoveSpecificMenu.Controls.Add(this.button6);
            this.MoveSpecificMenu.Controls.Add(this.label9);
            this.MoveSpecificMenu.Controls.Add(this.ShipSelectBox);
            this.MoveSpecificMenu.Location = new System.Drawing.Point(520, 196);
            this.MoveSpecificMenu.Name = "MoveSpecificMenu";
            this.MoveSpecificMenu.Size = new System.Drawing.Size(107, 138);
            this.MoveSpecificMenu.TabIndex = 26;
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
            this.button6.Click += new System.EventHandler(this.CloseMoveSpecificMenuButtonClick);
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
            this.button7.Location = new System.Drawing.Point(976, 492);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(99, 33);
            this.button7.TabIndex = 27;
            this.button7.Text = "End turn";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.EndTurn);
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(940, 39);
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
            // shipList
            // 
            this.shipList.AllowDrop = true;
            this.shipList.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.shipList.BackColor = System.Drawing.SystemColors.Control;
            this.shipList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.shipList.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shipList.FormattingEnabled = true;
            this.shipList.ItemHeight = 17;
            this.shipList.Location = new System.Drawing.Point(940, 83);
            this.shipList.Name = "shipList";
            this.shipList.Size = new System.Drawing.Size(167, 68);
            this.shipList.TabIndex = 34;
            this.shipList.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // BuildPortButton
            // 
            this.BuildPortButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BuildPortButton.AutoEllipsis = true;
            this.BuildPortButton.BackColor = System.Drawing.Color.Cyan;
            this.BuildPortButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BuildPortButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BuildPortButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BuildPortButton.Location = new System.Drawing.Point(976, 465);
            this.BuildPortButton.Name = "BuildPortButton";
            this.BuildPortButton.Size = new System.Drawing.Size(99, 23);
            this.BuildPortButton.TabIndex = 35;
            this.BuildPortButton.Text = "Build port";
            this.BuildPortButton.UseVisualStyleBackColor = false;
            this.BuildPortButton.Click += new System.EventHandler(this.BuildPortButtonClick);
            // 
            // ShipCargoPopup
            // 
            this.ShipCargoPopup.Controls.Add(this.button10);
            this.ShipCargoPopup.Controls.Add(this.CloseButton);
            this.ShipCargoPopup.Controls.Add(this.label14);
            this.ShipCargoPopup.Controls.Add(this.label13);
            this.ShipCargoPopup.Controls.Add(this.label12);
            this.ShipCargoPopup.Location = new System.Drawing.Point(523, 83);
            this.ShipCargoPopup.Name = "ShipCargoPopup";
            this.ShipCargoPopup.Size = new System.Drawing.Size(100, 118);
            this.ShipCargoPopup.TabIndex = 36;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(12, 66);
            this.button10.Name = "button10";
            this.button10.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 4;
            this.button10.Text = "Load cargo";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.OpenCargoMenu);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(12, 92);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 3;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseShipCargoPopup);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(21, 25);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 39);
            this.label14.TabIndex = 2;
            this.label14.Text = "100 wood\r\n50 metal\r\n6 cloth";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(15, 33);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(0, 13);
            this.label13.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(3, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(92, 21);
            this.label12.TabIndex = 0;
            this.label12.Text = "Ship cargo";
            // 
            // LoadCargoMenu
            // 
            this.LoadCargoMenu.Controls.Add(this.label17);
            this.LoadCargoMenu.Controls.Add(this.label16);
            this.LoadCargoMenu.Controls.Add(this.label15);
            this.LoadCargoMenu.Controls.Add(this.button11);
            this.LoadCargoMenu.Controls.Add(this.maskedTextBox3);
            this.LoadCargoMenu.Controls.Add(this.maskedTextBox2);
            this.LoadCargoMenu.Controls.Add(this.maskedTextBox1);
            this.LoadCargoMenu.Location = new System.Drawing.Point(528, 204);
            this.LoadCargoMenu.Name = "LoadCargoMenu";
            this.LoadCargoMenu.Size = new System.Drawing.Size(91, 105);
            this.LoadCargoMenu.TabIndex = 37;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 59);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(31, 13);
            this.label17.TabIndex = 6;
            this.label17.Text = "Cloth";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 34);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(33, 13);
            this.label16.TabIndex = 5;
            this.label16.Text = "Metal";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 9);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(36, 13);
            this.label15.TabIndex = 4;
            this.label15.Text = "Wood";
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(9, 79);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(75, 23);
            this.button11.TabIndex = 3;
            this.button11.Text = "Load cargo";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // maskedTextBox3
            // 
            this.maskedTextBox3.Location = new System.Drawing.Point(45, 55);
            this.maskedTextBox3.Name = "maskedTextBox3";
            this.maskedTextBox3.Size = new System.Drawing.Size(43, 20);
            this.maskedTextBox3.TabIndex = 2;
            this.maskedTextBox3.Text = " ";
            // 
            // maskedTextBox2
            // 
            this.maskedTextBox2.Location = new System.Drawing.Point(45, 30);
            this.maskedTextBox2.Name = "maskedTextBox2";
            this.maskedTextBox2.Size = new System.Drawing.Size(43, 20);
            this.maskedTextBox2.TabIndex = 1;
            this.maskedTextBox2.Text = " ";
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(45, 5);
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(43, 20);
            this.maskedTextBox1.TabIndex = 0;
            this.maskedTextBox1.Text = " ";
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(940, 168);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(55, 54);
            this.label18.TabIndex = 38;
            this.label18.Text = "0 wood\r\n0 metal\r\n0 cloth\r\n";
            // 
            // button12
            // 
            this.button12.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button12.AutoEllipsis = true;
            this.button12.BackColor = System.Drawing.Color.Yellow;
            this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button12.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button12.Location = new System.Drawing.Point(970, 435);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(111, 26);
            this.button12.TabIndex = 39;
            this.button12.Text = "Upgrade port";
            this.button12.UseVisualStyleBackColor = false;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // LevelText
            // 
            this.LevelText.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.LevelText.AutoSize = true;
            this.LevelText.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LevelText.Location = new System.Drawing.Point(940, 256);
            this.LevelText.Name = "LevelText";
            this.LevelText.Size = new System.Drawing.Size(47, 18);
            this.LevelText.TabIndex = 40;
            this.LevelText.Text = "Level:";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button1.AutoEllipsis = true;
            this.button1.BackColor = System.Drawing.Color.LimeGreen;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Location = new System.Drawing.Point(17, 483);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 33);
            this.button1.TabIndex = 41;
            this.button1.Text = "Save game";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Modern No. 20", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(3, 195);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(0, 25);
            this.label19.TabIndex = 42;
            // 
            // MainGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 528);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.LevelText);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.LoadCargoMenu);
            this.Controls.Add(this.ShipCargoPopup);
            this.Controls.Add(this.BuildPortButton);
            this.Controls.Add(this.shipList);
            this.Controls.Add(this.ClothResourceLabel);
            this.Controls.Add(this.MetalResourceLabel);
            this.Controls.Add(this.WoodResourceLabel);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.TeamLabel);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.MoveSpecificMenu);
            this.Controls.Add(this.MoveSpecificButton);
            this.Controls.Add(this.moveSettings);
            this.Controls.Add(this.MoveButton);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1145, 567);
            this.MinimumSize = new System.Drawing.Size(1145, 567);
            this.Name = "MainGameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Archipelago";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainGameForm_FormClosing);
            this.Shown += new System.EventHandler(this.Form2_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.moveSettings.ResumeLayout(false);
            this.moveSettings.PerformLayout();
            this.MoveSpecificMenu.ResumeLayout(false);
            this.MoveSpecificMenu.PerformLayout();
            this.ShipCargoPopup.ResumeLayout(false);
            this.ShipCargoPopup.PerformLayout();
            this.LoadCargoMenu.ResumeLayout(false);
            this.LoadCargoMenu.PerformLayout();
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
        private System.Windows.Forms.Button MoveButton;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel moveSettings;
        private System.Windows.Forms.Button MoveSpecificButton;
        private System.Windows.Forms.CheckedListBox ShipSelectBox;
        private System.Windows.Forms.Panel MoveSpecificMenu;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label TeamLabel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label WoodResourceLabel;
        private System.Windows.Forms.Label MetalResourceLabel;
        private System.Windows.Forms.Label ClothResourceLabel;
        private System.Windows.Forms.ListBox shipList;
        private System.Windows.Forms.Button BuildPortButton;
        private System.Windows.Forms.Panel ShipCargoPopup;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Panel LoadCargoMenu;
        private System.Windows.Forms.MaskedTextBox maskedTextBox3;
        private System.Windows.Forms.MaskedTextBox maskedTextBox2;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Label LevelText;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label19;
    }
}