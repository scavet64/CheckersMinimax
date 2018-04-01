using CheckersMinimax.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax
{
    public class CheckersPoint
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public CheckerPiece Checker { get; set; }

        public CheckersPoint(int Row, int Column, CheckerPiece Checker)
        {
            this.Row = Row;
            this.Column = Column;
            this.Checker = Checker;
        }

        public CheckersPoint(int Row, int Column, CheckerPieceType checkerPieceType)
        {
            this.Row = Row;
            this.Column = Column;
            this.Checker = CheckerPieceFactory.GetCheckerPiece(checkerPieceType);
        }

        public CheckersPoint(int Row, int Column)
        {
            this.Row = Row;
            this.Column = Column;
            this.Checker = CheckerPieceFactory.GetCheckerPiece(CheckerPieceType.nullPiece);
        }

        public List<CheckersMove> GetPotentialPointsForMove(CheckerBoard checkerBoard)
        {
            return this.Checker.GetPossiblePoints(this, checkerBoard);
        }

        // override object.Equals
        public override bool Equals(object obj)
        {

            if ((obj != null) && (obj is CheckersPoint otherPoint))
            {
                return this.Column == otherPoint.Column && this.Row == otherPoint.Row;
            }
            else
            {
                return false;
            }
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            int hashCode = 13;
            hashCode += Row.GetHashCode();
            hashCode += Column.GetHashCode();
            return hashCode;
        }
    }
}
