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
        public static readonly int MoveLeft = -1;
        public static readonly int MoveRight = 1;
        public static readonly int MoveUp = -1;
        public static readonly int MoveDown = 1;

        //CheckersPoint Location { get; set; }

        protected string imageSource;

        //public string ImageSource
        //{
        //    get { return imageSource; }
        //    set { imageSource = value; }
        //}

        protected CheckerPiece()
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
                list.AddRange(ProcessBoardHorizontal(currentLocation, checkerBoard, rowAbove, MoveUp, MoveRight));

                // can we move to the left?
                list.AddRange(ProcessBoardHorizontal(currentLocation, checkerBoard, rowAbove, MoveUp, MoveLeft));
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
                list.AddRange(ProcessBoardHorizontal(currentLocation, checkerBoard, rowBelow, MoveDown, MoveRight));

                // can we move to the left?
                list.AddRange(ProcessBoardHorizontal(currentLocation, checkerBoard, rowBelow, MoveDown, MoveLeft));
            }

            return ProcessJumpMoves(list);
        }

        private List<CheckersMove> ProcessBoardHorizontal(CheckersPoint currentLocation, CheckerBoard checkerBoard, int oneAdjacentRow, int verticalModifier, int horizontalModifier)
        {
            List<CheckersMove> list = new List<CheckersMove>();
            int adjacentCol = currentLocation.Column + (1 * horizontalModifier);
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
                        int twoAdjacentRow = oneAdjacentRow + (1 * verticalModifier);
                        int twoColAdjacent = adjacentCol + (1 * horizontalModifier);

                        //Check bounds
                        if (twoColAdjacent >= 0 && twoColAdjacent < 8 && twoAdjacentRow >= 0 && twoAdjacentRow < 8)
                        {
                            CheckerPiece possibleCheckerOnPossibleJumpPoint = checkerBoard.BoardArray[twoAdjacentRow][twoColAdjacent].CheckersPoint.Checker;
                            if (possibleCheckerOnPossibleJumpPoint == null || possibleCheckerOnPossibleJumpPoint is NullCheckerPiece)
                            {
                                //we can go here
                                CheckersMove jumpMove = new CheckersMove(currentLocation, new CheckersPoint(twoAdjacentRow, twoColAdjacent), new CheckersPoint(oneAdjacentRow, adjacentCol));


                                //This is a jump move
                                //Get all possible moves for destination point
                                //For each possible move that is a jump move, make a new move and link it

                                //make the move on a temp clone of the board and pass that to find any more multimoves

                                CheckerBoard clonedBoard = (CheckerBoard)checkerBoard.GetMinimaxClone();
                                clonedBoard.MakeMoveOnBoard((CheckersMove)jumpMove.GetMinimaxClone(), false);

                                List<CheckersMove> movesAfterJump = this.GetPossibleMoves(jumpMove.DestinationPoint, clonedBoard);

                                List<CheckersMove> processedList = GetJumpMoves(movesAfterJump);

                                if (processedList.Count > 0)
                                {
                                    foreach(CheckersMove move in processedList)
                                    {
                                        CheckersMove clonedMove = (CheckersMove)jumpMove.GetMinimaxClone();
                                        clonedMove.NextMove = move;
                                        list.Add(clonedMove);
                                    }
                                }
                                else
                                {
                                    list.Add(jumpMove);
                                }
                            }
                        }
                    }
                }
            }

            return list;
        }

        private List<CheckersMove> GetJumpMoves(List<CheckersMove> listToProcess)
        {
            List<CheckersMove> processedList = new List<CheckersMove>();

            foreach (CheckersMove move in listToProcess)
            {
                if (move.JumpedPoint != null)
                {
                    processedList.Add(move);
                }
            }

            return processedList;
        }

        private List<CheckersMove> ProcessJumpMoves(List<CheckersMove> listToProcess)
        {
            List<CheckersMove> processedList = GetJumpMoves(listToProcess);

            if (processedList.Count == 0)
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
