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
    public class CheckersPoint : IMinimaxClonable
    {
        /// <summary>
        /// Gets or sets the row.
        /// </summary>
        /// <value>
        /// The row.
        /// </value>
        public int Row { get; set; }

        /// <summary>
        /// Gets or sets the column.
        /// </summary>
        /// <value>
        /// The column.
        /// </value>
        public int Column { get; set; }

        /// <summary>
        /// Gets or sets the checker piece.
        /// </summary>
        /// <value>
        /// The checker.
        /// </value>
        public CheckerPiece Checker { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckersPoint"/> class.
        /// </summary>
        public CheckersPoint()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckersPoint"/> class.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <param name="checker">The checker.</param>
        public CheckersPoint(int row, int column, CheckerPiece checker)
        {
            Row = row;
            Column = column;
            Checker = checker;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckersPoint"/> class.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <param name="checkerPieceType">Type of the checker piece.</param>
        public CheckersPoint(int row, int column, CheckerPieceType checkerPieceType)
        {
            Row = row;
            Column = column;
            Checker = CheckerPieceFactory.GetCheckerPiece(checkerPieceType);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckersPoint"/> class.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        public CheckersPoint(int row, int column)
        {
            Row = row;
            Column = column;
            Checker = CheckerPieceFactory.GetCheckerPiece(CheckerPieceType.nullPiece);
        }

        /// <summary>
        /// Returns a list of all the possible moves on the passed in board for this particular point
        /// </summary>
        /// <param name="checkerBoard">The checker board to find moves on</param>
        /// <returns>list of all the possible moves on the passed in board for this particular point</returns>
        public List<CheckersMove> GetPossibleMoves(CheckerBoard checkerBoard)
        {
            return Checker.GetPossibleMoves(this, checkerBoard);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if ((obj != null) && (obj is CheckersPoint otherPoint))
            {
                return this.Column == otherPoint.Column && this.Row == otherPoint.Row;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            int hashCode = 13;
            hashCode += Row.GetHashCode();
            hashCode += Column.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Gets the minimax clone. For this class it is essentially a deep copy
        /// </summary>
        /// <returns>Minimax Clone of this class</returns>
        public object GetMinimaxClone()
        {
            CheckersPoint deepClone = new CheckersPoint()
            {
                Checker = (CheckerPiece)this.Checker.GetMinimaxClone(),
                Column = this.Column,
                Row = this.Row
            };

            return deepClone;
        }
    }
}
