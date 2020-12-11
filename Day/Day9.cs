using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day
{
    public class Day9 : IDay
    {
        private readonly string[] _inputs;
        
        public Day9()
        {
            _inputs = File.ReadAllLines("./data/day9.txt").Where(line => !string.IsNullOrEmpty(line)).ToArray();
        }

        public static void Run()
        {
            var day = new Day9();
            day.SolvePart1();
            day.SolvePart2();
        }

        public void SolvePart1()
        {
            var inputs = new List<long>();
            foreach (var input in _inputs)
                inputs.Add(long.Parse(input));
            
            long invalidNumber = -1;
            for (int i = 25; i < inputs.Count; i++)
            {
                if (!SumFound(inputs, i, 25))
                {
                    invalidNumber = inputs[i];
                    break;
                }
            }
            
            Console.WriteLine("Day 9\t> Part 1\n\t\t Invalid Number: {0}", invalidNumber);
        }

        public void SolvePart2()
        {
            var inputs = new List<long>();
            foreach (var input in _inputs)
                inputs.Add(long.Parse(input));
            
            long invalidNumber = -1;
            for (int i = 25; i < inputs.Count; i++)
            {
                if (!SumFound(inputs, i, 25))
                {
                    invalidNumber = inputs[i];
                    break;
                }
            }
            
            var q = new Queue<long>();
            for (int i = 0; i < inputs.Count; i++)
            {
                long curNum = inputs[i];
                q.Enqueue(curNum);            
                
                while (q.Sum() > invalidNumber)
                    q.Dequeue();
                
                if (q.Sum() == invalidNumber)
                    break;
            }

            Console.WriteLine("\t> Part 2\n\t\t Weakness number: {0}", q.Min() + q.Max());
        }

        private bool SumFound(List<long> inputs, int index, int prevCount)
        {
            long targetSum = inputs[index];
            // https://stackoverflow.com/a/28630744
            var numbers = inputs.Skip(index - prevCount).Take(prevCount);
            var cartesian = numbers.SelectMany(_ => numbers, (a, b) => Tuple.Create((long)a, (long)b));
            return cartesian.Any(tuple => tuple.Item1 + tuple.Item2 == targetSum);
        }
    }
}