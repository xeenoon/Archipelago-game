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
    public partial class MainGameForm : Form
    {
        Team hasTurn = Team.Red;
        public TeamMaterials teamMaterials;

        public MainGameForm()
        {
            InitializeComponent();
            pictureboxBitmap = new Bitmap(pictureBox1.Image);
            pictureBox1.Image = pictureboxBitmap;

            ManufactureFast.Visible = false;
            ManufactureLabel.Visible = false;
            ManufactureMedium.Visible = false;
            ManufactureHeavy.Visible = false;
            ManufactureVeryFast.Visible = false;


            ManufactureFast.DropDownWidth = 70;
            ManufactureHeavy.DropDownWidth = 70;
            ManufactureMedium.DropDownWidth = 70;
            ManufactureVeryFast.DropDownWidth = 80;


            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            ShipCargoPopup.Visible = false;
            LoadCargoMenu.Visible = false;

            for (int x = 0; x < horizontalSquares; ++x)
            {
                for (int y = 0; y < verticalSquares; ++y)
                {
                    squares[x, y] = new Square(x, y);
                }
            }
            CreatePort(4, 1, Team.Red);
            CreatePort(5, 10, Team.Green);
            CreatePort(21, 5, Team.Black);
            CreatePort(22, 18, Team.Blue);
            panel1.Visible = false;
            panel2.Visible = false;

            teamMaterials = new TeamMaterials(WoodResourceLabel, MetalResourceLabel, ClothResourceLabel, new Materials(1000, 100, 12), new Materials(1000, 100, 12), new Materials(1000, 100, 12), new Materials(1000, 100, 12));
        }
        public void CreatePort(int x, int y, Team t)
        {
            var g = Graphics.FromImage(pictureboxBitmap);
            Brush b;
            switch (t)
            {
                case Team.None:
                    throw new Exception("Team is not allowed to be none");
                case Team.Red:
                    b = Brushes.Red;
                    break;
                case Team.Green:
                    b = Brushes.Green;
                    break;
                case Team.Black:
                    b = Brushes.Black;
                    break;
                case Team.Blue:
                    b = Brushes.Blue;
                    break;
                default:
                    throw new Exception();
            }
            g.FillEllipse(b, new Rectangle(CentreOf(ConvertLocationToReal(new Point(x, y)), new Size(35, 35)), new Size(35, 35)));
            squares[x, y].isPort = true;
            squares[x, y].team = t;
            squares[x, y].generates = new Materials(200,20,2);
        }
        public Square[,] squares = new Square[horizontalSquares, verticalSquares];

        const int horizontalSquares = 29;
        const int verticalSquares = 21;

        const int png_top = 35;
        const int png_left = 35;

        const int png_boxsize = 69;

        Square selected;
        Square selectCache;
        public Point ConvertLocationToReal(Point p)
        {
            return new Point(p.X * png_boxsize + png_left + (png_boxsize / 2), p.Y * png_boxsize + png_top + (png_boxsize / 2));
        }
        public Point CentreOf(Point topleft, Size s)
        {
            return new Point(topleft.X - s.Width / 2, topleft.Y - s.Height / 2);
        }
        Bitmap pictureboxBitmap;
        bool justmoved;
        private bool CanMove(int square_x, int square_y)
        {
            var greenCount = 0;
            Bitmap asbitmap = new Bitmap(pictureBox1.Image);
            for (int x = (int)(square_x * png_boxsize + png_left); x < (square_x * png_boxsize) + png_boxsize + png_left; ++x)
            {
                for (int y = (int)(square_y * png_boxsize + png_top); y < (square_y * png_boxsize) + png_boxsize + png_top; ++y)
                {
                    Color original = asbitmap.GetPixel(x, y);

                    if (original.R > 120 && original.G > 150 && original.B > 120 && (original.R < 170 || original.B > 170) && ((original.B > original.G) || original.B > 160))
                    {
                        greenCount++;
                    }
                }
            }
            //total == 4761
            //10% = 476
            if (greenCount >= 476)
            {
                return true;
            }
            return false;
        }
        private void OnSquareClick(int xpos, int ypos)
        {
            TeamLabel.Text = "";

            if (MoveSquare && CanMove(xpos,ypos))
            {
                if (selected == squares[xpos,ypos])
                {
                    justmoved = false;

                    button1.BackColor = Color.Goldenrod;
                    button5.BackColor = Color.Goldenrod;
                    MoveSpecificSquare = false;
                    MoveSquare = false;
                    return;
                }

                if (selected.ships.Any(s => s.team == hasTurn))
                {
                    var distance = Math.Sqrt(Math.Pow(xpos - selected.location.X, 2) + Math.Pow(ypos - selected.location.Y, 2)) - 0.45;
                    foreach (var ship in selected.ships)
                    {
                        if ((int)ship.shipType < distance && !ship.hasMoved)
                        {
                            panel1.Visible = true;
                            selectCache = squares[selected.location.X, selected.location.Y];
                            selected = squares[xpos, ypos];
                            HighlightSquare(xpos, ypos);
                            return;
                        }
                    }
                    List<Ship> replaceList = new List<Ship>();
                    foreach (var ship in selected.ships)
                    {
                        if (ship.hasMoved)
                        {
                            replaceList.Add(ship);
                            continue;
                        }
                        squares[xpos, ypos].ships.Add(ship);
                        ship.hasMoved = true;
                    }
                    selected.ships = replaceList;
                    //      selectCache.ships = replaceList;
                    MoveSquare = false;
                    button1.BackColor = Color.Goldenrod;
                    justmoved = true;
                }
            }
            else if (MoveSpecificSquare && CanMove(xpos, ypos))
            {
                if (selected.ships.Any(s => s.team == hasTurn))
                {
                    var distance = Math.Sqrt(Math.Pow(xpos - selected.location.X, 2) + Math.Pow(ypos - selected.location.Y, 2)) - 0.45f;
                    foreach (var ship in selected.ships.Where(s => s.moveNext))
                    {
                        if ((int)ship.shipType < distance && !ship.hasMoved)
                        {
                            panel1.Visible = true;
                            selectCache = squares[selected.location.X, selected.location.Y];
                            selected = squares[xpos, ypos];
                            HighlightSquare(xpos, ypos);
                            return;
                        }
                    }
                    List<Ship> replaceList = new List<Ship>();
                    foreach (var ship in selected.ships.Where(s => s.moveNext))
                    {
                        if (ship.hasMoved)
                        {
                            replaceList.Add(ship);
                            continue;
                        }
                        squares[xpos, ypos].ships.Add(ship);
                        ship.hasMoved = true;
                    }
                    selected.ships.RemoveAll(s => s.moveNext);
                    selected.ships.AddRange(replaceList);
                    MoveSpecificSquare = false;
                    foreach (var s in squares[xpos, ypos].ships)
                    {
                        s.moveNext = false;
                    }
                    button5.BackColor = Color.Goldenrod;
                    justmoved = true;
                }
            }
            selected = squares[xpos, ypos];
            if (squares[xpos, ypos].isPort == true && squares[xpos,ypos].team == hasTurn)
            {
                ManufactureFast.Visible = true;
                ManufactureLabel.Visible = true;
                ManufactureMedium.Visible = true;
                ManufactureHeavy.Visible = true;
                ManufactureVeryFast.Visible = true;

                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
            }
            else
            {
                ManufactureFast.Visible = false;
                ManufactureLabel.Visible = false;
                ManufactureMedium.Visible = false;
                ManufactureHeavy.Visible = false;
                ManufactureVeryFast.Visible = false;

                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
            }
            if (justmoved)
            {
                RunAttack(selected);
            }

            listBox1.Items.Clear();
            foreach (var ship in selected.ships)
            {
                listBox1.Items.Add(ship.name+ "  " + ship.health);
            }

            switch (selected.team)
            {
                case Team.Red:
                    TeamLabel.Text = "Red";
                    TeamLabel.ForeColor = Color.Red;
                    break;
                case Team.Green:
                    TeamLabel.Text = "Green";
                    TeamLabel.ForeColor = Color.Green;
                    break;
                case Team.Black:
                    TeamLabel.Text = "Black";
                    TeamLabel.ForeColor = Color.Black;
                    break;
                case Team.Blue:
                    TeamLabel.Text = "Blue";
                    TeamLabel.ForeColor = Color.Blue;
                    break;
            }
            Team squaresShips = Team.None;
            foreach (var s in selected.ships)
            {
                squaresShips = s.team;
            }
            switch (squaresShips)
            {
                case Team.Red:
                    TeamLabel.Text = "Red";
                    TeamLabel.ForeColor = Color.Red;
                    break;
                case Team.Green:
                    TeamLabel.Text = "Green";
                    TeamLabel.ForeColor = Color.Green;
                    break;
                case Team.Black:
                    TeamLabel.Text = "Black";
                    TeamLabel.ForeColor = Color.Black;
                    break;
                case Team.Blue:
                    TeamLabel.Text = "Blue";
                    TeamLabel.ForeColor = Color.Blue;
                    break;
            }
            Materials myMats = selected.GetMaterials();
            label18.Text = string.Format("{0} wood\n{1} metal\n{2} cloth", myMats.wood, myMats.metal, myMats.cloth);
            HighlightSquare(xpos, ypos);
            RepaintShipPicture();

            justmoved = false;

            button1.BackColor = Color.Goldenrod;
            button5.BackColor = Color.Goldenrod;
            MoveSpecificSquare = false;
            MoveSquare = false;
        }
        Random r = new Random();

        private void RunAttack(Square square)
        {
            while (square.ships.Select(s => s.team).Distinct().Count() >= 2) 
            {
                var teams = square.ships.Select(s => s.team).Distinct().ToList();
                foreach (var team in teams)
                {
                    int totalCannons = square.ships.Where(s => s.team == team).Sum(s => s.cannons);
                    Ship ship = square.ships.First(p => p.team != team);
                    var damage = totalCannons * r.Next(1, 7);
                    ship.health -= damage;
                    MessageBox.Show(string.Format("Team {0} did {1} damage", team, damage), "FIGHT", MessageBoxButtons.OK);
                    if (ship.health <= 0)
                    {
                        MessageBox.Show(string.Format("Team {0} killed {1}", team, ship.name), "FIGHT", MessageBoxButtons.OK);
                        square.ships.Remove(ship);
                    }
                }
            }
            if (square.ships.Where(s => s.team == hasTurn).Count() >= 1 && square.isPort == true)
            {
                square.team = hasTurn;
                CreatePort(square.location.X, square.location.Y, hasTurn);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = pictureboxBitmap;

            double top = png_top / 1500.0 * pictureBox1.Height;
            double left = png_left / 2000.0 * pictureBox1.Width;

            double boxsize_x = png_boxsize / 2000.0 * pictureBox1.Width;
            double boxsize_y = png_boxsize / 1500.0 * pictureBox1.Height;

            Point mousepos = pictureBox1.PointToClient(Cursor.Position);

            //label1.Text = string.Format("{0}:{1}", mousepos.X, mousepos.Y);

            int square_x = (int)Math.Floor(((double)mousepos.X - left) / boxsize_x);
            int square_y = (int)Math.Floor(((double)mousepos.Y - top) / boxsize_y);
            //label2.Text = string.Format("{0},{1}", square_x, square_y);

            try
            {
                OnSquareClick(square_x, square_y);
            }
            catch
            {
                return;
            }
        }

        private void BuildShip(object sender, EventArgs e)
        {
            string shipName = ((ComboBox)(sender)).Text;
            Ship s = Ship.Create(shipName);

            var build = MessageBox.Show(string.Format("This build requires {0} wood, {1} metal, {2} cloth. The ship has {3} hp and {4} cannons", s.required.wood, s.required.metal, s.required.cloth, s.health, s.cannons), "BuildShip", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
            if (build)
            {
                CreateShip(sender);
            }
        }

        private void CreateShip(object sender)
        {
            if (selected.ships.Count == 3)
            {
                MessageBox.Show("You can only have three ships in harbour at once");
                return;
            }

            string shipName = ((ComboBox)(sender)).Text;
            Ship s = Ship.Create(shipName);

            if (Materials.Canbuild(teamMaterials.GetMaterials(hasTurn), s.required))
            {
                s.team = hasTurn;
                listBox1.Items.Add(s.name + "  " + s.health);
                teamMaterials.Pay(hasTurn, s.required);
                selected.ships.Add(s);
            }
            else
            {
                MessageBox.Show("You do not have enough materials to build " + shipName);
            }
        }

        bool MoveSquare;

        public bool MoveSpecificSquare;

        private void button1_Click(object sender, EventArgs e)
        {
            if (selected == null)
            {
                return;
            }

            MoveSquare = true;
            button1.BackColor = Color.AliceBlue;
            foreach (var s in selected.ships)
            {
                s.moveNext = true;
            }
        }

        private void Cancel(object sender, EventArgs e)
        {
            panel1.Visible = false;
            selectCache = null;

            MoveSquare = false;
            button1.BackColor = Color.Goldenrod;

            OnSquareClick(selected.location.X, selected.location.Y);
        }

        private void StayInSquare(object sender, EventArgs e)
        {
            var xpos = selected.location.X;
            var ypos = selected.location.Y;

            var distance = Math.Sqrt(Math.Pow(xpos - selectCache.location.X, 2) + Math.Pow(ypos - selectCache.location.Y, 2))-0.45;
            List<Ship> replacelis = new List<Ship>();
            foreach (var ship in selectCache.ships.Where(s=>s.moveNext))
            {
                if ((int)ship.shipType >= distance)
                {
                    squares[xpos, ypos].ships.Add(ship);
                }
                else
                {
                    replacelis.Add(ship);
                }
            }
            selectCache.ships = replacelis;
            MoveSquare = false;
            button1.BackColor = Color.Goldenrod;

            panel1.Visible = false;

            OnSquareClick(selected.location.X, selected.location.Y);
        }
        private void HighlightSquare(int square_x, int square_y)
        {
            Bitmap result = new Bitmap(pictureBox1.Image);

            for (int x = (int)(square_x * png_boxsize + png_left); x < (square_x * png_boxsize) + png_boxsize + png_left; ++x)
            {
                for (int y = (int)(square_y * png_boxsize + png_top); y < (square_y * png_boxsize) + png_boxsize + png_top; ++y)
                {
                    Color original = result.GetPixel(x, y);

                    if (original.R > 120 && original.G > 150 && original.B > 120 && (original.R < 170 || original.B > 170) && ((original.B > original.G) || original.B > 160))
                    {
                        Color filter = Color.FromArgb(original.R + 70 >= 255 ? 255 : original.R + 70, original.G - 50 <= 0 ? 0 : original.G - 50, original.B - 100 <= 0 ? 0 : original.B - 100);
                        result.SetPixel(x, y, filter);
                    }
                }
            }

            pictureBox1.Image = result;
        }

        private void HighlightSquare(int square_x, int square_y, Filter filter)
        {
            Bitmap result = new Bitmap(pictureBox1.Image);

            for (int x = (int)(square_x * png_boxsize + png_left); x < (square_x * png_boxsize) + png_boxsize + png_left; ++x)
            {
                for (int y = (int)(square_y * png_boxsize + png_top); y < (square_y * png_boxsize) + png_boxsize + png_top; ++y)
                {
                    Color original = result.GetPixel(x, y);

                    if (original.R > 120 && original.G > 150 && original.B > 120 && (original.R < 170 || original.B > 170) && ((original.B > original.G) || original.B > 160))
                    {
                        var finalColour = new Filter(original.R + filter.R, original.G + filter.G, original.B + filter.B);
                        if (finalColour.R > 255)
                        {
                            finalColour = new Filter(255, finalColour.G, finalColour.B);
                        }
                        if (finalColour.G > 255)
                        {
                            finalColour = new Filter(finalColour.R, 255, finalColour.B);
                        }
                        if (finalColour.G > 255)
                        {
                            finalColour = new Filter(finalColour.R, finalColour.G, 255);
                        }
                        result.SetPixel(x, y, finalColour.AsColor());
                    }
                }
            }

            pictureBox1.Image = result;
        }

        private void MoveClosest(object sender, EventArgs e)
        {
            var xpos = selected.location.X;
            var ypos = selected.location.Y;

            var distance = Math.Sqrt(Math.Pow(xpos - selectCache.location.X, 2) + Math.Pow(ypos - selectCache.location.Y, 2));
            foreach (var ship in selectCache.ships.Where(s => s.moveNext).ToList())
            {
                ship.hasMoved = true;

                if ((int)ship.shipType >= distance)
                {
                    squares[xpos, ypos].ships.Add(ship);
                }
                else
                {
                    PointF closest = MoveTowards(selectCache.location, selected.location, (int)ship.shipType+0.45f);
                    squares[(int)(Math.Floor(closest.X)), (int)(Math.Floor(closest.Y))].ships.Add(ship);
                }

                selectCache.ships.Remove(ship);
            }
            //selectCache.ships.RemoveAll((s => s.moveNext));
            MoveSquare = false;
            button1.BackColor = Color.Goldenrod;

            panel1.Visible = false;

            OnSquareClick(selected.location.X, selected.location.Y);
        }

        private PointF MoveTowards(Point origin, Point dest, float distance)
        {
            var vector = new PointF(dest.X - origin.X, dest.Y - origin.Y);
            var length = Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            var unitVector = new PointF(vector.X / (float)length, vector.Y / (float)length);
            PointF result = new PointF((float)(origin.X + unitVector.X * distance), (float)(origin.Y + unitVector.Y * distance));
            double xdiff = Math.Abs(dest.X - origin.X);
            double ydiff = Math.Abs(dest.Y - origin.Y);
            if (xdiff <= distance / 2)
            {
                result.X = dest.X;
            }
            if (ydiff <= distance / 2)
            {
                result.Y = dest.Y;
            }
            return result;
        }

        private void RepaintShipPicture()
        {
            foreach (var s in squares)
            {
                if (s.ships.Count >= 1)
                {
                    HighlightSquare(s.location.X, s.location.Y, new Filter(100,-100,-100));
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (selected == null)
            {
                return;
            }
            MoveSquare = false;
            MoveSpecificSquare = true;
            button1.BackColor = Color.Goldenrod;
            button5.BackColor = Color.AliceBlue;

            checkedListBox1.Items.Clear();
            foreach (var s in selected.ships)
            {
                checkedListBox1.Items.Add(s.name + "  " + s.health);
            }
            panel2.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            foreach (var s in selected.ships)
            {
                s.moveNext = false;
            }
            foreach (var item in checkedListBox1.CheckedItems)
            {
                var items = ((string)item).Split(new string[] {"  "}, StringSplitOptions.None);
                selected.ships.First(s => s.name == items[0] && s.health.ToString() == items[1]).moveNext = true;
            }
            panel2.Visible = false;
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            MessageBox.Show("Red teams turn", "Red", MessageBoxButtons.OK);
            Materials total = new Materials(0, 0, 0);

            foreach (var s in squares)
            {
                if (s.isPort && s.team == hasTurn)
                {
                    total += s.generates;
                }
            }
            MessageBox.Show(string.Format("Your ports generated you {0} wood, {1} metal, {2} cloth", total.wood, total.metal, total.cloth), "New materials", MessageBoxButtons.OK);
            teamMaterials.AddMaterials(hasTurn, total);

            teamMaterials.Show(hasTurn);

        }

        private void button7_Click(object sender, EventArgs e)
        {
            switch (hasTurn)
            {
                case Team.None:
                    throw new Exception("Team cannot be none");
                case Team.Red:
                    hasTurn = Team.Green;
                    break;
                case Team.Green:
                    hasTurn = Team.Black;
                    break;
                case Team.Black:
                    hasTurn = Team.Blue;
                    break;
                case Team.Blue:
                    hasTurn = Team.Red;
                    break;
            }
            foreach (var square in squares)
            {
                foreach (var ship in square.ships)
                {
                    ship.hasMoved = false;
                }
            }
            MessageBox.Show(hasTurn.ToString() + " teams turn", hasTurn.ToString(), MessageBoxButtons.OK);
            teamMaterials.Show(hasTurn);
            OnSquareClick(0,0);
            pictureBox1.Image = pictureboxBitmap;
            HighlightSquare(0,0);
            RepaintShipPicture();

            Materials total = new Materials(0,0,0);

            foreach (var s in squares)
            {
                if (s.isPort && s.team == hasTurn)
                {
                    total += s.generates;
                }
            }
            MessageBox.Show(string.Format("Your ports generated you {0} wood, {1} metal, {2} cloth", total.wood, total.metal, total.cloth),"New materials" , MessageBoxButtons.OK);
            teamMaterials.AddMaterials(hasTurn, total);
        }

        private void BuildPortButtonClick(object sender, EventArgs e)
        {
            if (selected == null)
            {
                MessageBox.Show("Please select a square to build a port in", "Error", MessageBoxButtons.OK);
            }
            else
            {
                var response = MessageBox.Show("Build level 1 port for 1000 wood?", "Build port", MessageBoxButtons.YesNo);
                if (response == DialogResult.Yes)
                {
                    if (selected.GetMaterials().wood >= 1000)
                    {
                        CreatePort(selected.location.X, selected.location.Y, hasTurn);
                        pictureBox1.Image = pictureboxBitmap;
                        selected.Buy(new Materials(1000,0,0));

                        Materials myMats = selected.GetMaterials();
                        label18.Text = string.Format("{0} wood\n{1} metal\n{2} cloth", myMats.wood, myMats.metal, myMats.cloth); //Reload the text that tells you how much cargo you have
                    }
                    else
                    {
                        MessageBox.Show("You do not have enough wood to build a level 1 port", "Not enough materials", MessageBoxButtons.OK);
                    }
                }
            }
        }
        Ship selectedShip;
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) //Nothing is selected
            {
                return;
            }
            //When the select index is changed, a new ship has been selected
            selectedShip = selected.ships[listBox1.SelectedIndex];
            ShipCargoPopup.Visible = true;
            label14.Text = string.Format("{0} wood\n{1} metal\n{2} cloth", selectedShip.loaded.wood, selectedShip.loaded.metal, selectedShip.loaded.cloth);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ShipCargoPopup.Visible = false;
            listBox1.SelectedIndex = -1;
            Materials myMats = selected.GetMaterials();
            label18.Text = string.Format("{0} wood\n{1} metal\n{2} cloth", myMats.wood, myMats.metal, myMats.cloth);
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            LoadCargoMenu.Visible = true;
            button9.Enabled = false;
        }

        private void MainGameForm_Click(object sender, EventArgs e)
        {
            maskedTextBox1.Text = "Wood";
            maskedTextBox2.Text = "Metal";
            maskedTextBox3.Text = "Cloth";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text == " ")
            {
                maskedTextBox1.Text = "0";
            }
            if (maskedTextBox3.Text == " ")
            {
                maskedTextBox3.Text = "0";
            }
            if (maskedTextBox2.Text == " ")
            {
                maskedTextBox2.Text = "0";
            } //Allow for blank boxes. These will default to 0

            int woodAmount  =0;
            int metalAmount =0;
            int clothAmount = 0;
            try
            {
                woodAmount = int.Parse(maskedTextBox1.Text);
                metalAmount = int.Parse(maskedTextBox2.Text);
                clothAmount = int.Parse(maskedTextBox3.Text);
            }
            catch
            {
                MessageBox.Show("Input must be a number", "Error", MessageBoxButtons.OK);
                return;
            }
            Materials cargoToLoad = new Materials(woodAmount,metalAmount,clothAmount);
            if (cargoToLoad.GetWeight() > selectedShip.cargoCapacity+selectedShip.loaded.GetWeight())
            {
                MessageBox.Show("Cargo too heavy", "Error", MessageBoxButtons.OK);
                return;
            }
            if (cargoToLoad < teamMaterials.GetMaterials(hasTurn))
            {
                switch (hasTurn)
                {
                    case Team.Red:
                        teamMaterials.redMaterials-=cargoToLoad;
                        break;
                    case Team.Green:
                        teamMaterials.redMaterials -= cargoToLoad;
                        break;
                    case Team.Black:
                        teamMaterials.redMaterials -= cargoToLoad;
                        break;
                    case Team.Blue:
                        teamMaterials.redMaterials -= cargoToLoad;
                        break;
                } //Using switch statement here because we cannot assign a valud to GetMaterials() here
            }
            else
            {
                MessageBox.Show("You do not have enough materials", "Error", MessageBoxButtons.OK);
                return;
            }
            selectedShip.LoadMaterials(cargoToLoad);
            label14.Text = string.Format("{0} wood\n{1} metal\n{2} cloth", selectedShip.loaded.wood, selectedShip.loaded.metal, selectedShip.loaded.cloth);
            LoadCargoMenu.Visible = false;
            button9.Enabled = true;
        }
    }
    public struct Filter
    {
        public int R;
        public int G;
        public int B;

        public Filter(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }

        internal Color AsColor()
        {
            return Color.FromArgb(R,G,B);
        }
    }
}
