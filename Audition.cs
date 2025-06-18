using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample1
{
    public class Solution
    {
        public int[][] func_a(int[][] arr)
        {
            int[][] ret = new int[arr.Length][];
            for (int i = 0; i < arr.Length; i++)
            {
                ret[i] = new int[arr[0].Length - 2];
                Array.Sort(arr[i]);
                for (int j = 1; j < arr[i].Length - 1; j++)
                    ret[i][j - 1] = arr[i][j];
            }
            return ret;
        }

        public int func_b(int[][] arr)
        {
            int[] ret = new int[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                    ret[i] += arr[i][j];
                ret[i] = ret[i] / arr[i].Length;
            }
            Array.Sort(ret);
            return ret[ret.Length - 1];
        }

        public int[][] convertJaggedArray(int[,] arr)
        {
            int[][] jaggedArray = new int[arr.GetLength(0)][];
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                jaggedArray[i] = new int[arr.GetLength(1)];
                for (int j = 0; j < arr.GetLength(1); j++)
                    jaggedArray[i][j] = arr[i, j];
            }
            return jaggedArray;
        }

        public int solution(int[,] scores)
        {
            int[][] scoreArray = convertJaggedArray(scores);
            int[][] arr2 = func_a(scoreArray);
            int answer = func_b(arr2);
            return answer;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Solution sol = new Solution();
            int[,] scores = new int[,] { { 85, 92, 95, 90 }, { 91, 76, 85, 50 } };
            int ret = sol.solution(scores);

            
            Console.WriteLine("가장 높은 오디션 평균점수는 " + ret + " 입니다.");

        }
    }
}
