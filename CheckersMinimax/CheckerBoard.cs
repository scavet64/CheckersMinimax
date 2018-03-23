using CheckersMinimax.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace CheckersMinimax
{
    public class CheckerBoard
    {
        private List<List<CheckersSquareUserControl>> boardArray = new List<List<CheckersSquareUserControl>>();

        public List<List<CheckersSquareUserControl>> BoardArray
        {
            get { return boardArray; }
            set { boardArray = value; }
        }


        public void MakeBoard(RoutedEventHandler routedEventHandler)
        {
            int count = 0;
            for (int Row = 0; Row < 8; Row++)
            {
                boardArray.Add(new List<CheckersSquareUserControl>());
                for (int Column = 0; Column < 8; Column++)
                {
                    CheckersSquareUserControl checkerSquareUC;
                    if (Row % 2 == 0)
                    {
                        if (Column % 2 == 0)
                        {
                            checkerSquareUC = new CheckersSquareUserControl(Brushes.White, new CheckersPoint(Row, Column, CheckerPieceType.nullp), routedEventHandler);
                        }
                        else
                        {
                            if (Row < 3)
                            {
                                checkerSquareUC = new CheckersSquareUserControl(Brushes.Black, new CheckersPoint(Row, Column, CheckerPieceType.BlackPawn), routedEventHandler);
                            }
                            else if (Row > 4)
                            {
                                checkerSquareUC = new CheckersSquareUserControl(Brushes.Black, new CheckersPoint(Row, Column, CheckerPieceType.RedPawn), routedEventHandler);
                            }
                            else
                            {
                                checkerSquareUC = new CheckersSquareUserControl(Brushes.Black, new CheckersPoint(Row, Column, CheckerPieceType.nullp), routedEventHandler);
                            }
                        }
                    }
                    else
                    {
                        if (Column % 2 == 0)
                        {
                            if (Row < 3)
                            {
                                checkerSquareUC = new CheckersSquareUserControl(Brushes.Black, new CheckersPoint(Row, Column, CheckerPieceType.BlackPawn), routedEventHandler);
                            }
                            else if (Row > 4)
                            {
                                checkerSquareUC = new CheckersSquareUserControl(Brushes.Black, new CheckersPoint(Row, Column, CheckerPieceType.RedPawn), routedEventHandler);
                            }
                            else
                            {
                                //empty middle spot
                                checkerSquareUC = new CheckersSquareUserControl(Brushes.Black, new CheckersPoint(Row, Column, CheckerPieceType.nullp), routedEventHandler);
                            }
                        }
                        else
                        {
                            checkerSquareUC = new CheckersSquareUserControl(Brushes.White, new CheckersPoint(Row, Column, CheckerPieceType.nullp), routedEventHandler);
                        }

                    }
                    count++;
                    boardArray[Row].Add(checkerSquareUC);
                }
            }
        }

        //public List<CheckersMove> getAvaliableMoves()
        //{
        //    foreach(List<CheckersSquareUserControl> rowsOfCheckerSquares in boardArray)
        //    {
        //        foreach(CheckersSquareUserControl checkerSquareUserControl in rowsOfCheckerSquares)
        //        {
        //            List<CheckersMove> list = new List<CheckersMove>();

        //        }
        //    }
        //}

        public List<CheckersPoint> getAvailablePointsForSquare(CheckersPoint checkersPoint)
        {
            if (checkersPoint.Checker == null)
            {
                return null;
            }

            return checkersPoint.GetPotentialPointsForMove();
        }
    }
}
