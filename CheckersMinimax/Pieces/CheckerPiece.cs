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

        public abstract List<CheckersMove> GetPossiblePoints(CheckersPoint currentLocation, CheckerBoard checkerBoard);

        public virtual ImageSource BuildCheckerImageSource()
        {
            return new BitmapImage(new Uri(imageSource, UriKind.Relative));
        }

        protected List<CheckersMove> ProcessUpMoves(CheckersPoint currentLocation, CheckerBoard checkerBoard)
        {

            List<CheckersMove> list = new List<CheckersMove>();
            //Can we go up the board?
            int rowAbove = currentLocation.Row - 1;
            if (rowAbove >= 0)
            {
                //can we move to the right?
                list.AddRange(ProcessMoveRight(currentLocation, checkerBoard, rowAbove, true));

                // can we move to the left?
                list.AddRange(ProcessBoardLeft(currentLocation, checkerBoard, rowAbove, true));
            }

            return ProcessJumpMoves(list);
        }

        protected List<CheckersMove> ProcessDownMoves(CheckersPoint currentLocation, CheckerBoard checkerBoard)
        {

            List<CheckersMove> list = new List<CheckersMove>();
            //Can we go up the board?
            int rowBelow = currentLocation.Row + 1;
            if (rowBelow < 9)
            {
                //can we move to the right?
                list.AddRange(ProcessMoveRight(currentLocation, checkerBoard, rowBelow, false));

                // can we move to the left?
                list.AddRange(ProcessBoardLeft(currentLocation, checkerBoard, rowBelow, false));
            }

            return ProcessJumpMoves(list);
        }

        private List<CheckersMove> ProcessMoveRight(CheckersPoint currentLocation, CheckerBoard checkerBoard, int oneAdjacentRow, bool isUp)
        {
            List<CheckersMove> list = new List<CheckersMove>();
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
                        list.Add(new CheckersMove(currentLocation, new CheckersPoint(oneAdjacentRow, rightCol)));
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
                                    list.Add(new CheckersMove(currentLocation, new CheckersPoint(twoAdjacentRow, twoColsRight), new CheckersPoint(oneAdjacentRow, rightCol)));
                                }
                            }
                        }
                    }
                }
            }
            return list;
        }

        private List<CheckersMove> ProcessBoardLeft(CheckersPoint currentLocation, CheckerBoard checkerBoard, int oneAdjacentRow, bool isUp)
        {
            List<CheckersMove> list = new List<CheckersMove>();
            int leftCol = currentLocation.Column - 1;
            if (leftCol >= 0)
            {
                CheckerPiece possibleCheckerOnPossiblePoint = checkerBoard.BoardArray[oneAdjacentRow][leftCol].CheckersPoint.Checker;
                if (possibleCheckerOnPossiblePoint == null || possibleCheckerOnPossiblePoint is NullCheckerPiece)
                {
                    //we can go here
                    list.Add(new CheckersMove(currentLocation, new CheckersPoint(oneAdjacentRow, leftCol)));
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
                                list.Add(new CheckersMove(currentLocation, new CheckersPoint(twoAdjacentRow, twoColLeft), new CheckersPoint(oneAdjacentRow, leftCol)));
                            }
                        }
                    }
                }
            }

            return list;
        }

        private  List<CheckersMove> ProcessJumpMoves(List<CheckersMove> listToProcess)
        {
            List<CheckersMove> processedList = new List<CheckersMove>();

            foreach(CheckersMove move in listToProcess)
            {
                if(move.JumpedPoint != null)
                {
                    processedList.Add(move);
                }
            }

            if(processedList.Count == 0)
            {
                //no jump moves were found so return the unaltered list
                return listToProcess;
            }
            else
            {
                return processedList;
            }
        }
    }
}
