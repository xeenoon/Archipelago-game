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
        Team hasTurn = Team.Red; //Hasturn is a type of team which shows what teams turn it is
        public TeamMaterials teamMaterials; //A way of determining the materials that the players whose turn it is has

        public MainGameForm()
        {
            InitializeComponent();
            pictureboxBitmap = new Bitmap(pictureBox1.Image);
            pictureBox1.Image = pictureboxBitmap; //Load the picture

            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;

            ShipCargoPopup.Visible = false;
            LoadCargoMenu.Visible = false;

            moveSettings.Visible = false;
            MoveSpecificMenu.Visible = false;

            ManufactureFast.Visible = false;
            ManufactureLabel.Visible = false;
            ManufactureMedium.Visible = false;
            ManufactureHeavy.Visible = false;
            ManufactureVeryFast.Visible = false; //Make menu's that should be invisible invisible


            ManufactureFast.DropDownWidth = 70;
            ManufactureHeavy.DropDownWidth = 70;
            ManufactureMedium.DropDownWidth = 70;
            ManufactureVeryFast.DropDownWidth = 80; //Change the width of dropdown menu's

            for (int x = 0; x < horizontalSquares; ++x)
            {
                for (int y = 0; y < verticalSquares; ++y) //Iterate through all the spaces available for squares
                {
                    squares[x, y] = new Square(x, y); //add a square
                }
            }
            CreatePort(4, 1, Team.Red);
            CreatePort(5, 10, Team.Green);
            CreatePort(21, 5, Team.Black);
            CreatePort(22, 18, Team.Blue); //Create the starting ports

            teamMaterials = new TeamMaterials(WoodResourceLabel, MetalResourceLabel, ClothResourceLabel, new Materials(1000, 100, 12), new Materials(1000, 100, 12), new Materials(1000, 100, 12), new Materials(1000, 100, 12));
            //Set the starting materials for each player
        }
        public void CreatePort(int x, int y, Team t)
        {
            var g = Graphics.FromImage(pictureboxBitmap); //Get the graphics from the bitmap
            Brush b; //Define the brush
            switch (t) //Switch on colours to change the colour of the port
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
            g.FillEllipse(b, new Rectangle(CentreOf(ConvertLocationToReal(new Point(x, y)), new Size(35, 35)), new Size(35, 35))); //Draw the port
            squares[x, y].isPort = true; //Make the square a port
            squares[x, y].team = t; //Specify the team
            squares[x, y].generates = new Materials(200,20,2); //Make the port generate 200 wood, 20 metal and 2 cloth every turn
        }
        public Square[,] squares = new Square[horizontalSquares, verticalSquares]; //An array of all the squares on the board

        const int horizontalSquares = 29; //The amount of horizontal squares
        const int verticalSquares = 21; //The amount of vertical squares

        const int png_top = 35;
        const int png_left = 35; //Offsets

        const int png_boxsize = 69;//The original size of the box

        Square selected; //The current selected square
        Square selectCache; //The previously selected square
        public Point ConvertLocationToReal(Point p)
        {
            return new Point(p.X * png_boxsize + png_left + (png_boxsize / 2), p.Y * png_boxsize + png_top + (png_boxsize / 2)); //Converts a squares position to a form position
        }
        public Point CentreOf(Point topleft, Size s)
        {
            return new Point(topleft.X - s.Width / 2, topleft.Y - s.Height / 2); //Find the centre of a square
        }
        Bitmap pictureboxBitmap; //The bitmap for the picturebox
        private bool CanMove(int square_x, int square_y)//Determines if a square is allowed to be moved to. A ship cannot move to a square that is more than 90% green
        {
            var greenCount = 0; //The amount of green pixels in a square
            Bitmap asbitmap = new Bitmap(pictureBox1.Image);
            for (int x = (int)(square_x * png_boxsize + png_left); x < (square_x * png_boxsize) + png_boxsize + png_left; ++x)
            {
                for (int y = (int)(square_y * png_boxsize + png_top); y < (square_y * png_boxsize) + png_boxsize + png_top; ++y) //Iterate through pixels
                {
                    Color original = asbitmap.GetPixel(x, y); //Get the colour of the pixel

                    if (original.R > 120 && original.G > 150 && original.B > 120 && (original.R < 170 || original.B > 170) && ((original.B > original.G) || original.B > 160))//Is the square green?
                    {
                        greenCount++;//Increment the green count
                    }
                }
            }
            //total == 4761
            //10% = 476
            if (greenCount >= 476)
            {
                return true; //We can move to this square
            }
            return false; //We cannot move to this square
        }
        bool justmoved; //Did we just move something? Relative to OnSquareClick(), Should not be referenced elsewhere, unless function relates to moving
        private void OnSquareClick(int xpos, int ypos)
        {
            TeamLabel.Text = ""; //Reset team label

            if (MoveSquare && CanMove(xpos,ypos)) //Can we move? And are we moving everything
            {
                if (selected == squares[xpos,ypos]) //Was the previously selected square the current square
                {
                    justmoved = false; //We have not just moved

                    MoveButton.BackColor = Color.Goldenrod;
                    MoveSpecificButtpn.BackColor = Color.Goldenrod; //Reset button colours
                    MoveSpecificSquare = false;
                    MoveSquare = false; //Reset move options
                    return; //Exit function
                }
                //At this point we have clicked on a square that is not out current square
                if (selected.ships.Any(s => s.team == hasTurn)) //Are the ships in the square mine
                {
                    var distance = Math.Sqrt(Math.Pow(xpos - selected.location.X, 2) + Math.Pow(ypos - selected.location.Y, 2)) - 0.45; //Calculate distance. -0.45 allows for diagonal movement
                    foreach (var ship in selected.ships) //Iterate through all ships in square
                    {
                        if ((int)ship.shipType < distance && !ship.hasMoved) //Is the ship unable to move the required distance and has it not already been moved this turn
                        {
                            moveSettings.Visible = true; //Set the movesettings panel visible
                            selectCache = squares[selected.location.X, selected.location.Y]; //Make the previously selected square the selected square
                            selected = squares[xpos, ypos];//Make the newly selected square the selected square
                            HighlightSquare(xpos, ypos); //Highlight the square
                            return; //Exit the function
                        }
                    }
                    List<Ship> replaceList = new List<Ship>(); //Since later on we clear the ships in the square, we need to figure out which ships stay in the square and store them in a list to be added back later
                    foreach (var ship in selected.ships) //Iterate through ships
                    {
                        if (ship.hasMoved) //Has the ship moved this turn?
                        {
                            replaceList.Add(ship); //Add it to the cache list
                            continue; //Continue to the next iteration
                        }
                        //Otherwise (else not required because of continue statement)
                        squares[xpos, ypos].ships.Add(ship); //Add the ship to the newly selected square
                        ship.hasMoved = true; //Since we just moved the ship, set the ships hasMoved status to true. This will stop it moving again this turn
                    }
                    selected.ships = replaceList; //Use the cache list to refill the selected list
                    MoveSquare = false; //We are no longer moving, so set this indicator to false
                    MoveButton.BackColor = Color.Goldenrod; //Reset button colours
                    justmoved = true; //Set the 'justmoved' variable to true, signifying that we have just finished moving some ships
                }
            }
            else if (MoveSpecificSquare && CanMove(xpos, ypos)) //Instead of moving all ships are we only moving specific ships and can we move to the position
            {
                if (selected.ships.Any(s => s.team == hasTurn)) //Are the ships in the square my team
                {
                    var distance = Math.Sqrt(Math.Pow(xpos - selected.location.X, 2) + Math.Pow(ypos - selected.location.Y, 2)) - 0.45f; //Calculate the distance betwen the two squares
                    foreach (var ship in selected.ships.Where(s => s.moveNext)) //Iterate through the ships in the square that have been selected to be moved
                    {
                        if ((int)ship.shipType < distance && !ship.hasMoved) //Is the ship unable to move the distance and has it not already been moved
                        {
                            moveSettings.Visible = true; //Show the movesettings menu
                            selectCache = squares[selected.location.X, selected.location.Y]; //cache the selected squares
                            selected = squares[xpos, ypos]; //Set the new selected square to the selected position
                            HighlightSquare(xpos, ypos); //Highlight the selected square
                            return;
                        }
                    }
                    List<Ship> replaceList = new List<Ship>(); //Since later on we clear the ships in the square, we need to figure out which ships stay in the square and store them in a list to be added back later
                    foreach (var ship in selected.ships.Where(s => s.moveNext)) //Iterate through the ships in the square that have been selected to be moved
                    {
                        if (ship.hasMoved) //Has the ship already moved?
                        {
                            replaceList.Add(ship); //Add the ship to the replace list
                            continue; //Continue to the next
                        }
                        //Else statement is not required because of the continue in the line above
                        squares[xpos, ypos].ships.Add(ship); //Add the ship to the newly selected square
                        ship.hasMoved = true; //The ship has now moved so set its indicator to true
                    }
                    selected.ships.RemoveAll(s => s.moveNext); //Removeall  ships from the square that where set to move
                    selected.ships.AddRange(replaceList); //Add the cache list to the selected ships
                    MoveSpecificSquare = false; //Set the move indicator to false
                    foreach (var s in squares[xpos, ypos].ships) //Iterate through all ships in the square we are moving too
                    {
                        s.moveNext = false; //Set the movenext to false
                    }
                    MoveSpecificButtpn.BackColor = Color.Goldenrod; //Reset the button colour
                    justmoved = true; //We have just moved some ships, so set that indicator to true
                }
            }
            selected = squares[xpos, ypos]; //Set the selected square to the newly selected poxition
            if (squares[xpos, ypos].isPort == true && squares[xpos,ypos].team == hasTurn) //Is the newly selected square a port, and is the port ours
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
                //Make the relative menu items visible
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
                //Otherwise, set those menu items to false
            }
            if (justmoved) //Have we just moved somewhere
            {
                RunAttack(selected); //Attack the square
            }

            shipList.Items.Clear(); //Clear all the items in the UI shiplist
            foreach (var ship in selected.ships) //Iterate through all the ships in the square
            {
                shipList.Items.Add(ship.name+ "  " + ship.health); //Update the shiplist with a new ship, specifying the name and the health of the new ship
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
            } //Change the colour and text of the team label
            Team squaresShips = selected.ships.Count >= 1 ? selected.ships[0].team : Team.None;
            //Set the team of the square to being the team of the first ship in the ship list
            //Even though we specified that earlier, we need to double check because the square may not be a port
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
            }//Update team labels
            Materials myMats = selected.GetMaterials();
            label18.Text = string.Format("{0} wood\n{1} metal\n{2} cloth", myMats.wood, myMats.metal, myMats.cloth); //Update cargo label
            HighlightSquare(xpos, ypos); //Highlight the selected square
            RepaintShipPicture(); //Repaint the picture

            justmoved = false; 

            MoveButton.BackColor = Color.Goldenrod;
            MoveSpecificButtpn.BackColor = Color.Goldenrod; //Reset button colours
            MoveSpecificSquare = false;
            MoveSquare = false; //Reset move options
        }
        Random r = new Random(); //The random class to be used when attacking

        private void RunAttack(Square square)
        {
            while (square.ships.Select(s => s.team).Distinct().Count() >= 2) //While there are ships to attack and ships to defend
            {
                var teams = square.ships.Select(s => s.team).Distinct().ToList(); //What teams are in the square
                foreach (var team in teams) //Iterate through the teams in the square
                {
                    int totalCannons = square.ships.Where(s => s.team == team).Sum(s => s.cannons); //Find out how many total cannons their are on all of the teams ships
                    Ship ship = square.ships.First(p => p.team != team); //Select a ship to do damage to
                    var damage = totalCannons * r.Next(1, 7); //Calculate damage done, random number from 1 inclusive to 7 exclusive
                    ship.health -= damage; //Decrement ships health
                    MessageBox.Show(string.Format("Team {0} did {1} damage", team, damage), "FIGHT", MessageBoxButtons.OK); //Show damage data
                    if (ship.health <= 0) //Did the ship die
                    {
                        MessageBox.Show(string.Format("Team {0} killed {1}", team, ship.name), "FIGHT", MessageBoxButtons.OK); //Show death data
                        square.ships.Remove(ship); //Remove the ship from the square
                    }
                }
            }
            if (square.ships.Where(s => s.team == hasTurn).Count() >= 1 && square.isPort == true) //After we have determined the attacker has won, determine if the square is a port
            {
                square.team = hasTurn; //Change the team of the port to the winning team
                CreatePort(square.location.X, square.location.Y, hasTurn); //Change the colour indicator on the screen
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = pictureboxBitmap; //Set the image to the stored bitmap

            double top = png_top / 1500.0 * pictureBox1.Height;
            double left = png_left / 2000.0 * pictureBox1.Width;

            double boxsize_x = png_boxsize / 2000.0 * pictureBox1.Width;
            double boxsize_y = png_boxsize / 1500.0 * pictureBox1.Height; //Calculate offsets and boxsizes

            Point mousepos = pictureBox1.PointToClient(Cursor.Position); //Calculate mousesize relative to the form

            //label1.Text = string.Format("{0}:{1}", mousepos.X, mousepos.Y);

            int square_x = (int)Math.Floor(((double)mousepos.X - left) / boxsize_x);
            int square_y = (int)Math.Floor(((double)mousepos.Y - top) / boxsize_y); //Calculate which square has been clicked
            //label2.Text = string.Format("{0},{1}", square_x, square_y);

            try
            {
                OnSquareClick(square_x, square_y); //Run the squareclick function
            }
            catch
            {
                return; //Something went wrong, just return and ignore the error
            }
        }

        private void BuildShip(object sender, EventArgs e)
        {
            string shipName = ((ComboBox)(sender)).Text; //Figure out which ship they have selected
            Ship s = Ship.Create(shipName); //Create the ship

            var build = MessageBox.Show(string.Format("This build requires {0} wood, {1} metal, {2} cloth. The ship has {3} hp and {4} cannons", s.required.wood, s.required.metal, s.required.cloth, s.health, s.cannons), "BuildShip", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
            //Show the messagebox
            if (build) //Does the user want to build?
            {
                CreateShip((ComboBox)sender); //Create the ship
            }
        }

        private void CreateShip(ComboBox sender)
        {
            if (selected.ships.Count >= 3) //Is there already three ships in the harbour
            {
                MessageBox.Show("You can only have three ships in harbour at once"); //There is no space in the shipyard to build the ship if there are more than three ships there, you can still store more than three ships in harbour though
                return; //Exit the function
            }

            string shipName = sender.Text; //Find out which ship was selected
            Ship s = Ship.Create(shipName); //Create the new ship

            if (Materials.Canbuild(teamMaterials.GetMaterials(hasTurn), s.required)) //Can we build the ship
            {
                s.team = hasTurn; //Set the ships team
                shipList.Items.Add(s.name + "  " + s.health); //Add the ship to shiplist
                teamMaterials.Pay(hasTurn, s.required); //Pay the required materials for the new ship
                selected.ships.Add(s); //Add the ship to the square
            }
            else
            {
                MessageBox.Show("You do not have enough materials to build " + shipName);
            }
        }

        bool MoveSquare;
        bool MoveSpecificSquare; //Move settings indicator

        private void MoveButtonClick(object sender, EventArgs e)
        {
            if (selected == null) //Have we selected a square
            {
                return;
            }

            MoveSquare = true; //Set the move indicator to true
            MoveButton.BackColor = Color.AliceBlue; //Change button colour
            foreach (var s in selected.ships) //Iterate through ships
            {
                s.moveNext = true; //Tell the ships to move on the next square click
            }
        }

        private void Cancel(object sender, EventArgs e)
        {
            moveSettings.Visible = false; //Make the movesettings panel invisible
            selectCache = null; //Delete select cache

            MoveSquare = false; //Set move indicator to false
            MoveButton.BackColor = Color.Goldenrod; //Change button colour

            OnSquareClick(selected.location.X, selected.location.Y); //Run onsquareclick
        }

        private void StayInSquare(object sender, EventArgs e)
        {
            var xpos = selected.location.X;
            var ypos = selected.location.Y; //Determine the selected x and y positions

            var distance = Math.Sqrt(Math.Pow(xpos - selectCache.location.X, 2) + Math.Pow(ypos - selectCache.location.Y, 2))-0.45; //Calculate the distance
            List<Ship> replacelis = new List<Ship>(); //Create a replace cache list
            foreach (var ship in selectCache.ships.Where(s=>s.moveNext)) //Iterate through the ships that where suppost to move
            {
                if ((int)ship.shipType >= distance) //Can the ship move to the square?
                {
                    squares[xpos, ypos].ships.Add(ship); //Move the ship
                }
                else
                {
                    replacelis.Add(ship); //Otherwise add it to cache
                }
            }
            selectCache.ships = replacelis; //Make the previous squares ship = the cache
            MoveSquare = false; //Reset move indicator
            MoveButton.BackColor = Color.Goldenrod; //Change button colour

            moveSettings.Visible = false; //Make menu invisible

            OnSquareClick(selected.location.X, selected.location.Y); //Run onsquareclick
        }
        private void HighlightSquare(int square_x, int square_y)
        {
            Bitmap result = new Bitmap(pictureBox1.Image); //Create a bitmap of the shown image

            for (int x = (int)(square_x * png_boxsize + png_left); x < (square_x * png_boxsize) + png_boxsize + png_left; ++x)
            {
                for (int y = (int)(square_y * png_boxsize + png_top); y < (square_y * png_boxsize) + png_boxsize + png_top; ++y) //Iterate through all pixels
                {
                    Color original = result.GetPixel(x, y); //Get the colour of the pixel

                    if (original.R > 120 && original.G > 150 && original.B > 120 && (original.R < 170 || original.B > 170) && ((original.B > original.G) || original.B > 160)) //Is it blue?
                    {
                        Color filter = Color.FromArgb(original.R + 70 >= 255 ? 255 : original.R + 70, original.G - 50 <= 0 ? 0 : original.G - 50, original.B - 100 <= 0 ? 0 : original.B - 100); //Create the filter
                        result.SetPixel(x, y, filter); //Replace the old pixel with the filter
                    }
                }
            }

            pictureBox1.Image = result; //Change the image being shown to the one with the filter
        }

        private void HighlightSquare(int square_x, int square_y, Filter filter)
        {
            Bitmap result = new Bitmap(pictureBox1.Image); //Create a bitmap based off the picture

            for (int x = (int)(square_x * png_boxsize + png_left); x < (square_x * png_boxsize) + png_boxsize + png_left; ++x)
            {
                for (int y = (int)(square_y * png_boxsize + png_top); y < (square_y * png_boxsize) + png_boxsize + png_top; ++y) //Iterate through all pixels
                {
                    Color original = result.GetPixel(x, y); //Get the pixel's colour

                    if (original.R > 120 && original.G > 150 && original.B > 120 && (original.R < 170 || original.B > 170) && ((original.B > original.G) || original.B > 160)) //Is it blue?
                    {
                        var finalColour = new Filter(original.R + filter.R, original.G + filter.G, original.B + filter.B); //Create filter
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
                        } //If any rgb value is larger than 255, make it 255
                        result.SetPixel(x, y, finalColour.AsColor()); //Change the colour of the pixel to the new filter colour
                    }
                }
            }

            pictureBox1.Image = result; //Replace the image with the filtered result
        }

        private void MoveClosest(object sender, EventArgs e)
        {
            var xpos = selected.location.X;
            var ypos = selected.location.Y; //Find out the x and y positions of the newly selected square

            var distance = Math.Sqrt(Math.Pow(xpos - selectCache.location.X, 2) + Math.Pow(ypos - selectCache.location.Y, 2)); //Calculate the distance
            foreach (var ship in selectCache.ships.Where(s => s.moveNext).ToList()) //Iterate through the ships that where supposed to move
            {
                ship.hasMoved = true; //The ship will move in all scenarios

                if ((int)ship.shipType >= distance) //Can the ship move to the destination
                {
                    squares[xpos, ypos].ships.Add(ship); //Move the ship
                }
                else
                {
                    PointF closest = MoveTowards(selectCache.location, selected.location, (int)ship.shipType+0.45f); //Calculate the closest point the ship can move too
                    squares[(int)(Math.Floor(closest.X)), (int)(Math.Floor(closest.Y))].ships.Add(ship); //Calculate which square this lies in, then move the ship there
                }

                selectCache.ships.Remove(ship); //Remove the ship from the previous swuare
            }
            MoveSquare = false; //Reset move settings
            MoveButton.BackColor = Color.Goldenrod; //Change button colour

            moveSettings.Visible = false; //Make the move menu invisible

            OnSquareClick(selected.location.X, selected.location.Y); //Run onsquare click
        }

        private PointF MoveTowards(Point origin, Point dest, float distance) //Moves from one point to another for a distance
        {
            var vector = new PointF(dest.X - origin.X, dest.Y - origin.Y); //Calculate vector
            var length = Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y); //Determine length
            var unitVector = new PointF(vector.X / (float)length, vector.Y / (float)length); //relative vector
            PointF result = new PointF((float)(origin.X + unitVector.X * distance), (float)(origin.Y + unitVector.Y * distance)); //Result point
            double xdiff = Math.Abs(dest.X - origin.X);
            double ydiff = Math.Abs(dest.Y - origin.Y); 
            if (xdiff <= distance / 2)
            {
                result.X = dest.X;
            }
            if (ydiff <= distance / 2)
            {
                result.Y = dest.Y;
            }//Settle differences
            return result;
        }

        private void RepaintShipPicture()
        {
            foreach (var s in squares)
            {
                if (s.ships.Count >= 1) //Are there ships in the square?
                {
                    HighlightSquare(s.location.X, s.location.Y, new Filter(100,-100,-100)); //Highlight square with a red filter (increases red by 100, decreases everything else by 100)
                }
            }
        }

        private void MoveSpecificSquareButtonClick(object sender, EventArgs e)
        {
            if (selected == null) //Is no square selected
            {
                return; //Exit function to ensure there are no errors
            }
            MoveSquare = false;
            MoveSpecificSquare = true; //Modify move indicators
            
            MoveButton.BackColor = Color.Goldenrod;
            MoveSpecificButtpn.BackColor = Color.AliceBlue; //Change button colours

            ShipSelectBox.Items.Clear(); //Remove all items from the ship select box, this ensures that we do not keep the items from the last select
            foreach (var s in selected.ships) //Iterate through all ships
            {
                ShipSelectBox.Items.Add(s.name + "  " + s.health); //Add them to the list
            }
            MoveSpecificMenu.Visible = true; //Make the menu visible
        }

        private void CloseMoveSpecificMenuButtonClick(object sender, EventArgs e)
        {
            foreach (var s in selected.ships)
            {
                s.moveNext = false; //Set all ships movenext values to false
            }
            foreach (var item in ShipSelectBox.CheckedItems) //Iterate through all ships that have been selected
            {
                var items = ((string)item).Split(new string[] {"  "}, StringSplitOptions.None); //Create an array of strings of the different items. The array contains the ships name and its health.
                selected.ships.First(s => s.name == items[0] && s.health.ToString() == items[1]).moveNext = true; //Set the selected ships movenext to true
            }
            MoveSpecificMenu.Visible = false; //Make the movemenu disapear
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            MessageBox.Show("Red teams turn", "Red", MessageBoxButtons.OK); //Signify that red will start
            Materials total = new Materials(0, 0, 0); //The amount of materials the player earns at the start of the game

            foreach (var s in squares) //Iterate through squares
            {
                if (s.isPort && s.team == hasTurn) //Go through each port owned by the player
                {
                    total += s.generates; //Increment the amount of materials the player has
                }
            }
            MessageBox.Show(string.Format("Your ports generated you {0} wood, {1} metal, {2} cloth", total.wood, total.metal, total.cloth), "New materials", MessageBoxButtons.OK); //Show data in a messagebox
            teamMaterials.AddMaterials(hasTurn, total); //Add the materials to the teamMaterials

            teamMaterials.Show(hasTurn); //Show the materials on the left side of the screen
        }

        private void EndTurn(object sender, EventArgs e)
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
            } //Change which team will have a turn next
            foreach (var square in squares)
            {
                foreach (var ship in square.ships) //Iterate through all ships in all squares
                {
                    ship.hasMoved = false; //They all have not moved on this turn, so set their hasmoved variable to false
                }
            }
            MessageBox.Show(hasTurn.ToString() + " teams turn", hasTurn.ToString(), MessageBoxButtons.OK); //Signify which teams turn is next
            OnSquareClick(0,0); //Deselct the previously selected square by the player who had a turn before
            pictureBox1.Image = pictureboxBitmap; //Reset the picturebox1 bitmap
            HighlightSquare(0,0); //Highlight the selected square
            RepaintShipPicture(); //Repaint the image to include ports and highlights

            Materials total = new Materials(0, 0, 0); //The amount of materials the player earns at the start of the game

            foreach (var s in squares) //Iterate through squares
            {
                if (s.isPort && s.team == hasTurn) //Go through each port owned by the player
                {
                    total += s.generates; //Increment the amount of materials the player has
                }
            }
            MessageBox.Show(string.Format("Your ports generated you {0} wood, {1} metal, {2} cloth", total.wood, total.metal, total.cloth), "New materials", MessageBoxButtons.OK); //Show data in a messagebox
            teamMaterials.AddMaterials(hasTurn, total); //Add the materials to the teamMaterials

            teamMaterials.Show(hasTurn); //Show the materials on the left side of the screen
        }

        private void BuildPortButtonClick(object sender, EventArgs e)
        {
            if (selected == null)
            {
                MessageBox.Show("Please select a square to build a port in", "Error", MessageBoxButtons.OK); //Cannot build port on empty square
            }
            else
            {
                var response = MessageBox.Show("Build level 1 port for 1000 wood?", "Build port", MessageBoxButtons.YesNo);
                if (response == DialogResult.Yes) //Does the user want to build a port?
                {
                    if (selected.GetMaterials().wood >= 1000) //Does the user have enough wood to build the port INSIDE the square
                    {
                        CreatePort(selected.location.X, selected.location.Y, hasTurn); //Create the port at the selected location
                        pictureBox1.Image = pictureboxBitmap; //Reset the picturebox bitmap
                        selected.Buy(new Materials(1000,0,0)); //Spend the materials on the port

                        Materials myMats = selected.GetMaterials(); //Temporary variable to make next line of code more readable. Gets the materials inside the square
                        label18.Text = string.Format("{0} wood\n{1} metal\n{2} cloth", myMats.wood, myMats.metal, myMats.cloth); //Reload the text that tells you how much cargo you have
                    }
                    else
                    {
                        MessageBox.Show("You do not have enough wood to build a level 1 port", "Not enough materials", MessageBoxButtons.OK); //If they do not have enough wood, let them know
                    }
                }
            }
        }
        Ship selectedShip;
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (shipList.SelectedIndex == -1) //Nothing is selected
            {
                return; //If nothing is selected, just return
            }
            //When the select index is changed, a new ship has been selected
            selectedShip = selected.ships[shipList.SelectedIndex]; //Change which ship has been selected
            ShipCargoPopup.Visible = true; //Make the cargo menu visible
            label14.Text = string.Format("{0} wood\n{1} metal\n{2} cloth", selectedShip.loaded.wood, selectedShip.loaded.metal, selectedShip.loaded.cloth); //Show cargo data
        }

        private void CloseShipCargoPopup(object sender, EventArgs e)
        {
            ShipCargoPopup.Visible = false; //Make shipcargo popup invisible
            shipList.SelectedIndex = -1; //Change the selected index to -1 to deselect the item
            Materials myMats = selected.GetMaterials();
            label18.Text = string.Format("{0} wood\n{1} metal\n{2} cloth", myMats.wood, myMats.metal, myMats.cloth); //Update cargo data
        }

        private void OpenCargoMenu(object sender, EventArgs e)
        {
            LoadCargoMenu.Visible = true; //Show the loading cargo menu
            CloseButton.Enabled = false; //Other menu cannot be closed until we are finished loading cargo
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
                woodAmount = int.Parse(maskedTextBox1.Text);   //Define material amounts
                metalAmount = int.Parse(maskedTextBox2.Text);  //Define material amounts
                clothAmount = int.Parse(maskedTextBox3.Text);  //Define material amounts
            }
            catch
            {
                MessageBox.Show("Input must be a number", "Error", MessageBoxButtons.OK);
                return;
            }
            Materials cargoToLoad = new Materials(woodAmount,metalAmount,clothAmount); //Create a new materials object to store the data the user inputted
            if (cargoToLoad.GetWeight() > selectedShip.cargoCapacity+selectedShip.loaded.GetWeight())
            {
                MessageBox.Show("Cargo too heavy", "Error", MessageBoxButtons.OK);
                return;
            }
            if (cargoToLoad < teamMaterials.GetMaterials(hasTurn))
            {
                teamMaterials.Pay(hasTurn, new Materials(1000,0,0));
                //Decreasing the teams materials by the amount of cargo to be loaded
            }
            else
            {
                MessageBox.Show("You do not have enough materials", "Error", MessageBoxButtons.OK);
                return;
            }
            selectedShip.LoadMaterials(cargoToLoad); //Load the materials into the ship
            label14.Text = string.Format("{0} wood\n{1} metal\n{2} cloth", selectedShip.loaded.wood, selectedShip.loaded.metal, selectedShip.loaded.cloth); //Update cargo text
            LoadCargoMenu.Visible = false; //Close the loadCargoMenu
            CloseButton.Enabled = true; //Re-enable the close menu button since we have finished loading cargo
        }
    }
    public struct Filter
    {
        /**
         * A class for storing incremental changes in colour
         * Contains an R, B, and G value which will decrement or increment a different color
         * */
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
