using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax.Pieces
{
    class BlackPawnCheckerPiece : CheckerPiece
    {
        public BlackPawnCheckerPiece()
        {
            imageSource = "Resources/black60p.png";
        }

        public override List<CheckersPoint> GetPossiblePoints(CheckersPoint currentLocation)
        {
            List<CheckersPoint> list = new List<CheckersPoint>();

            //Can we go down the board?
            if (currentLocation.Row + 1 < 9)
            {
                //Can we move to the right?
                if (currentLocation.Column + 1 < 9)
                {
                    list.Add(new CheckersPoint(currentLocation.Row + 1, currentLocation.Column + 1));
                }

                //Can we move to the left?
                if (currentLocation.Column - 1 >= 0)
                {
                    list.Add(new CheckersPoint(currentLocation.Row + 1, currentLocation.Column - 1));
                }
            }

            return list;
        }
    }
}
