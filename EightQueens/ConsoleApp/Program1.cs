using System;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Exercises.CleanCode.EightQueens.ConsoleApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            List<char[][]> solutions = new List<char[][]>();

            char[][] result = new char[8][];

            DrawChessboard(result);

            SolveAllNQueens(result, 0, solutions);

            Console.WriteLine(solutions.Count);
            DrawAllSolutions(solutions);

            Console.ReadKey();
        }

        private static void DrawAllSolutions(List<char[][]> solutions)
        {
            for (int i = 0; i < solutions.Count; i++)
            {
                Console.WriteLine("\nSolution " + (i + 1));

                char[][] board = solutions[i];

                for (int r = 0; r < board.Length; r++)
                {
                    for (int c = 0; c < board[r].Length; c++)
                    {
                        Console.Write(board[r][c] + " ");
                    }

                    Console.WriteLine();
                }
            }
        }

        private static void DrawChessboard(char[][] result)
        {
            for (int r1 = 0; r1 < 8; r1++)
            {
                result[r1] = new char[8];

                for (int r2 = 0; r2 < 8; r2++)
                {
                    result[r1][r2] = '.';
                }
            }
        }

        private static void SolveAllNQueens(char[][] board, int col, List<char[][]> solutions)
        {
            if (col == board.Length)
            {
                char[][] copy = new char[board.Length][];
                for (int r = 0; r < board.Length; r++)
                {
                    copy[r] = new char[board[0].Length];
                }

                for (int r = 0; r < board.Length; r++)
                {
                    for (int c = 0; c < board[0].Length; c++)
                    {
                        copy[r][c] = board[r][c];
                    }
                }

                solutions.Add(copy);
            }
            else
            {
                for (int row = 0; row < board.Length; row++)
                {
                    board[row][col] = 'Q';
                    bool canBeSafe = true;

                    // Go vertical
                    canBeSafe = GoThroughFieldsToFindQueen(board, canBeSafe);

                    // Go horizontal
                    canBeSafe = GoThroughFieldsToFindQueen(board, canBeSafe);

                    canBeSafe = CheckDiagonally(board, canBeSafe, 0, -1, -1, 0);

                    canBeSafe = CheckDiagonally(board, canBeSafe, 1, 1, 1, 1);

                    if (canBeSafe)
                    {
                        SolveAllNQueens(board, col + 1, solutions);
                    }

                    board[row][col] = '.';
                }
            }
        }

        private static bool CheckDiagonally(char[][] board, bool canBeSafe, int coefficient1, int coefficient2, int coefficient3, int coefficient4)
        {
            for (int offset = -board.Length; offset < board.Length; offset++)
            {
                bool found = false;
                for (int i = 0; i < board.Length; i++)
                {
                    int sidestep = (board.Length * coefficient1) - (offset * coefficient2) - (i * coefficient3) - (1 * coefficient4);
                    CheckStepBound(board, ref canBeSafe, ref found, i, sidestep);
                }
            }

            return canBeSafe;
        }

        private static void CheckStepBound(char[][] board, ref bool canBeSafe, ref bool found, int i, int sidestep)
        {
            if (Inbounds(i, sidestep, board))
            {
                if (board[i][sidestep] == 'Q')
                {
                    CheckIfFoundIsTrue(ref canBeSafe, ref found);
                }
            }
        }

        private static bool GoThroughFieldsToFindQueen(char[][] board, bool canBeSafe)
        {
            for (int i = 0; i < board.Length; i++)
            {
                bool found = false;
                for (int j = 0; j < board.Length; j++)
                {
                    CheckIfQueenFound(board, ref canBeSafe, i, ref found, j);
                }
            }

            return canBeSafe;
        }

        private static void CheckIfQueenFound(char[][] board, ref bool canBeSafe, int counter1, ref bool found, int counter2)
        {
            if (board[counter1][counter2] == 'Q')
            {
                CheckIfFoundIsTrue(ref canBeSafe, ref found);
            }
        }

        private static void CheckIfFoundIsTrue(ref bool canBeSafe, ref bool found)
        {
            if (found)
            {
                canBeSafe = false;
            }

            found = true;
        }

        private static bool Inbounds(int row, int col, char[][] mat)
        {
            return row >= 0 && row < mat.Length && col >= 0 && col < mat[0].Length;
        }
    }
}