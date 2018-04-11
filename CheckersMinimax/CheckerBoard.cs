using CheckersMinimax.AI;
using CheckersMinimax.Clone;
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
    [Serializable]
    /// <summary>
    /// [Row][Column]
    /// </summary>
    public class CheckerBoard: IMinimaxClonable
    {
        public static int idCounter = 0;
        public int id;
        private List<List<CheckersSquareUserControl>> boardArray = new List<List<CheckersSquareUserControl>>();
        public PlayerColor CurrentPlayerTurn { get; set; } = PlayerColor.Red;

        public CheckerBoard()
        {
            id = idCounter++;
        }

        /// <summary>
        /// [Row][Column]
        /// </summary>
        /// <value>
        /// The board array.
        /// </value>
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
                            checkerSquareUC = new CheckersSquareUserControl(Brushes.White, new CheckersPoint(Row, Column, CheckerPieceType.nullPiece), routedEventHandler);
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
                                checkerSquareUC = new CheckersSquareUserControl(Brushes.Black, new CheckersPoint(Row, Column, CheckerPieceType.nullPiece), routedEventHandler);
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
                                checkerSquareUC = new CheckersSquareUserControl(Brushes.Black, new CheckersPoint(Row, Column, CheckerPieceType.nullPiece), routedEventHandler);
                            }
                        }
                        else
                        {
                            checkerSquareUC = new CheckersSquareUserControl(Brushes.White, new CheckersPoint(Row, Column, CheckerPieceType.nullPiece), routedEventHandler);
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

        public List<CheckersMove> getAvailablePointsForSquare(CheckersPoint checkersPoint)
        {
            if (checkersPoint.Checker == null)
            {
                return null;
            }

            return checkersPoint.GetPotentialPointsForMove(this);
        }

        public object GetWinner()
        {
            List<CheckersPoint> redCheckerPoints = GetPointsForColor<IRedPiece>();
            List<CheckersPoint> blackCheckerPoints = GetPointsForColor<IBlackPiece>();

            if (blackCheckerPoints.Count == 0)
            {
                return PlayerColor.Red;
            }
            else if (redCheckerPoints.Count == 0)
            {
                return PlayerColor.Black;
            }
            else
            {
                return null;
            }
        }

        public List<CheckersPoint> GetPointsForColor<ColorInterface>()
        {
            List<CheckersPoint> listOfColor = new List<CheckersPoint>();

            foreach (List<CheckersSquareUserControl> list in BoardArray)
            {
                foreach (CheckersSquareUserControl squareUC in list)
                {
                    if (squareUC.CheckersPoint.Checker != null)
                    {
                        if (squareUC.CheckersPoint.Checker is ColorInterface)
                        {
                            listOfColor.Add(squareUC.CheckersPoint);
                        }
                    }
                }
            }

            return listOfColor;
        }

        public int ScoreA()
        {
            int score = 0;
            object winningColor = this.GetWinner();

            if (winningColor != null)
            {
                score = int.MaxValue;
            }
            else
            {
                foreach (CheckersPoint point in GetPointsForColor<IRedPiece>())
                {
                    score += point.Checker is KingCheckerPiece ? AIController.KING_WORTH : AIController.PAWN_WORTH;
                }
                foreach (CheckersPoint point in GetPointsForColor<IBlackPiece>())
                {
                    score -= point.Checker is KingCheckerPiece ? AIController.KING_WORTH : AIController.PAWN_WORTH;
                }
            }
            return score;
        }

        public void SwapTurns()
        {
            if (CurrentPlayerTurn == PlayerColor.Red)
            {
                CurrentPlayerTurn = PlayerColor.Black;
            }
            else
            {
                CurrentPlayerTurn = PlayerColor.Red;
            }
        }

        public List<CheckersMove> GetMovesForPlayer(PlayerColor currentPlayerTurn)
        {
            List<CheckersPoint> playersPoints = null;

            if (currentPlayerTurn == PlayerColor.Red)
            {
                playersPoints = GetPointsForColor<IRedPiece>();
            }
            else if(currentPlayerTurn == PlayerColor.Black)
            {
                playersPoints = GetPointsForColor<IBlackPiece>();
            }
            else
            {
                throw new ArgumentException("Unknown Player Color");
            }

            List<CheckersMove> allAvailableMoves = new List<CheckersMove>();
            foreach (CheckersPoint checkerPoint in playersPoints)
            {
                allAvailableMoves.AddRange(checkerPoint.GetPotentialPointsForMove(this));
            }

            //If we have jump moves, filter out non jumps
            List<CheckersMove> jumpMoves = new List<CheckersMove>();
            foreach (CheckersMove move in allAvailableMoves)
            {
                if(move.JumpedPoint != null)
                {
                    jumpMoves.Add(move);
                }
            }

            if(jumpMoves.Count > 0)
            {
                return jumpMoves;
            }
            else
            {
                return allAvailableMoves;
            }
        }

        public void MakeMoveOnBoard(CheckersMove moveToMake)
        {
            CheckersPoint source = moveToMake.SourcePoint;
            CheckersPoint destination = moveToMake.DestinationPoint;

            Console.WriteLine("Piece1 " + source.Row + ", " + source.Column);
            Console.WriteLine("Piece2 " + destination.Row + ", " + destination.Column);

            //was this a cancel?
            if (source != destination)
            {
                destination.Checker = source.Checker;
                source.Checker = CheckerPieceFactory.GetCheckerPiece(CheckerPieceType.nullPiece);

                //was this a jump move?
                CheckersPoint jumpedPoint = moveToMake.JumpedPoint;
                if (jumpedPoint != null)
                {
                    //delete the checker piece that was jumped
                    CheckersSquareUserControl jumpedSquareUserControl = this.BoardArray[jumpedPoint.Row][jumpedPoint.Column];
                    jumpedSquareUserControl.CheckersPoint.Checker = CheckerPieceFactory.GetCheckerPiece(CheckerPieceType.nullPiece);
                    jumpedSquareUserControl.UpdateSquare();
                }

                //Check for win

                //Is this piece a king now?
                if (!(destination.Checker is KingCheckerPiece))
                {
                    if (destination.Row == 7 || destination.Row == 0)
                    {
                        //Should be a king now
                        if (destination.Checker is IRedPiece)
                        {
                            destination.Checker = new RedKingCheckerPiece();
                        }
                        else
                        {
                            destination.Checker = new BlackKingCheckerPiece();
                        }

                    }
                }

                //Swap the current players turn
                SwapTurns();
            }
        }

        public object GetMinimaxClone()
        {
            CheckerBoard clonedBoard = new CheckerBoard();
            clonedBoard.CurrentPlayerTurn = this.CurrentPlayerTurn;
            List<List<CheckersSquareUserControl>> rows = new List<List<CheckersSquareUserControl>>();

            for(int row = 0; row < this.boardArray.Count; row++)
            {
                List<CheckersSquareUserControl> columns = new List<CheckersSquareUserControl>();
                for (int col = 0; col < this.boardArray[row].Count; col++){
                    columns.Add((CheckersSquareUserControl)this.boardArray[row][col].GetMinimaxClone());
                }
                rows.Add(columns);
            }

            clonedBoard.boardArray = rows;

            return clonedBoard;
        }

        //public CheckerBoard GetCloneOfThisObject()
        //{
        //    CheckerBoard clonedBoard =  new CheckerBoard();
        //    clonedBoard.CurrentPlayerTurn = this.CurrentPlayerTurn.Copy();
        //    clonedBoard.boardArray = new List<List<CheckersSquareUserControl>>();

        //    foreach(List<CheckersSquareUserControl> list in this.boardArray)
        //    {
        //        clonedBoard.boardArray.Add(new List<CheckersSquareUserControl>());
        //        foreach (CheckersSquareUserControl csuc in list)
        //        {
        //            CheckersPoint newPoint = csuc.CheckersPoint.Copy();
        //            new CheckersSquareUserControl(csuc.BackgroundColor, newPoint, null);
        //            clonedBoard.boardArray[]
        //        }
        //    }
        //}
    }
}
