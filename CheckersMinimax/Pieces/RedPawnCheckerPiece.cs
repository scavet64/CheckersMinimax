using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax.Pieces
{
    class RedPawnCheckerPiece: CheckerPiece, IRedPiece, IJumpable
    {
        public RedPawnCheckerPiece()
        {
            imageSource = "Resources/red60p.png";
        }

        public override List<CheckersPoint> GetPossiblePoints(CheckersPoint currentLocation, CheckerBoard checkerBoard)
        {
            List<CheckersPoint> list = new List<CheckersPoint>();

            //Can we go up the board?
            int rowAbove = currentLocation.Row - 1;
            if (rowAbove >= 0)
            {
                //can we move to the right?
                int rightCol = currentLocation.Column + 1;
                if (rightCol < 8)
                {
                    CheckerPiece possibleCheckerOnPossiblePoint = checkerBoard.BoardArray[rowAbove][rightCol].CheckersPoint.Checker;
                    if(possibleCheckerOnPossiblePoint == null || possibleCheckerOnPossiblePoint is NullCheckerPiece)
                    {
                        //we can go here
                        list.Add(new CheckersPoint(rowAbove, rightCol));
                    }
                    else
                    {
                        //can we jump this guy?
                        if (!(possibleCheckerOnPossiblePoint is IRedPiece))
                        {
                            //go another row up and another column to the right
                            int twoRowsUp = rowAbove - 1;
                            int twoColsRight = rightCol + 1;

                            //Check bounds
                            if (twoColsRight < 8 && twoRowsUp >= 0)
                            {
                                CheckerPiece possibleCheckerOnPossibleJumpPoint = checkerBoard.BoardArray[twoRowsUp][twoColsRight].CheckersPoint.Checker;
                                if (possibleCheckerOnPossibleJumpPoint == null || possibleCheckerOnPossibleJumpPoint is NullCheckerPiece)
                                {
                                    //we can go here
                                    list.Add(new CheckersPoint(twoRowsUp, twoColsRight));
                                }
                            }
                        }
                    }
                }

                // can we move to the left?
                int leftCol = currentLocation.Column - 1;
                if (leftCol >= 0)
                {
                    CheckerPiece possibleCheckerOnPossiblePoint = checkerBoard.BoardArray[rowAbove][leftCol].CheckersPoint.Checker;
                    if (possibleCheckerOnPossiblePoint == null || possibleCheckerOnPossiblePoint is NullCheckerPiece)
                    {
                        //we can go here
                        list.Add(new CheckersPoint(rowAbove, leftCol));
                    }
                    else
                    {
                        //can we jump this guy?
                        if (!(possibleCheckerOnPossiblePoint is IRedPiece))
                        {
                            //go another row up and another column to the right
                            int twoRowsUp = rowAbove - 1;
                            int twoColLeft = leftCol - 1;

                            //Check bounds
                            if (twoColLeft < 8 && twoRowsUp >= 0)
                            {
                                CheckerPiece possibleCheckerOnPossibleJumpPoint = checkerBoard.BoardArray[twoRowsUp][twoColLeft].CheckersPoint.Checker;
                                if (possibleCheckerOnPossibleJumpPoint == null || possibleCheckerOnPossibleJumpPoint is NullCheckerPiece)
                                {
                                    //we can go here
                                    list.Add(new CheckersPoint(twoRowsUp, twoColLeft));
                                }
                            }
                        }
                    }
                }
            }
            return list;
        }
    }
}
