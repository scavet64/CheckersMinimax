using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax.Pieces
{
    public static class CheckerPieceFactory
    {
        public static CheckerPiece GetCheckerPiece(CheckerPieceType type)
        {
            switch (type)
            {
                case CheckerPieceType.RedPawn:
                    return new RedPawnCheckerPiece();
                case CheckerPieceType.RedKing:
                    return new RedKingCheckerPiece();
                case CheckerPieceType.BlackPawn:
                    return new BlackPawnCheckerPiece();
                case CheckerPieceType.BlackKing:
                    return new BlackKingCheckerPiece();
                case CheckerPieceType.nullPiece:
                    return new NullCheckerPiece();
                default:
                    throw new ArgumentException("Enum to Checker Piece not defined");
            }
        }
    }
}
