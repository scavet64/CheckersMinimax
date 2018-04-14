using CheckersMinimax.AI;
using CheckersMinimax.Clone;
using CheckersMinimax.Pieces;
using CheckersMinimax.Properties;
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
    public class CheckerBoard : IMinimaxClonable
    {
        private static readonly Settings settings = Settings.Default;
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

        public override string ToString()
        {
            StringBuilder boardString = new StringBuilder();
            foreach (List<CheckersSquareUserControl> list in BoardArray)
            {
                StringBuilder rowBuilder = new StringBuilder();
                foreach (CheckersSquareUserControl squareUC in list)
                {
                    if (squareUC.CheckersPoint.Checker != null)
                    {
                        //There is a piece here
                        rowBuilder.Append(squareUC.CheckersPoint.Checker.GetStringRep());
                    }
                }
                boardString.AppendLine(rowBuilder.ToString());
            }

            return boardString.ToString();
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

        //public List<CheckersMove> GetAvailableMovesForSquare(CheckersPoint checkersPoint)
        //{
        //    if (checkersPoint.Checker == null)
        //    {
        //        return null;
        //    }

        //    return checkersPoint.GetPossibleMoves(this);
        //}

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
                    if (squareUC.CheckersPoint.Checker != null && squareUC.CheckersPoint.Checker is ColorInterface)
                    {
                        listOfColor.Add(squareUC.CheckersPoint);
                    }
                }
            }

            return listOfColor;
        }

        public int ScoreA()
        {
            int score = 0;
            object winningColor = this.GetWinner();

            if (winningColor != null && winningColor is PlayerColor winnerColor)
            {
                if (winnerColor == PlayerColor.Black)
                {
                    score = int.MaxValue;
                }
                else
                {
                    score = int.MinValue;
                }

            }
            else
            {
                foreach (CheckersPoint point in GetPointsForColor<IBlackPiece>())
                {
                    score += point.Checker is KingCheckerPiece ? settings.KingWorth : settings.PawnWorth;
                }
                foreach (CheckersPoint point in GetPointsForColor<IRedPiece>())
                {
                    score -= point.Checker is KingCheckerPiece ? settings.KingWorth : settings.PawnWorth;
                }
            }
            return score;
        }

        internal int ScoreB()
        {
            int score = 0;

            foreach (List<CheckersSquareUserControl> list in BoardArray)
            {
                foreach (CheckersSquareUserControl squareUC in list)
                {
                    if (squareUC.CheckersPoint.Checker != null && !(squareUC.CheckersPoint.Checker is KingCheckerPiece))
                    {
                        if (CurrentPlayerTurn == PlayerColor.Black)
                        {
                            if (squareUC.CheckersPoint.Checker is IRedPiece)
                            {
                                //Closer to top means red is closer to being king
                                score -= ((squareUC.CheckersPoint.Row - 7) * 1);
                            }

                            if (squareUC.CheckersPoint.Checker is IBlackPiece)
                            {
                                //Closer to bottom means that black is closer to being a king
                                score += (squareUC.CheckersPoint.Row);
                            }
                        }
                        else
                        {
                            if (squareUC.CheckersPoint.Checker is IRedPiece)
                            {
                                //Closer to top means red is closer to being king
                                score += ((squareUC.CheckersPoint.Row - 7) * 1);
                            }

                            if (squareUC.CheckersPoint.Checker is IBlackPiece)
                            {
                                //Closer to bottom means that black is closer to being a king
                                score -= (squareUC.CheckersPoint.Row);
                            }
                        }
                    }
                }
            }

            return score;
        }

        internal int ScoreC()
        {
            int score = 0;

            List<CheckersMove> movesForOtherPlayer = GetMovesForPlayer(CurrentPlayerTurn);

            foreach(CheckersMove move in movesForOtherPlayer)
            {
                CheckersMove moveToCheck = move;
                do
                {
                    if (moveToCheck.JumpedPoint != null)
                    {
                        //A piece is in danger
                        if(moveToCheck.JumpedPoint.Checker is KingCheckerPiece)
                        {
                            score -= settings.KingDangerValue;
                        }
                        else
                        {
                            score -= settings.PawnDangerValue;
                        }
                    }
                    moveToCheck = moveToCheck.NextMove;
                } while (moveToCheck != null);
            }

            if(CurrentPlayerTurn == PlayerColor.Black)
            {
                //invert score for other player
                score *= -1;
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
            else if (currentPlayerTurn == PlayerColor.Black)
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
                allAvailableMoves.AddRange(checkerPoint.GetPossibleMoves(this));
            }

            //If we have jump moves, filter out non jumps
            List<CheckersMove> jumpMoves = new List<CheckersMove>();
            foreach (CheckersMove move in allAvailableMoves)
            {
                if (move.JumpedPoint != null)
                {
                    jumpMoves.Add(move);
                }
            }

            if (jumpMoves.Count > 0)
            {
                return jumpMoves;
            }
            else
            {
                return allAvailableMoves;
            }
        }

        /// <summary>
        /// Makes the move on the board. Returns a boolean that represents if the turn was finished after making this move
        /// </summary>
        /// <param name="moveToMake">Move to make</param>
        /// <returns>True if the turn is over</returns>
        public bool MakeMoveOnBoard(CheckersMove moveToMake)
        {
            return MakeMoveOnBoard(moveToMake, true);
        }

        public bool MakeMoveOnBoard(CheckersMove moveToMake, bool swapTurn)
        {
            CheckersPoint moveSource = moveToMake.SourcePoint;
            CheckersPoint moveDestination = moveToMake.DestinationPoint;

            //Console.WriteLine("Piece1 " + source.Row + ", " + source.Column);
            //Console.WriteLine("Piece2 " + destination.Row + ", " + destination.Column);

            //was this a cancel?
            if (moveSource != moveDestination)
            {
                CheckersPoint realDestination = this.BoardArray[moveDestination.Row][moveDestination.Column].CheckersPoint;
                CheckersPoint realSource = this.BoardArray[moveSource.Row][moveSource.Column].CheckersPoint;

                realDestination.Checker = (CheckerPiece) realSource.Checker.GetMinimaxClone();
                realSource.Checker = CheckerPieceFactory.GetCheckerPiece(CheckerPieceType.nullPiece);

                //was this a jump move?
                CheckersPoint jumpedPoint = moveToMake.JumpedPoint;
                if (jumpedPoint != null)
                {
                    //delete the checker piece that was jumped
                    CheckersSquareUserControl jumpedSquareUserControl = this.BoardArray[jumpedPoint.Row][jumpedPoint.Column];
                    jumpedSquareUserControl.CheckersPoint.Checker = CheckerPieceFactory.GetCheckerPiece(CheckerPieceType.nullPiece);
                    jumpedSquareUserControl.UpdateSquare();
                }

                //Is this piece a king now?
                if (!(realDestination.Checker is KingCheckerPiece) 
                    && (realDestination.Row == 7 || realDestination.Row == 0))
                {
                    //Should be a king now
                    if (realDestination.Checker is IRedPiece)
                    {
                        realDestination.Checker = new RedKingCheckerPiece();
                    }
                    else
                    {
                        realDestination.Checker = new BlackKingCheckerPiece();
                    }
                }

                //Is this players turn over?
                if (moveToMake.NextMove == null && swapTurn)
                {
                    //Swap the current players turn
                    SwapTurns();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public object GetMinimaxClone()
        {
            List<List<CheckersSquareUserControl>> rows = new List<List<CheckersSquareUserControl>>();

            for (int row = 0; row < this.boardArray.Count; row++)
            {
                List<CheckersSquareUserControl> columns = new List<CheckersSquareUserControl>();
                for (int col = 0; col < this.boardArray[row].Count; col++)
                {
                    columns.Add((CheckersSquareUserControl)this.boardArray[row][col].GetMinimaxClone());
                }
                rows.Add(columns);
            }

            return new CheckerBoard
            {
                CurrentPlayerTurn = this.CurrentPlayerTurn,
                BoardArray = rows
            };
        }
    }
}
