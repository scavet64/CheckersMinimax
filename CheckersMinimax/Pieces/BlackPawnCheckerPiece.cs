using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax.Pieces
{
    class BlackPawnCheckerPiece : CheckerPiece, IBlackPiece, IJumpable
    {
        public BlackPawnCheckerPiece()
        {
            imageSource = "Resources/black60p.png";
        }

        public override object GetMinimaxClone()
        {
            return new BlackPawnCheckerPiece();
        }

        public override List<CheckersMove> GetPossibleMoves(CheckersPoint currentLocation, CheckerBoard checkerBoard)
        {
            return base.ProcessDownMoves(currentLocation, checkerBoard);
        }

        public override string GetStringRep()
        {
            return "| b |";
        }
    }
}
