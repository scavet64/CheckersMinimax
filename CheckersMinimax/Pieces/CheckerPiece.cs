using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CheckersMinimax.Pieces
{
    public abstract class CheckerPiece
    {
        //CheckersPoint Location { get; set; }

        protected string imageSource;

        //public string ImageSource
        //{
        //    get { return imageSource; }
        //    set { imageSource = value; }
        //}

        public CheckerPiece()
        {
            //this.Location = location;
        }

        public virtual ImageSource BuildCheckerImageSource()
        {
            return new BitmapImage(new Uri(imageSource, UriKind.Relative));
        }

        public abstract List<CheckersPoint> GetPossiblePoints(CheckersPoint currentLocation, CheckerBoard checkerBoard);

        protected List<CheckersPoint> ProcessUpMoves(CheckersPoint currentLocation, CheckerBoard checkerBoard)
        {

            List<CheckersPoint> list = new List<CheckersPoint>();
            //Can we go up the board?
            int rowAbove = currentLocation.Row - 1;
            if (rowAbove >= 0)
            {
                //can we move to the right?
                list.AddRange(ProcessMoveRight(currentLocation, checkerBoard, rowAbove, true));

                // can we move to the left?
                list.AddRange(ProcessBoardLeft(currentLocation, checkerBoard, rowAbove, true));
            }

            return list;
        }

        protected List<CheckersPoint> ProcessDownMoves(CheckersPoint currentLocation, CheckerBoard checkerBoard)
        {

            List<CheckersPoint> list = new List<CheckersPoint>();
            //Can we go up the board?
            int rowBelow = currentLocation.Row + 1;
            if (rowBelow < 9)
            {
                //can we move to the right?
                list.AddRange(ProcessMoveRight(currentLocation, checkerBoard, rowBelow, false));

                // can we move to the left?
                list.AddRange(ProcessBoardLeft(currentLocation, checkerBoard, rowBelow, false));
            }

            return list;
        }

        private List<CheckersPoint> ProcessMoveRight(CheckersPoint currentLocation, CheckerBoard checkerBoard, int oneAdjacentRow, bool isUp)
        {
            List<CheckersPoint> list = new List<CheckersPoint>();
            if (oneAdjacentRow >= 0)
            {
                //can we move to the right?
                int rightCol = currentLocation.Column + 1;
                if (rightCol < 8)
                {
                    CheckerPiece possibleCheckerOnPossiblePoint = checkerBoard.BoardArray[oneAdjacentRow][rightCol].CheckersPoint.Checker;
                    if (possibleCheckerOnPossiblePoint == null || possibleCheckerOnPossiblePoint is NullCheckerPiece)
                    {
                        //we can go here
                        list.Add(new CheckersPoint(oneAdjacentRow, rightCol));
                    }
                    else
                    {
                        //can we jump this guy?

                        if ((possibleCheckerOnPossiblePoint is IRedPiece && this is IBlackPiece) ||
                            possibleCheckerOnPossiblePoint is IBlackPiece && this is IRedPiece)
                        {
                            int twoAdjacentRow;
                            if (isUp)
                            {
                                //go another row up 
                                twoAdjacentRow = oneAdjacentRow - 1;
                            }
                            else
                            {
                                //go another row down
                                twoAdjacentRow = oneAdjacentRow + 1;
                            }

                            //another column to the right
                            int twoColsRight = rightCol + 1;

                            //Check bounds
                            if (twoColsRight < 8 && twoAdjacentRow >= 0)
                            {
                                CheckerPiece possibleCheckerOnPossibleJumpPoint = checkerBoard.BoardArray[twoAdjacentRow][twoColsRight].CheckersPoint.Checker;
                                if (possibleCheckerOnPossibleJumpPoint == null || possibleCheckerOnPossibleJumpPoint is NullCheckerPiece)
                                {
                                    //we can go here
                                    list.Add(new CheckersPoint(twoAdjacentRow, twoColsRight));
                                }
                            }
                        }
                    }
                }
            }
            return list;
        }

        private List<CheckersPoint> ProcessBoardLeft(CheckersPoint currentLocation, CheckerBoard checkerBoard, int oneAdjacentRow, bool isUp)
        {
            List<CheckersPoint> list = new List<CheckersPoint>();
            int leftCol = currentLocation.Column - 1;
            if (leftCol >= 0)
            {
                CheckerPiece possibleCheckerOnPossiblePoint = checkerBoard.BoardArray[oneAdjacentRow][leftCol].CheckersPoint.Checker;
                if (possibleCheckerOnPossiblePoint == null || possibleCheckerOnPossiblePoint is NullCheckerPiece)
                {
                    //we can go here
                    list.Add(new CheckersPoint(oneAdjacentRow, leftCol));
                }
                else
                {
                    //can we jump this guy?
                    if ((possibleCheckerOnPossiblePoint is IRedPiece && this is IBlackPiece) ||
                            possibleCheckerOnPossiblePoint is IBlackPiece && this is IRedPiece)
                    {
                        //go another row up and another column to the right
                        int twoAdjacentRow;
                        if (isUp)
                        {
                            //go another row up 
                            twoAdjacentRow = oneAdjacentRow - 1;
                        }
                        else
                        {
                            //go another row down
                            twoAdjacentRow = oneAdjacentRow + 1;
                        }
                        int twoColLeft = leftCol - 1;

                        //Check bounds
                        if (twoColLeft < 8 && twoAdjacentRow >= 0)
                        {
                            CheckerPiece possibleCheckerOnPossibleJumpPoint = checkerBoard.BoardArray[twoAdjacentRow][twoColLeft].CheckersPoint.Checker;
                            if (possibleCheckerOnPossibleJumpPoint == null || possibleCheckerOnPossibleJumpPoint is NullCheckerPiece)
                            {
                                //we can go here
                                list.Add(new CheckersPoint(twoAdjacentRow, twoColLeft));
                            }
                        }
                    }
                }
            }

            return list;
        }
    }
}
