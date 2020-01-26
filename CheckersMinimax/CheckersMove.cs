using CheckersMinimax.Clone;
using CheckersMinimax.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax
{
    [Serializable]
    public class CheckersMove : IMinimaxClonable
    {
        /// <summary>
        /// Gets or sets the source point.
        /// </summary>
        /// <value>
        /// The source point.
        /// </value>
        public CheckersPoint SourcePoint { get; set; }

        /// <summary>
        /// Gets or sets the destination point.
        /// </summary>
        /// <value>
        /// The destination point.
        /// </value>
        public CheckersPoint DestinationPoint { get; set; }

        /// <summary>
        /// Gets or sets the jumped point.
        /// </summary>
        /// <value>
        /// The jumped point.
        /// </value>
        public CheckersPoint JumpedPoint { get; set; }

        /// <summary>
        /// Gets or sets the next move.
        /// </summary>
        /// <value>
        /// The next move.
        /// </value>
        public CheckersMove NextMove { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckersMove"/> class.
        /// </summary>
        public CheckersMove()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckersMove"/> class.
        /// </summary>
        /// <param name="sourcePoint">The source point.</param>
        /// <param name="destinationPoint">The destination point.</param>
        public CheckersMove(CheckersPoint sourcePoint, CheckersPoint destinationPoint)
        {
            SourcePoint = sourcePoint;
            DestinationPoint = destinationPoint;
            JumpedPoint = null;
            NextMove = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckersMove"/> class.
        /// </summary>
        /// <param name="sourcePoint">The source point.</param>
        /// <param name="destinationPoint">The destination point.</param>
        /// <param name="jumpedPoint">The jumped point.</param>
        public CheckersMove(CheckersPoint sourcePoint, CheckersPoint destinationPoint, CheckersPoint jumpedPoint)
        {
            SourcePoint = sourcePoint;
            DestinationPoint = destinationPoint;
            JumpedPoint = jumpedPoint;
            NextMove = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckersMove"/> class.
        /// </summary>
        /// <param name="sourcePoint">The source point.</param>
        /// <param name="destinationPoint">The destination point.</param>
        /// <param name="jumpedPoint">The jumped point.</param>
        /// <param name="nextMove">The next move.</param>
        public CheckersMove(CheckersPoint sourcePoint, CheckersPoint destinationPoint, CheckersPoint jumpedPoint, CheckersMove nextMove)
        {
            SourcePoint = sourcePoint;
            DestinationPoint = destinationPoint;
            JumpedPoint = jumpedPoint;
            NextMove = nextMove;
        }

        /// <summary>
        /// Gets the minimax clone.
        /// </summary>
        /// <returns>Minimax clone of this object</returns>
        public object GetMinimaxClone()
        {
            CheckersMove clone = new CheckersMove();

            if (this.DestinationPoint != null)
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
