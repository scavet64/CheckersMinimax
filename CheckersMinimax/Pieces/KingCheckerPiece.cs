using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax.Pieces
{
    class KingCheckerPiece : CheckerPiece, IJumpable
    {
        public override List<CheckersPoint> GetPossiblePoints(CheckersPoint currentLocation, CheckerBoard checkerBoard)
        {
            List<CheckersPoint> list = new List<CheckersPoint>();

            //Can we go down the board?
            list.AddRange(base.ProcessDownMoves(currentLocation, checkerBoard));

            //Can we go up the board?
            list.AddRange(base.ProcessUpMoves(currentLocation, checkerBoard));

            return list;
        }
    }
}
