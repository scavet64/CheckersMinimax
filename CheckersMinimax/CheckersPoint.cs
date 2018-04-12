using CheckersMinimax.Clone;
using CheckersMinimax.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax
{
    public class CheckersPoint: IMinimaxClonable
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public CheckerPiece Checker { get; set; }

        public CheckersPoint() { }

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

        public List<CheckersMove> GetPossibleMoves(CheckerBoard checkerBoard)
        {
            return this.Checker.GetPossibleMoves(this, checkerBoard);
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

        public object GetMinimaxClone()
        {
            CheckersPoint deepClone = new CheckersPoint()
            {
                Checker = (CheckerPiece) this.Checker.GetMinimaxClone(),
                Column = this.Column,
                Row = this.Row
            };

            return deepClone;
        }
    }
}
