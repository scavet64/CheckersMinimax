using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax.Pieces
{
    [Serializable]
    public class BlackKingCheckerPiece : KingCheckerPiece, IBlackPiece
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlackKingCheckerPiece"/> class.
        /// </summary>
        public BlackKingCheckerPiece()
        {
            imageSource = "Resources/black60p_king.png";
        }

        /// <summary>
        /// Gets the minimax clone.
        /// </summary>
        /// <returns>
        /// clone to be used for minimax
        /// </returns>
        public override object GetMinimaxClone()
        {
            return new BlackKingCheckerPiece();
        }

        /// <summary>
        /// Gets the string rep.
        /// </summary>
        /// <returns>
        /// String representation used for debugging
        /// </returns>
        public override string GetStringRep()
        {
            return "| B |";
        }
    }
}
