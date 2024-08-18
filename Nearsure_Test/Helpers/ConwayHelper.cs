using ConwayGame.Models;
using System.Text;

namespace ConwayGame.Helpers
{
    public class ConwayHelper
    {
        public const string LIVE_CELL = "X";
        public const string DEAD_CELL = "-";
        public const int MAX_ATTEMPTS = 50;
        public static string[,] ConvertStringToBoard(string boardString, int rowCount, int colCount)
        {
            var lines = boardString.Split(new[] { "\r\n" }, StringSplitOptions.None);

            var newBoard = new string[rowCount, colCount];

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    newBoard[i, j] = lines[i][j].ToString();
                }
            }

            return newBoard;
        }

        public static string ConvertBoardToString(string[,] board, int rowCount, int columnCount)
        {
            var boardString = new StringBuilder();

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    boardString.Append(board[i, j]);
                }

                if (i < rowCount - 1)
                {
                    boardString.Append("\r\n");
                }
            }

            return boardString.ToString();
        }

        public static int CountLiveNeighbors(string[,] board, int row, int col)
        {
            int count = 0;
            int rows = board.GetLength(0);
            int cols = board.GetLength(1);

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                        continue;

                    int newRow = row + i;
                    int newCol = col + j;

                    if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols)
                    {
                        count += board[newRow, newCol] == LIVE_CELL ? 1 : 0;
                    }
                }
            }

            return count;
        }

        public static string GetNextState(string currentState, int rowCount, int columnCount)
        {
            var currentBoard = ConvertStringToBoard(currentState, rowCount, columnCount);
            var nextBoard = (string[,])currentBoard.Clone();

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    int liveNeighbors = CountLiveNeighbors(currentBoard, i, j);

                    if (currentBoard[i, j] == LIVE_CELL && (liveNeighbors < 2 || liveNeighbors > 3))
                    {
                        nextBoard[i, j] = DEAD_CELL;
                    }
                    else if (currentBoard[i, j] == DEAD_CELL && liveNeighbors == 3)
                    {
                        nextBoard[i, j] = LIVE_CELL;
                    }
                }
            }

            string nextState = ConvertBoardToString(nextBoard, rowCount, columnCount);
            return nextState;
        }

        public static string GetFutureState(string currentState, int rowCount, int columnCount, int steps)
        {
            string resultState = currentState;
            for (int i = 0; i < steps; i++)
            {
                resultState = GetNextState(resultState, rowCount, columnCount);
            }

            return resultState;
        }

        public static string? GetFinalState(string boardState, int rowCount, int columnCount)
        {
            var previousStates = new HashSet<string>();
            int attempts = 0;

            while (attempts < MAX_ATTEMPTS)
            {
                // Case of cyclic board
                if (previousStates.Contains(boardState))
                {
                    return boardState;
                }

                previousStates.Add(boardState);
                string nextBoard = GetNextState(boardState, rowCount, columnCount);

                // Case of no live cells
                if (!nextBoard.Contains(LIVE_CELL))
                {
                    return "";
                }

                // Case of still lifes
                if (boardState == nextBoard)
                {
                    return boardState;
                }

                boardState = nextBoard;
                attempts++;
            }
            return null;
        }
    }
}
