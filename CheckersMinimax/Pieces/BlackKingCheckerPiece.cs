using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax.Pieces
{
    class BlackKingCheckerPiece : KingCheckerPiece, IBlackPiece
    {
        public BlackKingCheckerPiece()
        {
            imageSource = "Resources/black60p_king.png";
        }

        public override object GetMinimaxClone()
        {
            return new BlackKingCheckerPiece();
        }

        public override string GetStringRep()
        {
            return "| B |";
        }
    }
}
