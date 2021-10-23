﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Archipelago
{
    public partial class MainGameForm : Form
    {
        public enum MapType
        {
            SmallIslands,
            Continents,
        }
        public static Team hasTurn = Team.None; //Hasturn is a type of team which shows what teams turn it is
        public static TeamMaterials teamMaterials; //A way of determining the materials that the players whose turn it is has
        public static MainGameForm currentForm;
        public static MapType maptype = MapType.SmallIslands;
        public MainGameForm(Team playerTeams, Team AiTeams, MapType maptype)
        {
            InitializeComponent();
            MainGameForm.maptype = maptype;
            if (maptype == MapType.SmallIslands) {
                pictureBox1.Image = Properties.Resources.Archipelago2;
            }
            else
            {
                pictureBox1.Image = Properties.Resources.Archipelago;
            }
            pictureboxBitmap = new Bitmap(pictureBox1.Image);
            pictureBox1.Image = pictureboxBitmap; //Load the picture

            Ship.Setup();//Setup the ships

            currentForm = this; //Assign the current form to the form that is running

            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;

            ShipCargoPopup.Visible = false;
            LoadCargoMenu.Visible = false;

            moveSettings.Visible = false;
            MoveSpecificMenu.Visible = false;

            button12.Visible = false;

            BuildPortButton.Visible = false;

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
            //CanMovePopulate(); Only to be used if repopulating squareValidity. Just copy and paste from txt file. Change username in Filepath to your own username
            playerTeams |= AiTeams;
            if (maptype == MapType.Continents) {
                if ((playerTeams & Team.Red) == Team.Red) {
                    CreatePort(4, 2, Team.Red);
                }
                if ((playerTeams & Team.Green) == Team.Green)
                {
                    CreatePort(11, 14, Team.Green);
                }
                if ((playerTeams & Team.Black) == Team.Black)
                {
                    CreatePort(21, 5, Team.Black);
                }
                if ((playerTeams & Team.Blue) == Team.Blue)
                {
                    CreatePort(22, 18, Team.Blue);
                }
            }
            else
            {
                if ((playerTeams & Team.Red) == Team.Red)
                {
                    CreatePort(5, 2, Team.Red);
                }
                if ((playerTeams & Team.Green) == Team.Green)
                {
                    CreatePort(4, 18, Team.Green);
                }
                if ((playerTeams & Team.Black) == Team.Black)
                {
                    CreatePort(24, 4, Team.Black);
                }
                if ((playerTeams & Team.Blue) == Team.Blue)
                {
                    CreatePort(22, 17, Team.Blue);
                }
            }
            AIteam = AiTeams;
            moveSettings.Visible = false;
            MoveSpecificMenu.Visible = false;
            OnSquareClick(0,0); //Highlight the top left square
            teamMaterials = new TeamMaterials(WoodResourceLabel, MetalResourceLabel, ClothResourceLabel, new Materials(1000, 100, 12), new Materials(1000, 100, 12), new Materials(1000, 100, 12), new Materials(1000, 100, 12));
            //Set the starting materials for each player
        }
        public static void CreatePort(int x, int y, Team t)
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
                case Team.Pirate:
                    b = Brushes.Brown;
                    break;
                default:
                    throw new Exception();
            }

            g.FillEllipse(b, new Rectangle(CentreOf(ConvertLocationToReal(new Point(x, y)), new Size(35, 35)), new Size(35, 35)));
            if (!squares[x,y].isPort) //Is it not already a port?
            {
                squares[x, y].generates = CalculatedGenerated(x, y); //Calculate how many materials will be generated by the port
            } //Otherwise we want to keep the original generates because of the compounded materials gained by adding levels
            squares[x, y].isPort = true;
            squares[x, y].team = t;

            currentForm.RepaintShipPicture(); //Repaint the picture to include the newly highlighted bitmap
            currentForm.OnSquareClick(x, y); //Highlight the port
        }
        public static Materials CalculatedGenerated(int square_x, int square_y)
        {
            Materials baseMats = new Materials(200, 20, 3);
            float blueCount=0;
            float greenCount=0;
            for (int x = (int)(square_x * png_boxsize + png_left); x < (square_x * png_boxsize) + png_boxsize + png_left; ++x)
            {
                for (int y = (int)(square_y * png_boxsize + png_top); y < (square_y * png_boxsize) + png_boxsize + png_top; ++y) //Iterate through all squares
                {
                    Color original = pictureboxBitmap.GetPixel(x, y);

                    if (original.R > 120 && original.G > 150 && original.B > 120 && (original.R < 170 || original.B > 170) && ((original.B > original.G) || original.B > 160))
                    {
                        blueCount++;
                    }
                    else
                    {
                        greenCount++;
                    } //Increment or decrement the blue or green count depending on the colour of the pixel
                }
            }
            var ratio = greenCount/blueCount; //Get the ratio of green to blue
            if (greenCount/blueCount >= 1.85f)
            {
                ratio = 1.85f;
            }
            return baseMats * ratio;
        }

        public static Square[,] squares = new Square[horizontalSquares, verticalSquares];

        public const int horizontalSquares = 29; //The amount of horizontal squares
        public const int verticalSquares = 21; //The amount of vertical squares

        const int png_top = 35;
        const int png_left = 35; //Offsets

        const int png_boxsize = 69;//The original size of the box

        public static Square selected; //The current selected square
        Square selectCache; //The previously selected square
        public static Point ConvertLocationToReal(Point p)
        {
            return new Point(p.X * png_boxsize + png_left + (png_boxsize / 2), p.Y * png_boxsize + png_top + (png_boxsize / 2)); //Converts a squares position to a form position
        }
        public static Point CentreOf(Point topleft, Size s)
        {
            return new Point(topleft.X - s.Width / 2, topleft.Y - s.Height / 2); //Find the centre of a square
        }
        static Bitmap pictureboxBitmap; //The bitmap for the picturebox
        public bool CanBuildPort(int square_x, int square_y)
        {
            Bitmap asbitmap = pictureboxBitmap; //The original bitmap, not the highlighted bitmap
            for (int x = (int)(square_x * png_boxsize + png_left); x < (square_x * png_boxsize) + png_boxsize + png_left; ++x)
            {
                for (int y = (int)(square_y * png_boxsize + png_top); y < (square_y * png_boxsize) + png_boxsize + png_top; ++y) //Iterate through pixels
                {
                    Color original = asbitmap.GetPixel(x, y); //Get the colour of the pixel

                    if (original.R > 120 && original.G > 150 && original.B > 120 && (original.R < 170 || original.B > 170) && ((original.B > original.G) || original.B > 160))//Is the square green?
                    {
                        return true; //There is at least one green pixel in the square
                    }
                }
            }
            return false; //There are no green squares in the square
        }
        public static bool[,] ContinentValidity = new bool[,] {
            { true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , true  },
            { true , true , true , true , true , true , true , true , true , true , true , true , true , false, false, true , true , true , true , true , true  },
            { true , false, false, true , true , true , true , true , true , true , true , true , true , false, false, true , true , true , true , true , true  },
            { true , true , false, false, true , true , false, true , true , true , true , true , false, false, true , true , true , true , true , true , true  },
            { true , true , true , true , false, true , true , true , true , true , true , false, false, true , true , true , true , true , true , true , true  },
            { true , true , true , true , true , true , true , true , true , true , true , false, true , true , true , true , true , true , true , true , true  },
            { true , true , true , true , true , false, true , true , true , true , false, false, false, false, false, false, false, true , true , true , true  },
            { true , true , true , true , true , true , true , true , true , true , true , false, false, false, false, false, false, true , true , true , true  },
            { true , true , true , true , true , true , true , true , true , true , true , true , false, false, false, true , true , true , true , true , true  },
            { true , true , true , true , true , true , true , true , true , true , true , true , true , false, false, true , true , true , true , false, true  },
            { true , true , true , true , true , true , true , true , true , true , true , true , true , false, false, true , true , true , true , true , true  },
            { true , true , true , true , true , true , false, false, true , true , true , true , true , false, true , true , true , false, false, true , true  },
            { true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , false, true , true  },
            { true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , true  },
            { true , true , true , false, true , true , false, true , true , true , true , true , true , true , true , true , true , true , true , true , true  },
            { true , true , true , true , false, false, false, true , true , true , true , true , true , true , true , true , true , true , true , true , true  },
            { true , true , true , true , true , false, true , true , true , true , true , true , true , true , true , true , true , true , true , true , true  },
            { true , false, true , true , true , false, true , true , true , true , true , true , true , true , true , true , true , true , true , true , true  },
            { true , false, true , true , true , false, true , true , true , true , true , true , true , true , true , true , true , true , true , true , true  },
            { true , true , true , true , false, true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , true  },
            { true , true , true , true , false, true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , true  },
            { true , true , true , true , true , true , true , true , true , false, true , true , true , true , true , true , true , true , false, true , true  },
            { true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , true  },
            { true , true , false, false, true , true , true , true , true , true , true , true , true , true , false, true , true , true , true , true , true  },
            { true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , false, false, false, true , true , true  },
            { true , true , true , true , true , true , true , true , true , false, false, true , true , true , true , true , false, true , true , true , true  },
            { true , true , true , true , true , false, true , true , false, true , true , true , true , true , true , true , true , true , true , true , true  },
            { true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , true , true  },
        };
        public static bool[,] IslandValidity = new bool[,] { 
            {true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true },
            {true ,true ,true ,true ,false,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,false,true ,true },
            {true ,true ,true ,false,false,false,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,false,true ,true ,true },
            {true ,true ,true ,true ,false,false,true ,true ,false,false,true ,true ,true ,true ,true ,true ,true ,false,false,true ,true },
            {true ,true ,false,false,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true },
            {true ,true ,true ,false,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true },
            {true ,true ,true ,false,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,false,true },
            {true ,true ,true ,false,false,true ,true ,true ,true ,true ,false,true ,true ,true ,true ,true ,true ,true ,false,true ,true },
            {true ,true ,true ,false,true ,false,true ,true ,true ,true ,true ,true ,false,true ,true ,true ,true ,true ,true ,true ,true },
            {true ,true ,true ,false,false,false,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true },
            {true ,true ,true ,true ,true ,true ,true ,true ,true ,false,true ,true ,true ,true ,true ,true ,true ,true ,true ,false,true },
            {true ,false,true ,true ,true ,true ,true ,true ,true ,false,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true },
            {true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true },
            {true ,false,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,false,true ,true ,true ,true ,true ,false,true ,true },
            {true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,false,true ,true ,false,false,true ,true ,true ,true },
            {false,true ,false,true ,true ,true ,true ,true ,true ,true ,true ,true ,false,true ,true ,false,false,true ,true ,true ,true },
            {true ,true ,false,true ,true ,false,false,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true },
            {true ,true ,false,true ,true ,true ,true ,true ,false,false,false,true ,true ,true ,true ,true ,true ,true ,true ,true ,true },
            {true ,true ,true ,true ,true ,true ,true ,true ,true ,false,true ,true ,true ,true ,true ,false,true ,true ,true ,true ,true },
            {true ,true ,true ,true ,true ,true ,false,true ,true ,true ,true ,true ,true ,true ,true ,false,true ,true ,true ,true ,true },
            {true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true },
            {true ,true ,true ,true ,true ,true ,true ,true ,false,true ,true ,true ,true ,true ,true ,true ,true ,true ,false,true ,true },
            {true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true },
            {true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,false,true ,true ,true ,true ,true ,true ,true ,true ,true ,true },
            {true ,true ,true ,true ,true ,true ,true ,true ,true ,false,true ,true ,true ,true ,true ,true ,true ,true ,false,true ,true },
            {true ,true ,true ,true ,true ,true ,true ,false,true ,true ,true ,true ,true ,true ,true ,true ,true ,false,true ,true ,true },
            {true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true },
            {true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true ,true },};
        //     public List<bool[,]> squareValidityFromFile()
        //     {
        //         //Iterate through the strings using string parsing techniques
        //         //This will get you a list of bool[,]
        //     }
        public static void CanMovePopulate()//Determines if a square is allowed to be moved to. A ship cannot move to a square that is more than 90% green
        {
            
            string Filepath = @"C:\Users\chris\Downloads\writeme.txt";
            string result = "public static bool[,] squareValidity = new bool[,] { ";
            for (int square_x = 0; square_x < horizontalSquares-1; square_x++)
            {
                string toadd = "{";
                for (int square_y = 0; square_y < verticalSquares; square_y++)
                {
                    var greenCount = 0; //The amount of green pixels in a square
                    Bitmap asbitmap = pictureboxBitmap; //The original bitmap, not the highlighted bitmap
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
                        toadd += "true ,";
                    }
                    else
                    {
                        toadd += "false,";
                    }
                }
                toadd = toadd.Substring(0,toadd.Length-1);
                toadd += "},\n";
                result += toadd;
            }
            result = result.Substring(0,result.Length-1); //Remove last character
            result += "};";
            File.WriteAllText(Filepath, File.ReadAllText(Filepath)+"\n"+result);
        } //This function is only used to write text to a file. Should not be referenced in code
        public static bool CanMove(int square_x, int square_y)
        {
            if (square_x == 28)
            {
                return false;
            }
            if (maptype == MapType.SmallIslands)
            {
                return IslandValidity[square_x, square_y];
            }
            else
            {
                return ContinentValidity[square_x, square_y];
            }
        }
        bool justmoved; //Did we just move something? Relative to OnSquareClick(), Should not be referenced elsewhere, unless function relates to moving
        public void OnSquareClick(int xpos, int ypos)
        {
            TeamLabel.Text = ""; //Reset team label

            if (MoveSquare && CanMove(xpos, ypos)) //Can we move? And are we moving everything
            {
                if (selected == squares[xpos, ypos]) //Was the previously selected square the current square
                {
                    justmoved = false; //We have not just moved

                    MoveButton.BackColor = Color.Goldenrod;
                    MoveSpecificButton.BackColor = Color.Goldenrod; //Reset button colours
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
                        if ((int)ship.shipType < distance && !ship.hasMoved)
                        //Is the ship unable to move the required distance
                        //Has it not already moved this turn
                        {
                            moveSettings.Visible = true; //Set the movesettings panel visible
                            selectCache = squares[selected.location.X, selected.location.Y]; //Make the previously selected square the selected square
                            selected = squares[xpos, ypos];//Make the newly selected square the selected square
                            HighlightSquare(xpos, ypos); //Highlight the square
                            return; //Exit the function
                        }
                    }
                    List<Ship> replaceList = new List<Ship>(); //Since later on we clear the ships in the square, we need to figure out which ships stay in the square and store them in a list to be added back later
                    bool enemies = (squares[xpos, ypos].GetTeam() != Team.None && squares[xpos, ypos].GetTeam() != hasTurn);
                    foreach (var ship in selected.ships) //Iterate through ships
                    {
                        if (ship.hasMoved || (enemies ^ ship.canAttack)) //Has the ship moved this turn
                                                                         //Or if it cannot attack, make sure it is not going to move to a square with enemies in it
                        {
                            replaceList.Add(ship); //Add it to the cache list
                            continue; //Continue to the next iteration
                        }
                        //Otherwise (else not required because of continue statement)
                        squares[xpos, ypos].ships.Add(ship); //Add the ship to the newly selected square
                        ship.hasMoved = true; //Since we just moved the ship, set the ships hasMoved status to true. This will stop it moving again this turn
                    }
                    selected.ships = replaceList; //Use the cache list to refill the selected list
                    //MoveSquare = false; //We are no longer moving, so set this indicator to false
                    MoveButton.BackColor = Color.Goldenrod; //Reset button colours
                    justmoved = true; //Set the 'justmoved' variable to true, signifying that we have just finished moving some ships                }
                }
            }
            else if (MoveSpecificSquare && CanMove(xpos, ypos)) //Instead of moving all ships are we only moving specific ships and can we move to the position
            {
                if (selected.ships.Any(s => s.team == hasTurn)) //Are the ships in the square my team
                {
                    var distance = Math.Sqrt(Math.Pow(xpos - selected.location.X, 2) + Math.Pow(ypos - selected.location.Y, 2)) - 0.45f; //Calculate the distance betwen the two squares
                    foreach (var ship in selected.ships.Where(s => s.moveNext)) //Iterate through the ships in the square that have been selected to be moved
                    {
                        bool enemies = (squares[xpos, ypos].GetTeam() != Team.None && squares[xpos, ypos].GetTeam() != hasTurn);
                        if ((int)ship.shipType < distance && !ship.hasMoved && !(enemies ^ ship.canAttack))
                            //Is the ship unable to move the required distance
                            //Has it not already moved this turn
                            //If it cannot attack, make sure it is not going to move to a square with enemies in it
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
                    MoveSpecificButton.BackColor = Color.Goldenrod; //Reset the button colour
                    justmoved = true; //We have just moved some ships, so set that indicator to true
                }
            }
            selected = squares[xpos, ypos]; //Set the selected square to the newly selected poxition
            if (squares[xpos, ypos].isPort == true && squares[xpos, ypos].team == hasTurn) //Is the newly selected square a port, and is the port ours
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
                shipList.Items.Add(ship.name + "  " + ship.health); //Update the shiplist with a new ship, specifying the name and the health of the new ship
            }

            switch (selected.GetTeam())
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
            LevelText.Text = "Level: " + selected.level; //Update level text

            RepaintShipPicture(); //Repaint the picture
            HighlightSquare(xpos, ypos); //Highlight the selected square

            if (selected.isPort && selected.team == hasTurn)
            {
                button12.Visible = true;
            }
            else
            {
                button12.Visible = false;
            }

            justmoved = false;

            MoveButton.BackColor = Color.Goldenrod;
            MoveSpecificButton.BackColor = Color.Goldenrod; //Reset button colours
            MoveSpecificSquare = false;
            MoveSquare = false; //Reset move options

            if (selected.ships.Count() != 0 && selected.isPort == false && CanBuildPort(selected.location.X, selected.location.Y))
            {
                BuildPortButton.Visible = true;
            }
            else
            {
                BuildPortButton.Visible = false;
            }

            if (selectCache != null && selectCache != selected) //Make sure we are not going to do an operation on a null variable, also ensure we have not selected the square we selected last time
            {
                selectCache.orange = false; //The previously selected square should no longer be orange
            }
        }
        Random r = new Random(); //The random class to be used when attacking

        public static void RunAttack(Square square)
        {
            while (square.ships.Select(s => s.team).Distinct().Count() >= 2) //While there are ships to attack and ships to defend
            {
                var teams = square.ships.Select(s => s.team).Distinct().ToList(); //What teams are in the square
                foreach (var team in teams) //Iterate through the teams in the square
                {
                    int totalCannons = square.ships.Where(s => s.team == team).Sum(s => s.cannons); //Find out how many total cannons their are on all of the teams ships
                    Ship ship = square.ships.First(p => p.team != team); //Select a ship to do damage to
                    var damage = totalCannons * currentForm.r.Next(1, 7); //Calculate damage done, random number from 1 inclusive to 7 exclusive
                    //We have to use current form because this is a static function
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
                //We have to use current form because this is a static function
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            

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
                s.canAttack = false; //Ship cannot move on the same turn it was built
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
            squares[square_x, square_y].orange = true;

            Bitmap result = new Bitmap(pictureBox1.Image); //Create a bitmap of the shown image

            for (int x = (int)(square_x * png_boxsize + png_left); x < (square_x * png_boxsize) + png_boxsize + png_left; ++x)
            {
                for (int y = (int)(square_y * png_boxsize + png_top); y < (square_y * png_boxsize) + png_boxsize + png_top; ++y) //Iterate through all pixels
                {
                    Color original = pictureboxBitmap.GetPixel(x, y); //Get the colour of the pixel

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
            squares[square_x, square_y].red = true;

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
        private void RemoveHighlight(int square_x, int square_y)
        {
            Bitmap result = new Bitmap(pictureBox1.Image); //Create a bitmap based off the picture

            for (int x = (int)(square_x * png_boxsize + png_left); x < (square_x * png_boxsize) + png_boxsize + png_left; ++x)
            {
                for (int y = (int)(square_y * png_boxsize + png_top); y < (square_y * png_boxsize) + png_boxsize + png_top; ++y) //Iterate through all pixels
                {
                    result.SetPixel(x, y, pictureboxBitmap.GetPixel(x, y)); //Change the colour of the pixel to the original pixels colour
                }
            }
            squares[square_x, square_y].red = false;
            squares[square_x, square_y].orange = false;

            pictureBox1.Image = result; //Replace the image with the unfiltered result
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
                    int distanceShipMoves = (int)ship.shipType + 1; //We start with +1 since we decrement at the start of the 'do' function
                    Point closestReal;
                    do {
                        distanceShipMoves--; //Decrement ship moves so that each time we cant move somewhere, we move one square less
                        PointF closest = selectCache.location.MoveTowards(selected.location, distanceShipMoves + 0.45f); //Calculate the closest point the ship can move too
                        closestReal = new Point((int)(Math.Floor(closest.X)), (int)(Math.Floor(closest.Y)));
                    } while(!CanMove(closestReal.X, closestReal.Y)); //If we cannot move to the square, try again except move one square less
                    if (distanceShipMoves == 0) //Can we not even move one square?
                    {
                        ship.hasMoved = false; //This ship will not move this turn
                        MessageBox.Show("Cannot move directly through land. Please choose a different direction", "Error"); //Notify the user
                        
                        MoveSquare = false; //Reset move settings
                        MoveButton.BackColor = Color.Goldenrod; //Change button colour

                        moveSettings.Visible = false; //Make the move menu invisible
                        selectCache.orange = false; //The last selected square should not be orange
                        OnSquareClick(selected.location.X, selected.location.Y); //Run onsquare click
                        RepaintShipPicture();

                        return; //Exit the function
                    }

                    squares[closestReal.X, closestReal.Y].ships.Add(ship); //Calculate which square this lies in, then move the ship there
                }

                selectCache.ships.Remove(ship); //Remove the ship from the previous swuare
            }
            MoveSquare = false; //Reset move settings
            MoveButton.BackColor = Color.Goldenrod; //Change button colour

            moveSettings.Visible = false; //Make the move menu invisible
            OnSquareClick(selected.location.X, selected.location.Y); //Run onsquare click

            RepaintShipPicture();
        }

        private void RepaintShipPicture()
        {
            foreach (var s in squares)
            {
                if (s.orange)
                {
                    RemoveHighlight(s.location.X, s.location.Y);
                }
                if (s.ships.Count >= 1) //Are there ships in the square?
                {
                    if (!s.red) 
                    {
                        HighlightSquare(s.location.X, s.location.Y, new Filter(100, -100, -100)); //Highlight square with a red filter (increases red by 100, decreases everything else by 100)
                    }

                    if (s.red && s.orange)
                    {
                        HighlightSquare(s.location.X, s.location.Y); //Highlight square with a orange filter
                    }
                    s.red = true;
                }
                else
                {
                    if (s.red)
                    {
                        RemoveHighlight(s.location.X, s.location.Y);
                    }
                    if (s.red && s.orange)
                    {
                        HighlightSquare(s.location.X, s.location.Y); //Highlight square with a orange filter
                    }
                    s.red = false;
                }
                s.orange = false;
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
            MoveSpecificButton.BackColor = Color.AliceBlue; //Change button colours

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
            EndTurn(new object(), new EventArgs()); //Since hasturn = team.none, endturn will start reds turn
        }
        Team AIteam = Team.Green;
        private void EndTurn(object sender, EventArgs e)
        {
            switch (hasTurn)
            {
                case Team.None:
                    hasTurn = Team.Red;
                    break;
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
                    hasTurn = Team.Pirate;
                    PirateMoves();
                    break;
                case Team.Pirate:
                    hasTurn = Team.Red;
                    break;
            } //Change which team will have a turn next
            bool unitsLeft=false;
            foreach (var square in squares)
            {
                foreach (var ship in square.ships) //Iterate through all ships in all squares
                {
                    ship.hasMoved = false; //They all have not moved on this turn, so set their hasmoved variable to false
                    ship.canAttack = true; //On their next turn, all ships should be allowed to attack
                }
                if (square.GetTeam() == hasTurn)
                {
                    unitsLeft = true;
                }
            }
            if (unitsLeft == false) //Has the player been eliminated
            {
                EndTurn(sender, e);
                return; //End the turn automatically and do not bother showing the messagebox

            }
            BuildPortButton.Visible = false;

            MessageBox.Show(hasTurn.ToString() + " teams turn", hasTurn.ToString(), MessageBoxButtons.OK); //Signify which teams turn is next
            OnSquareClick(0,0); //Deselct the previously selected square by the player who had a turn before

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
            LoadCargoMenu.Visible = false;
            ShipCargoPopup.Visible = false;
            MoveSpecificMenu.Visible = false;
            moveSettings.Visible = false;
            //Hide all open panels

            if (hasTurn != Team.Pirate)
            {
                MessageBox.Show(string.Format("Your ports generated you {0} wood, {1} metal, {2} cloth", total.wood, total.metal, total.cloth), "New materials", MessageBoxButtons.OK); //Show data in a messagebox
            }
            teamMaterials.AddMaterials(hasTurn, total); //Add the materials to the teamMaterials

            teamMaterials.Show(hasTurn); //Show the materials on the left side of the screen

            if ((hasTurn & Team.Pirate) != Team.None)
            {
                EndTurn(new object(), new EventArgs());
            }
            if ((hasTurn & AIteam) != Team.None)
            {
                AIMove();
                EndTurn(new object(), new EventArgs());
            }
        }
        public void AIMove()
        {
            foreach (var rule in Rule.rules)
            {
                if (rule.Condition()) //Is the condition of the rule satisfied
                {
                    foreach (var move in rule.Reaction())
                    {
                        move.DoMove();
                    }//Do the required reaction
                }
            }
            RepaintShipPicture();// Repaint the picture
        }
        int iterations = 0;
        private void PirateMoves()
        {
            List<Square> availableSquares = new List<Square>();
            List<Square> pirateSquares = new List<Square>();
            foreach (var s in squares)
            {
                ++iterations;
                if (s.GetTeam() == Team.None && CanMove(s.location.X, s.location.Y))//Are the no ships in the square?
                {
                    availableSquares.Add(s);
                }
                if (s.GetTeam() == Team.Pirate && !s.isPort) //Are there pirate ships in the square. If a pirate ship is in a port it will stay to defend it
                {
                    pirateSquares.Add(s);
                }
            }

            foreach (var square in pirateSquares)
            {
                var ship = square.ships.FirstOrDefault();
                Point destination = Ship.Destinations(ship.shipType, square.location).Where(p => squares[p.X, p.Y].team != Team.Pirate && CanMove(p.X, p.Y)).ToList().RandomItem(); 
                //Select a random destination that does not have pirates in it and has water
                square.ships.Remove(ship);
                var destinationSquare = squares[destination.X, destination.Y];
                destinationSquare.ships.Add(ship);
                RunAttack(destinationSquare);
            }

            Square pirateAddedSquare = availableSquares.RandomItem();
            var shipToAdd = Ship.RandomShip();
            shipToAdd.team = Team.Pirate;
            pirateAddedSquare.ships.Add(shipToAdd);
        }
        private void BuildPortButtonClick(object sender, EventArgs e)
        {
            if (selected == null)
            {
                MessageBox.Show("Please select a square to build a port in", "Error", MessageBoxButtons.OK); //Cannot build port on empty square
            }
            else
            {
                if (selected.isPort || !CanBuildPort(selected.location.X, selected.location.Y))
                {
                    return; //Do not build a port here if there already is one, or if there are no green pixels in the square
                }

                var response = MessageBox.Show("Build level 1 port for 1000 wood?", "Build port", MessageBoxButtons.YesNo);
                if (response == DialogResult.Yes) //Does the user want to build a port?
                {
                    if (selected.GetMaterials().wood >= 1000) //Does the user have enough wood to build the port INSIDE the square
                    {
                        CreatePort(selected.location.X, selected.location.Y, hasTurn); //Create the port at the selected location
                       // pictureBox1.Image = pictureboxBitmap; //Reset the picturebox bitmap
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
            if (shipList.SelectedIndex == -1 || selected.isPort == false || selected.team != hasTurn) //Nothing is selected or we are not at a port or it isn't my square
            {
                shipList.SelectedIndex = -1; //Deselect all items
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
            if (cargoToLoad < teamMaterials.GetMaterials(hasTurn) && cargoToLoad+selectedShip.loaded > new Materials(0,0,0)) //Can we afford it and are we not going to get negative materials
            {
                teamMaterials.Pay(hasTurn, cargoToLoad);
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

        private void button12_Click(object sender, EventArgs e)
        {
            int cost = (int)(2000 * (((float)selected.level) / 2f)); //Casting as float to force decimal math, formula used to slowly increase the price of a port
            var yesno = MessageBox.Show(string.Format("Upgrade port to level {0} for {1} wood?", selected.level+1, cost), "Upgrade port", MessageBoxButtons.YesNo);
            if (yesno == DialogResult.Yes)
            {
                if (teamMaterials.GetMaterials(hasTurn).wood >= cost) 
                {
                    teamMaterials.Pay(hasTurn, new Materials(cost, 0, 0)); //Pay for the port
                    selected.UpgradePort(); //Upgrade the port
                    LevelText.Text = "Level: " + selected.level;
                }
                else
                {
                    MessageBox.Show("Not enough materials");
                }
            }
        }

        private void MainGameForm_Resize(object sender, EventArgs e)
        {
            const int labelHeightSml = 30;
            const int labelHeightMed = 35;
            const int labelHeightBig = 40;
            const int leftColWidthSml = 118;
            const int leftColWidthMed = 135;
            const int leftColWidthBig = 150;
            const int rightColWidthSml = 200;
            const int rightColWidthMed = 250;
            const int rightColWidthBig = 300;

            Font labelFontSml = new Font("Modern No. 20", 12);
            Font labelFontMed = new Font("Modern No. 20", 14);
            Font labelFontBig = new Font("Modern No. 20", 18);

            Font headFontSml = new Font("Modern No. 20", 14);
            Font headFontMed = new Font("Modern No. 20", 18);
            Font headFontBig = new Font("Modern No. 20", 20);


            Font labelFont = labelFontSml;
            Font headFont = headFontSml;
            int labelHeight = labelHeightSml;

            int buttonPad = 37;

            if (this.Width < 1300)
            {
                labelFont = labelFontSml;
                headFont = headFontSml;
                labelHeight = labelHeightSml;

//                buttonPad = 37;

                pictureBox1.Left = leftColWidthSml;
                rightPanel.Width = rightColWidthSml;

            }
            else if (this.Width < 1600)
            {
                labelFont = labelFontMed;
                headFont = headFontMed;
                labelHeight = labelHeightMed;
                pictureBox1.Left = leftColWidthMed;
                rightPanel.Width = rightColWidthMed;
            }
            else 
            {
                labelFont = labelFontBig;
                headFont = headFontBig;
                labelHeight = labelHeightBig;
                pictureBox1.Left = leftColWidthBig;
                rightPanel.Width = rightColWidthBig;
            }
            pictureBox1.Width = this.Width - pictureBox1.Left - rightPanel.Width;
            rightPanel.Left = this.Width - rightPanel.Width;
            shipList.Width = rightPanel.Width - shipList.Left;

            MetalResourceLabel.Top = WoodResourceLabel.Top + labelHeight;
            ClothResourceLabel.Top = WoodResourceLabel.Top + labelHeight * 2;


            label11.Font = headFont;
            WoodResourceLabel.Font = labelFont;
            MetalResourceLabel.Font = labelFont;
            ClothResourceLabel.Font = labelFont;

            Button[] buttons = { button7, button12, BuildPortButton };
            foreach(Control control in rightPanel.Controls)
            {                
                switch (control.Tag)
                {
                    case "shipTypeFont":
                        control.Font = labelFont;
                        control.Left = ManufactureHeavy.Left - control.Width - 5;
                        break;
                    case "labelFont":
                    case "dataFont":
                        control.Font = labelFont;
                        control.Left = 4;
                        break;
                    case "headFont":
                        control.Font = headFont;
                        control.Left = 15;
                        break;
                    case "labelButton":
                        control.Font = labelFont;
                        control.Left = buttonPad;
                        control.Width = rightPanel.Width - buttonPad * 2;
                        break;
                    case "headButton":
                        control.Font = headFont;
                        control.Left = buttonPad;
                        control.Width = rightPanel.Width - buttonPad * 2;
                        break;
                }
            }

            TeamLabel.Left = label10.Left + label10.Width + 5;

       //     foreach (var button in buttons)
       //     {
       //         button.Left = buttonPad;
       //         button.Width = rightPanel.Width - buttonPad * 2;
       //         switch (button.Tag)
       //         {
       //             case "labelFont":
       //                 button.Font = labelFont;
       //                 break;
       //             case "headFont":
       //                 button.Font = headFont;
       //                 break;
       //         }
       //     }
        }
    }
    public struct Filter
    {
        /**
         * A class for storing incremental changes in colour
         * Contains an R, B, and G value which will decrement or increment a different color value
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