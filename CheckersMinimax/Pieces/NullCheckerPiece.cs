using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CheckersMinimax.Pieces
{
    [Serializable]
    public class NullCheckerPiece : CheckerPiece
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullCheckerPiece"/> class.
        /// </summary>
        public NullCheckerPiece()
        {
            imageSource = null;
        }

        /// <summary>
        /// Builds the checker image source.
        /// </summary>
        /// <returns>Image source object</returns>
        public override ImageSource BuildCheckerImageSource()
        {
            return null;
        }

        /// <summary>
        /// Gets the minimax clone.
        /// </summary>
        /// <returns>clone to be used for minimax</returns>
        public override object GetMinimaxClone()
        {
            return new NullCheckerPiece();
        }

        /// <summary>
        /// Gets the possible moves. Null Checker Piece has no moves
        /// </summary>
        /// <param name="currentLocation">The current location.</param>
        /// <param name="checkerBoard">The checker board.</param>
        /// <returns>List of possible moves for this checkerpiece</returns>
        public override List<CheckersMove> GetPossibleMoves(CheckersPoint currentLocation, CheckerBoard checkerBoard)
        {
            return new List<CheckersMove>();
        }

        /// <summary>
        /// Gets the string rep.
        /// </summary>
        /// <returns>String representation</returns>
        public override string GetStringRep()
        {
            return "|   |";
        }
    }
}
