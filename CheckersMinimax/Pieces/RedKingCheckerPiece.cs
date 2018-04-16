using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax.Pieces
{
    public class RedKingCheckerPiece : KingCheckerPiece, IRedPiece
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedKingCheckerPiece"/> class.
        /// </summary>
        public RedKingCheckerPiece()
        {
            imageSource = "Resources/red60p_king.png";
        }

        /// <summary>
        /// Gets the minimax clone.
        /// </summary>
        /// <returns>Minimax clone of this object</returns>
        public override object GetMinimaxClone()
        {
            return new RedKingCheckerPiece();
        }

        /// <summary>
        /// Gets the string rep.
        /// </summary>
        /// <returns>String representation</returns>
        public override string GetStringRep()
        {
            return "| R |";
        }
    }
}
