using CheckersMinimax.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax
{
    public class CheckersMove
    {
        public CheckersSquareUserControl Source { get; set; }
        public CheckerPiece PieceMoving { get; set; }
        public CheckersSquareUserControl Destination { get; set; }

        public CheckersMove()
        {

        }
    }
}
