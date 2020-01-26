using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax.Pieces
{
    [Serializable]
    public class BlackPawnCheckerPiece : CheckerPiece, IBlackPiece
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlackPawnCheckerPiece"/> class.
        /// </summary>
        public BlackPawnCheckerPiece()
        {
            imageSource = "Resources/black60p.png";
        }

        /// <summary>
        /// Gets the minimax clone.
        /// </summary>
        /// <returns>
        /// clone to be used for minimax
        /// </returns>
        public override object GetMinimaxClone()
        {
            return new BlackPawnCheckerPiece();
        }

        /// <summary>
        /// Gets the possible moves.
        /// </summary>
        /// <param name="currentLocation">The current location.</param>
        /// <param name="checkerBoard">The checker board.</param>
        /// <returns>
        /// List of possible moves for this piece
        /// </returns>
        public override List<CheckersMove> GetPossibleMoves(CheckersPoint currentLocation, CheckerBoard checkerBoard)
        {
            return ProcessDownMoves(currentLocation, checkerBoard);
        }

        /// <summary>
        /// Gets the string rep.
        /// </summary>
        /// <returns>
        /// String representation used for debugging
        /// </returns>
        public override string GetStringRep()
        {
            return "| b |";
        }
    }
}
