using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax.Pieces
{
    class KingCheckerPiece : CheckerPiece
    {
        public override List<CheckersPoint> GetPossiblePoints(CheckersPoint currentLocation)
        {
            List<CheckersPoint> list = new List<CheckersPoint>();

            //Can we go down the board?
            if(currentLocation.Row + 1 < 9)
            {
                //Can we move to the right?
                if(currentLocation.Column + 1 < 9)
                {
                    list.Add(new CheckersPoint(currentLocation.Column + 1, currentLocation.Row + 1));
                }

                //Can we move to the left?
                if (currentLocation.Column + 1 >= 0)
                {
                    list.Add(new CheckersPoint(currentLocation.Column - 1, currentLocation.Row + 1));
                }
            }

            //Can we go up the board?
            if(currentLocation.Row - 1 >= 0)
            {
                //can we move to the right?
                if(currentLocation.Column + 1 < 9)
                {
                    list.Add(new CheckersPoint(currentLocation.Column + 1, currentLocation.Row - 1));
                }

                // can we move to the left?
                if(currentLocation.Column - 1 >= 0)
                {
                    list.Add(new CheckersPoint(currentLocation.Column - 1, currentLocation.Row - 1));
                }
            }
            return list;
        }
    }
}
