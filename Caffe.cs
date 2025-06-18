using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample5
{
    public class Solution
    {
        private int func_a(int[] arr)
        {
            int answer = -1;
            for (int i = 0; i < arr.Length; i++)
            {
                if (answer == -1)
                    answer = i;
                else if (arr[answer] < arr[i])
                    answer = i;
            }
            return answer;
        }

        private int[] func_b(string[] arr1, string[] arr2)
        {
            int[] answer = new int[arr1.Length];
            for (int i = 0; i < arr1.Length; i++)
            {
                for (int j = 0; j < arr2.Length; j++)
                {
                    if (arr1[i].Equals(arr2[j]))
                        answer[i]++;
                }
            }
            return answer;
        }

        private int func_c(int[] arr, int number)
        {
            int answer = -1;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == number)
                    continue;
                if (answer == -1)
                    answer = i;
                else if (arr[answer] < arr[i])
                    answer = i; //here
            }
            return answer;
        }

        public string[] solution(string[] menu, string[] votes)
        {

            int[] counter = func_b(menu, votes);
            int first = func_a(counter);
            int second = func_c(counter, counter[first]);

            string[] answer = new string[2];
            answer[0] = menu[first];
            answer[1] = menu[second];
            return answer;
        }
    }



    internal class Program
    {
        static void Main(string[] args)
        {
            Solution sol = new Solution();
            string[] menuA = new string[] { "Latte", "Americano", "Espresso" };
            string[] votesA = new string[] { "Latte", "Americano", "Americano", "Latte", "Americano", "Americano", "Latte", "Latte", "Latte", "Latte" };
            string[] retA = sol.solution(menuA, votesA);
                        
            Console.WriteLine("첫번째 투표 결과 => " + "1위 : " + string.Join(", 2위 : ", retA) + "" + " 입니다.");

            string[] menuB = new string[] { "MochaLatte", "GreenTea", "Cappuccino" };
            string[] votesB = new string[] { "GreenTea", "GreenTea", "MochaLatte", "GreenTea", "Cappuccino", "Cappuccino" };
            string[] retB = sol.solution(menuB, votesB);
            
            Console.WriteLine("두번째 투표 결과 => " + "1위 : " + string.Join(", 2위 : ", retB) + "" + " 입니다.");

        }
    }
}
