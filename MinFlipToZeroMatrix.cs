namespace ZeroMatrix
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        public static void Main()
        {
            int[][] tempMat =  new int[][] { new int[] { 0, 0, 0 },
                                             new int[] { 0, 1, 0 },
                                             new int[] { 0, 0, 0 }
            };
            int numberOfSteps = MinFlips(tempMat);

            int MinFlips(int[][] mat)
            {
                Queue<int[][]> queueOfMatState = new Queue<int[][]>();
                List<string> matStateKeyList = new List<string>();
                int matColumns = mat[0].Length; // 1~3
                int matRows = mat.Length; // 1~3
                int stepsToZeroMat = 0; 
                int queueSize;
                int[][] currentMatState;

                queueOfMatState.Enqueue(mat);
                while (queueOfMatState.Count > 0)
                {
                    queueSize = queueOfMatState.Count;
                    for (int i = 0; i < queueSize; i++)
                    {
                        currentMatState = queueOfMatState.Dequeue();
                        
                        if (CheckZeroMatrix(currentMatState) == 0)
                        {
                            Console.WriteLine($"Steps to zero!!: {stepsToZeroMat}");
                            return stepsToZeroMat;
                        }

                        for(int row = 0; row < matRows; row++)
                        {
                            for(int col = 0; col < matColumns; col++)
                            {
                                int[][] newMatState = FlipOnIndex(currentMatState, row, col, matRows, matColumns);
                                string matStringKey = convertMatToString(newMatState);

                                if (!matStateKeyList.Contains(matStringKey))
                                {
                                    queueOfMatState.Enqueue(newMatState);
                                    matStateKeyList.Add(matStringKey);
                                    Console.WriteLine($"States: {matStateKeyList.Count}");
                                }
                            }
                        }
                    }
                    stepsToZeroMat++;
                }
                Console.WriteLine("-1");
                return -1;
            }
            Console.Read();
        }

        static int CheckZeroMatrix(int[][] mat)
        {
            int matSum = 0;

            foreach (int[] row in mat)
            {
                matSum += row.Sum();
            }

            return matSum;
        }

        static string convertMatToString(int[][] newMatState )
        {
            string matStateKey = string.Empty;

            foreach (int[] row in newMatState)
            {
                foreach (int val in row)
                {
                    matStateKey += val.ToString();
                }
            }

            return matStateKey;
        }

        static int[][] FlipOnIndex(int[][] mat, int row, int col, int numberOfRows, int numberOfCols)
        {
            int[][] flippedMat = mat.Select(val => val.ToArray()).ToArray();
            int[][] indexNeighborsOffset = { new int[] { 1, 0 },
                                             new int[] { 0, 1 },
                                             new int[] { -1, 0 },
                                             new int[] { 0, -1 }};
            int rowNeighborIndex;
            int colNeighborIndex;

            flippedMat[row][col] ^= 1;
            foreach (int[] offset in indexNeighborsOffset)
            {
                rowNeighborIndex = offset[0] + row;
                colNeighborIndex = offset[1] + col;
                if (rowNeighborIndex < numberOfRows && colNeighborIndex < numberOfCols && rowNeighborIndex >= 0 && colNeighborIndex >= 0)
                { 
                flippedMat[rowNeighborIndex][colNeighborIndex] ^= 1;
                }
            }
            return flippedMat;
        }
    }
}