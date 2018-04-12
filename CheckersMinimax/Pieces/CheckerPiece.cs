using CheckersMinimax.Clone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CheckersMinimax.Pieces
{
    public abstract class CheckerPiece: IMinimaxClonable
    {
        public static readonly bool MoveLeft = true;
        public static readonly bool MoveRight = false;

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

        public abstract List<CheckersMove> GetPossibleMoves(CheckersPoint currentLocation, CheckerBoard checkerBoard);

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
                list.AddRange(ProcessBoardHorizontal(currentLocation, checkerBoard, rowAbove, true, MoveRight));

                // can we move to the left?
                list.AddRange(ProcessBoardHorizontal(currentLocation, checkerBoard, rowAbove, true, MoveLeft));
            }

            return ProcessJumpMoves(list);
        }

        protected List<CheckersMove> ProcessDownMoves(CheckersPoint currentLocation, CheckerBoard checkerBoard)
        {

            List<CheckersMove> list = new List<CheckersMove>();
            //Can we go up the board?
            int rowBelow = currentLocation.Row + 1;
            if (rowBelow < 8)
            {
                //can we move to the right?
                list.AddRange(ProcessBoardHorizontal(currentLocation, checkerBoard, rowBelow, false, MoveRight));

                // can we move to the left?
                list.AddRange(ProcessBoardHorizontal(currentLocation, checkerBoard, rowBelow, false, MoveLeft));
            }

            return ProcessJumpMoves(list);
        }

        private List<CheckersMove> ProcessBoardHorizontal(CheckersPoint currentLocation, CheckerBoard checkerBoard, int oneAdjacentRow, bool isUp, bool isLeft)
        {
            List<CheckersMove> list = new List<CheckersMove>();
            int adjacentCol = -1;
            if (isLeft)
            {
                adjacentCol = currentLocation.Column - 1;
            }
            else
            {
                adjacentCol = currentLocation.Column + 1;
            }
            //Check our bounds
            if (adjacentCol >= 0 && adjacentCol < 8)
            {
                CheckerPiece possibleCheckerOnPossiblePoint = checkerBoard.BoardArray[oneAdjacentRow][adjacentCol].CheckersPoint.Checker;
                if (possibleCheckerOnPossiblePoint == null || possibleCheckerOnPossiblePoint is NullCheckerPiece)
                {
                    //we can go here
                    list.Add(new CheckersMove(currentLocation, new CheckersPoint(oneAdjacentRow, adjacentCol)));
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

                        int twoColAdjacent = -1;
                        if (isLeft)
                        {
                            twoColAdjacent = adjacentCol - 1;
                        }
                        else
                        {
                            twoColAdjacent = adjacentCol + 1;
                        }

                        //Check bounds
                        if (twoColAdjacent >= 0 && twoColAdjacent < 8 && twoAdjacentRow >= 0 && twoAdjacentRow < 8)
                        {
                            CheckerPiece possibleCheckerOnPossibleJumpPoint = checkerBoard.BoardArray[twoAdjacentRow][twoColAdjacent].CheckersPoint.Checker;
                            if (possibleCheckerOnPossibleJumpPoint == null || possibleCheckerOnPossibleJumpPoint is NullCheckerPiece)
                            {
                                //we can go here
                                list.Add(new CheckersMove(currentLocation, new CheckersPoint(twoAdjacentRow, twoColAdjacent), new CheckersPoint(oneAdjacentRow, adjacentCol)));
                            }
                        }
                    }
                }
            }

            return list;
        }

        private List<CheckersMove> ProcessJumpMoves(List<CheckersMove> listToProcess)
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

        public abstract object GetMinimaxClone();
    }
}
