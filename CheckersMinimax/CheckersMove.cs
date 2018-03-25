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
        public CheckersPoint SourcePoint { get; set; }
        public CheckersPoint DestinationPoint { get; set; }

        public bool IsJumpMove { get; set; }

        public CheckersMove()
        {

        }

        public CheckersMove(CheckersPoint SourcePoint, CheckersPoint DestinationPoint, bool IsJumpMove)
        {
            this.SourcePoint = SourcePoint;
            this.DestinationPoint = DestinationPoint;
            this.IsJumpMove = IsJumpMove;
        }
    }
}
