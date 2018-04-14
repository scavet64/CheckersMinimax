using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax.Pieces
{
    class RedKingCheckerPiece : KingCheckerPiece, IRedPiece
    {
        public RedKingCheckerPiece()
        {
            imageSource = "Resources/red60p_king.png";
        }

        public override object GetMinimaxClone()
        {
            return new RedKingCheckerPiece();
        }

        public override string GetStringRep()
        {
            return "| R |";
        }
    }
}
