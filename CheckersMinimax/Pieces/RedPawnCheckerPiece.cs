using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax.Pieces
{
    class RedPawnCheckerPiece: CheckerPiece, IRedPiece, IJumpable
    {
        public RedPawnCheckerPiece()
        {
            imageSource = "Resources/red60p.png";
        }

        /// <summary>
        /// Gets the possible points for a red pawn checker.
        /// Red pawn checkers can only move up diagnally left or right. This method calls the base class ProcessUpMoves
        /// to generate the up moves.
        /// </summary>
        /// <param name="currentLocation">The current location.</param>
        /// <param name="checkerBoard">The checker board.</param>
        /// <returns></returns>
        public override List<CheckersMove> GetPossiblePoints(CheckersPoint currentLocation, CheckerBoard checkerBoard)
        {
            return base.ProcessUpMoves(currentLocation, checkerBoard);
        }
    }
}
