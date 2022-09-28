   using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesweeperProgrammingProject
{
    public partial class MinesweeperForm : Form
    {
        //////////////////////////////////////////
        // class constants
        private const int ROWS = 15;
        private const int COLS = 24;
        private const int BUTTON_SIZE = 25;
        private const string BOMB = "\uD83D\uDCA3";
        private const string FLAG = "\uD83D\uDEA9";
        private const int BOMB_PROBABILITY = 3;

        Cell[,] cells = new Cell[ROWS, COLS];


        //////////////////////////////////////////
        // fields and properties
        private int Rows { get; set; }
        private int Cols { get; set; }

        private bool IsInEndState { get; set; }

        private int NumBombs { get; set; }


        //////////////////////////////////////////
        // constructor
        public MinesweeperForm()
        {
            InitializeComponent();
            this.Rows = ROWS;
            this.Cols = COLS;
        }

        

        //////////////////////////////////////////
        // event handlers
        private void MinesweeperForm_Load(object sender, EventArgs e)
        {
            // resize the form
            this.Width = BUTTON_SIZE * this.Cols + this.Cols;
            int titleHeight = this.Height - this.ClientRectangle.Height;
            this.Height = BUTTON_SIZE * this.Rows + this.Rows + titleHeight;

            // create the buttons on the form
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Cols; j++)
                {
                    // create a new button control
                    Button b = new Button();
                    // set the button width and height
                    b.Width = BUTTON_SIZE;
                    b.Height = BUTTON_SIZE;
                    // give the button the color blue
                    b.BackColor = Color.Gainsboro;
                    // position the button on the form
                    b.Top = i * BUTTON_SIZE;
                    b.Left = j * BUTTON_SIZE;
                    // no text
                    b.Text = String.Empty;
                    // set the button style
                    b.FlatStyle = FlatStyle.Popup;
                    // add a MouseDown event handler
                    b.MouseDown += new MouseEventHandler(MinesweeperForm_MouseDown);
                    // give the button a name in "row_col" format 
                    b.Name = i + "_" + j;
                    // add the button control to the form
                    this.Controls.Add(b);

                    // populate the array of Cells
                    cells[i, j] = new Cell(i, j, b);
                }
            }

            // set up the board
            // randomly assign cells bombs
            Random rand = new Random();
            for (int r = 0; r < this.Rows; r++)
            {
                for (int c = 0; c < this.Cols; c++)
                {
                    Cell cell = cells[r, c];
                    int percent = rand.Next(0, 100);

                    if (percent < BOMB_PROBABILITY)
                    {
                        cell.HasBomb = true;
                        this.NumBombs++;
                    }
                }
            }




            // now determine the number of adjacent bombs for each cell
            // Account for corner Cells
            // top Left Corner
            int count = 0;
            if (cells[0, 0].HasBomb)
            {
                // do nothing
            }
            else
            {
                if (cells[0, 1].HasBomb)
                {
                    count++;
                }
                if (cells[1, 0].HasBomb)
                {
                    count++;
                }
                if (cells[1, 1].HasBomb)
                {
                    count++;
                }
                cells[0, 0].AdjacentBombs = count;
            }



            // top Right Corner
            count = 0;
            if (cells[0, this.Cols - 1].HasBomb)
            {
                // do nothing
            }
            else
            {
                if (cells[0, this.Cols - 2].HasBomb)
                {
                    count++;
                }
                if (cells[1, this.Cols - 2].HasBomb)
                {
                    count++;
                }
                if (cells[1, this.Cols - 1].HasBomb)
                {
                    count++;
                }
                cells[0, this.Cols - 1].AdjacentBombs = count;
            }


            // bottom Left Corner
            count = 0;
            if (cells[this.Rows - 1, 0].HasBomb)
            {
                // do nothing
            }
            else
            {
                if (cells[this.Rows - 1, 0].HasBomb)
                {
                    count++;
                }
                if (cells[this.Rows - 2, 1].HasBomb)
                {
                    count++;
                }
                if (cells[this.Rows - 2, 1].HasBomb)
                {
                    count++;
                }
                cells[this.Rows - 1, 0].AdjacentBombs = count;
            }
            

            // bottom Right Corner
            count = 0;
            if (cells[this.Rows - 1, this.Cols - 1].HasBomb)
            {
                // do nothing
            }
            else
            {
                if (cells[this.Rows - 2, this.Cols - 2].HasBomb)
                {
                    count++;
                }
                if (cells[this.Rows - 1, this.Cols - 2].HasBomb)
                {
                    count++;
                }
                if (cells[this.Rows - 2, this.Cols - 1].HasBomb)
                {
                    count++;
                }
                cells[this.Rows - 1, this.Cols - 1].AdjacentBombs = count;
            }
            
             
            
            // assign top row
            for (int i = 1; i < this.Cols - 1; i++)
            {
                count = 0;
                if (cells[0, i].HasBomb)
                {
                    continue;
                }

                if (cells[0, i + 1].HasBomb)
                {
                    count++;
                }
                if (cells[0, i - 1].HasBomb)
                {
                    count++;
                }
                if (cells[1, i + 1].HasBomb)
                {
                    count++;
                }
                if (cells[1, i - 1].HasBomb)
                {
                    count++;
                }
                if (cells[1, i].HasBomb)
                {
                    count++;
                }
                cells[0, i].AdjacentBombs = count;
            }

            
            // assign bottom row
            for (int i = 1; i < this.Cols - 1; i++)
            {
                if (cells[this.Rows - 1, i].HasBomb)
                {
                    continue;
                }
                count = 0;
                if (cells[this.Rows - 1, i + 1].HasBomb)
                {
                    count++;
                }
                if (cells[this.Rows - 1, i - 1].HasBomb)
                {
                    count++;
                }
                if (cells[this.Rows - 2, i + 1].HasBomb)
                {
                    count++;
                }
                if (cells[this.Rows - 2, i - 1].HasBomb)
                {
                    count++;
                }
                if (cells[this.Rows - 2, i].HasBomb)
                {
                    count++;
                }
                cells[this.Rows - 1, i].AdjacentBombs = count;
            }

            // assign left collumn
            for (int i = 1; i < this.Rows - 1; i++)
            {
                if (cells[i, 0].HasBomb)
                {
                    continue;
                }
                count = 0;
                if (cells[i - 1, 0].HasBomb)
                {
                    count++;
                }
                if (cells[i + 1, 0].HasBomb)
                {
                    count++;
                }
                if (cells[i - 1, 1].HasBomb)
                {
                    count++;
                }
                if (cells[i + 1, 1].HasBomb)
                {
                    count++;
                }
                if (cells[i, 1].HasBomb)
                {
                    count++;
                }
                cells[i, 0].AdjacentBombs = count;
            }

            // assign right collumn
            for (int i = 1; i < this.Rows - 1; i++)
            {
                if (cells[i, this.Cols - 1].HasBomb)
                {
                    continue;
                }
                count = 0;
                if (cells[i + 1, this.Cols - 1].HasBomb)
                {
                    count++;
                }
                if (cells[i - 1, this.Cols - 1].HasBomb)
                {
                    count++;
                }
                if (cells[i + 1, this.Cols - 2].HasBomb)
                {
                    count++;
                }
                if (cells[i - 1, this.Cols - 2].HasBomb)
                {
                    count++;
                }
                if (cells[i, this.Cols - 2].HasBomb)
                {
                    count++;
                }
                cells[i, this.Cols - 1].AdjacentBombs = count;
            }

            // do the rest    
            for (int i = 1; i < this.Rows - 1; i++)
            {
                for (int j = 1; j < this.Cols - 1; j++)
                {
                    count = 0;
                    // if cell has bomb, skip it
                    if (cells[i, j].HasBomb)
                    {
                        continue;
                    }


                    // check row above
                    if (cells[i - 1, j - 1].HasBomb)
                    {
                        count++;
                    }
                    if (cells[i - 1, j].HasBomb)
                    {
                        count++;
                    }
                    if (cells[i - 1, j + 1].HasBomb)
                    {
                        count++;
                    }

                    // check row below
                    if (cells[i + 1, j - 1].HasBomb)
                    {
                        count++;
                    }
                    if (cells[i + 1, j].HasBomb)
                    {
                        count++;
                    }
                    if (cells[i + 1, j + 1].HasBomb)
                    {
                        count++;
                    }

                    // check same row
                    if (cells[i, j - 1].HasBomb)
                    {
                        count++;
                    }
                    
                    if (cells[i, j + 1].HasBomb)
                    {
                        count++;
                    }


                    cells[i, j].AdjacentBombs = count;
                }
            }

        }

        

        public bool CelllIsValid(int row, int col)
        {
            return !(row < 0 || col < 0 || row >= this.Rows || col >= this.Cols);
        }

        private void RevealCell(Cell c, Button b)
        {
            if (c.IsRevealed || c.HasBomb)
            {
                // do nothing
            }
            else if (CelllIsValid(c.Row, c.Col))
            {
                c.IsRevealed = true;
                c.Button.BackColor = Color.FromKnownColor(KnownColor.DarkSeaGreen);
                b = c.Button;

                // if cell has no adjacentBombs, reveal adjacent cells
                if (c.AdjacentBombs == 0)
                {
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            int row = c.Row + i;
                            int col = c.Col + j;

                            if (CelllIsValid(row, col))
                            {
                                Cell adjacentC = cells[row, col];
                                RevealCell(adjacentC, adjacentC.Button);

                            }
                        }
                    }
                }
                else
                {
                    b.Text = c.AdjacentBombs.ToString();
                }

                bool unrevealedCellIsFound = false;
                // check if all cells have been revealed
                for (int i = 0; i < this.Rows; i++)
                {
                    for (int j = 0; j < Cols; j++)
                    {
                        Cell cell = cells[i, j];
                        // if a unrevealed cell which isn't a bomb is found, then the game isn't won
                        if (!cell.IsRevealed && !cell.HasBomb)
                        {
                            unrevealedCellIsFound = true;
                            break;
                        }
                    }
                }
                // if you didn't find a non-bomb cell that isn't revealed (i.e., all non-bomb cells are revealed), then the game is won
                if (!unrevealedCellIsFound)
                {
                    TriggerEndState();
                    MessageBox.Show("You Won!");
                }
            }
        }


        private void FlagCell(Cell c, Button b)
        {
            // change image of button to flag
            if (!c.IsFlagged)
            {
                b.Text = FLAG;
            }
            else
            {
                b.Text = "";
            }
            // flag the position as a possible mine
            // or unflag if already flagged
            c.IsFlagged = !c.IsFlagged;
        }

        private void MinesweeperForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender is Button)
            {
                // if the game is in the end state (bomb has been clicked/detonated), don't let the board state change
                if (this.IsInEndState)
                {
                    // do nothing
                }
                else
                {
                    Button b = (Button)sender;
                    // extract the row and column from the button name
                    int index = b.Name.IndexOf("_");
                    int i = int.Parse(b.Name.Substring(0, index));
                    int j = int.Parse(b.Name.Substring(index + 1));

                    Cell c = cells[i, j];

                    // if the cell is already revealed, don't do anything
                    if (c.IsRevealed)
                    {
                        // do nothing
                    }
                    // handle mousebuttons left and right differently
                    if (e.Button == MouseButtons.Left)
                    {
                        if (c.HasBomb)
                        {
                            // GAME OVER
                            b.Text = BOMB;
                            this.TriggerEndState();
                        }
                        else
                        {
                            RevealCell(c, b);
                        }
                    }
                    else
                    {
                        FlagCell(c, b);
                    }
                }
            }
        }

        //////////////////////////////////////////
        // instance methods
        public void TriggerEndState()
        {
            this.IsInEndState = true;

            // reveal all bombs
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j <this.Cols; j++)
                {
                    Cell c = cells[i, j];
                    if (c.HasBomb)
                    {
                        c.Button.Text = BOMB;
                    }
                }
            }

 
            /*
            // reveal other bombs
            for (int i = 0; i < this.Rows - 1; i++)
            {
                for (int j = 0; j < this.Cols - 1; j++)
                {
                    if (cells[i, j].HasBomb)
                    {
                        cells[i, j]
                    }
                }
            }
            */

        }

    }
}
