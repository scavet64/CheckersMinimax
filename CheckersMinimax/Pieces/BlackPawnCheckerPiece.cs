using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax.Pieces
{
    public class BlackPawnCheckerPiece : CheckerPiece, IBlackPiece, IJumpable
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
            return ProcessDownMoves(currentLocation, checkerBoard);
        }

        public override string GetStringRep()
        {
            return "| b |";
        }
    }
}
