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

        public override List<CheckersPoint> GetPossiblePoints(CheckersPoint currentLocation, CheckerBoard checkerBoard)
        {
            return base.ProcessDownMoves(currentLocation, checkerBoard);
        }
    }
}
