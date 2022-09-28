using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesweeperProgrammingProject
{
    class Cell
    {
        // properties
        public int AdjacentBombs { get; set; } 
        public bool IsRevealed { get; set; }
        public bool HasBomb { get; set; }
        public bool IsFlagged { get; set; }

        public Button Button { get; set; }
        

        public int Row { get; set; }
        public int Col { get; set; }


        // constructor
        public Cell (int row, int col, Button b)
        {
            this.Row = row;
            this.Col = col;
            this.Button = b;
        }

        

    }
}
