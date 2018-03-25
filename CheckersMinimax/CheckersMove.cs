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
        public CheckersPoint JumpedPoint { get; set; }

        public CheckersMove() { }

        public CheckersMove(CheckersPoint SourcePoint, CheckersPoint DestinationPoint)
        {
            this.SourcePoint = SourcePoint;
            this.DestinationPoint = DestinationPoint;
            this.JumpedPoint = null;
        }

        public CheckersMove(CheckersPoint SourcePoint, CheckersPoint DestinationPoint, CheckersPoint JumpedPoint)
        {
            this.SourcePoint = SourcePoint;
            this.DestinationPoint = DestinationPoint;
            this.JumpedPoint = JumpedPoint;
        }
    }
}
