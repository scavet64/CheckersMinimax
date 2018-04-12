using CheckersMinimax.Clone;
using CheckersMinimax.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax
{
    public class CheckersMove: IMinimaxClonable
    {
        public CheckersPoint SourcePoint { get; set; }
        public CheckersPoint DestinationPoint { get; set; }
        public CheckersPoint JumpedPoint { get; set; }
        public CheckersMove NextMove { get; set; }
         

        public CheckersMove() { }

        public CheckersMove(CheckersPoint SourcePoint, CheckersPoint DestinationPoint)
        {
            this.SourcePoint = SourcePoint;
            this.DestinationPoint = DestinationPoint;
            this.JumpedPoint = null;
            this.NextMove = null;
        }

        public CheckersMove(CheckersPoint SourcePoint, CheckersPoint DestinationPoint, CheckersPoint JumpedPoint)
        {
            this.SourcePoint = SourcePoint;
            this.DestinationPoint = DestinationPoint;
            this.JumpedPoint = JumpedPoint;
            this.NextMove = null;
        }

        public CheckersMove(CheckersPoint SourcePoint, CheckersPoint DestinationPoint, CheckersPoint JumpedPoint, CheckersMove NextMove)
        {
            this.SourcePoint = SourcePoint;
            this.DestinationPoint = DestinationPoint;
            this.JumpedPoint = JumpedPoint;
            this.NextMove = NextMove;
        }

        public object GetMinimaxClone()
        {
            CheckersMove clone = new CheckersMove();

            if(this.DestinationPoint != null)
            {
                clone.DestinationPoint = (CheckersPoint)this.DestinationPoint.GetMinimaxClone();
            }

            if (this.SourcePoint != null)
            {
                clone.SourcePoint = (CheckersPoint)this.SourcePoint.GetMinimaxClone();
            }

            if (this.JumpedPoint != null)
            {
                clone.JumpedPoint = (CheckersPoint)this.JumpedPoint.GetMinimaxClone();
            }

            if (this.NextMove != null)
            {
                clone.NextMove = (CheckersMove)this.NextMove.GetMinimaxClone();
            }

            return clone;
        }
    }
}
