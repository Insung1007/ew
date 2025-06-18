using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample2
{
    public class Solution
    {
        public string solution(int money, int[,] cost, string[] name)
        {
            string answer = null;
            long max_distance = 0;

            for (int i = 0; i < cost.GetLength(0); i++)
            {
                int oil = money / cost[i, 1];
                long distance = cost[i, 0] * oil;
                if (distance > max_distance)
                {
                    max_distance = distance;
                    answer = name[i];
                }
            }
            return answer;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            int money = 100000;
            int[,] cost = new int[,] { { 50, 5000 }, { 20, 1000 }, { 20, 5000 }, { 50, 1000 } };
            string[] name = new string[] { "A", "B", "C", "D" };
            Solution sol = new Solution();
            string ret = sol.solution(money, cost, name);

            
            Console.WriteLine("운행가능거리가 가장 긴 차량은 " + ret + " 입니다.");

        }
    }
}
