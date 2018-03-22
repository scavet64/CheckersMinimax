using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax
{
    class CheckersMove
    {
        public CheckerPiece PieceMoving { get; set; }
        public CheckersPoint Destination { get; set; }
    }
}
