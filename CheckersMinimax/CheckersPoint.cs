using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax
{
    class CheckersPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
        public CheckerPiece Piece { get; set; }

        public CheckersPoint(int X, int Y, CheckerPiece Piece)
        {
            this.X = X;
            this.Y = Y;
            this.Piece = Piece;
        }

        public CheckersPoint(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }
}
