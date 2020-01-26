using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax.Pieces
{
    [Serializable]
    public class RedPawnCheckerPiece : CheckerPiece, IRedPiece
    {
        public RedPawnCheckerPiece()
        {
            imageSource = "Resources/red60p.png";
        }

        /// <summary>
        /// Gets the minimax clone.
        /// </summary>
        /// <returns>Clone to be used for minimax</returns>
        public override object GetMinimaxClone()
        {
            return new RedPawnCheckerPiece();
        }

        /// <summary>
        /// Gets the possible points for a red pawn checker.
        /// Red pawn checkers can only move up diagnally left or right. This method calls the base class ProcessUpMoves
        /// to generate the up moves.
        /// </summary>
        /// <param name="currentLocation">The current location.</param>
        /// <param name="checkerBoard">The checker board.</param>
        /// <returns>List of possible moves</returns>
        public override List<CheckersMove> GetPossibleMoves(CheckersPoint currentLocation, CheckerBoard checkerBoard)
        {
            return ProcessUpMoves(currentLocation, checkerBoard);
        }

        /// <summary>
        /// Gets the string representation.
        /// </summary>
        /// <returns>String representation</returns>
        public override string GetStringRep()
        {
            return "| r |";
        }
    }
}