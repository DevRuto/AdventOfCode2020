using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day
{
    public class DayThree : IDay
    {
        private readonly string[] _inputs;
        public DayThree()
        {
            _inputs = File.ReadAllLines("./data/day3.txt").Where(line => !string.IsNullOrEmpty(line)).ToArray();
        }
        public void SolvePart1()
        {
            int maxX = _inputs[0].Length;
            int maxY = _inputs.Length;
            var map = new char[maxY, maxX];
            for (int row = 0; row < maxY; row++)
            {
                var line = _inputs[row];
                for (int col = 0; col < line.Length; col++)
                    map[row, col] = line[col];
            }

            int x = 0, y = 0;
            int treeEncounters = 0;

            int slopeX = 3;
            int slopeY = 1;
            while (y < maxY)
            {
                if (map[y, x] == '#')
                    treeEncounters++;
                x += slopeX;
                y += slopeY;
                if (x >= maxX)
                    x -= maxX;
            }
            Console.WriteLine("Day Three Part 1: {0} trees encountered", treeEncounters);
        }

        public void SolvePart2()
        {
            int maxX = _inputs[0].Length;
            int maxY = _inputs.Length;
            var map = new char[maxY, maxX];
            for (int row = 0; row < maxY; row++)
            {
                var line = _inputs[row];
                for (int col = 0; col < line.Length; col++)
                    map[row, col] = line[col];
            }

            var slopes = new List<KeyValuePair<int, int>>
            {
                new KeyValuePair<int, int>(1, 1),
                new KeyValuePair<int, int>(3, 1),
                new KeyValuePair<int, int>(5, 1),
                new KeyValuePair<int, int>(7, 1),
                new KeyValuePair<int, int>(1, 2)
            };
            List<int> lstTreeEncounters = new List<int>();
            foreach (var slope in slopes)
            {
                int slopeX = slope.Key, slopeY = slope.Value;
                int x = 0, y = 0;
                int treeEncounters = 0;
                
                while (y < maxY)
                {
                    if (map[y, x] == '#')
                        treeEncounters++;
                    x += slopeX;
                    y += slopeY;
                    if (x >= maxX)
                        x -= maxX;
                }
                lstTreeEncounters.Add(treeEncounters);
            }
            Console.WriteLine("Day Three Part 2");
            long product = lstTreeEncounters[0];
            for (int i = 0; i < lstTreeEncounters.Count; i++)
            {
                Console.WriteLine("\t{0}. {1} trees encountered", i, lstTreeEncounters[i]);
                if (i != 0)
                    product *= (long)lstTreeEncounters[i];
            }
            
            Console.WriteLine("\tProduct: {0}", product);
        }

        public static void Run()
        {
            var day = new DayThree();
            day.SolvePart1();
            day.SolvePart2();
        }
    }
}