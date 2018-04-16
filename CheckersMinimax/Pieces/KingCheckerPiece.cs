using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax.Pieces
{
    public abstract class KingCheckerPiece : CheckerPiece, IJumpable
    {
        public override List<CheckersMove> GetPossibleMoves(CheckersPoint currentLocation, CheckerBoard checkerBoard)
        {
            List<CheckersMove> list = new List<CheckersMove>();

            //Can we go down the board?
            list.AddRange(ProcessDownMoves(currentLocation, checkerBoard));

            //Can we go up the board?
            list.AddRange(ProcessUpMoves(currentLocation, checkerBoard));

            return list;
        }
    }
}
