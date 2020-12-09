using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day
{
    public class Day5 : IDay
    {
        private readonly string[] _inputs;
        
        public Day5()
        {
            _inputs = File.ReadAllLines("./data/day5.txt").Where(line => !string.IsNullOrEmpty(line)).ToArray();
        }

        public void SolvePart1()
        {
            var seatIds = new List<int>();
            foreach (var line in _inputs)
            {
                var pass = CreateBoardingPass(line);
                seatIds.Add(pass.SeatID);
            }
            Console.WriteLine("Day 5\t> Part 1\n\t\t Highest Seat ID: {0}", seatIds.Max());
        }

        public void SolvePart2()
        {
            var seatIds = new List<int>();
            foreach (var line in _inputs)
            {
                var pass = CreateBoardingPass(line);
                seatIds.Add(pass.SeatID);
            }
            seatIds.Sort();
            int seatId = 0;
            for (int i = 0; i < seatIds.Count - 1; i++)
            {
                if (seatIds[i] == seatIds[i+1] - 2)
                {
                    seatId = seatIds[i] + 1;
                    break;
                }
            }
            Console.WriteLine("\t> Part 2\n\t\t Your Seat ID: {0}", seatId);
        }

        public static void Run()
        {
            var day = new Day5();
            day.SolvePart1();
            day.SolvePart2();
        }

        private record BoardingPass(int Row, int Col)
        {
            public int SeatID => (Row * 8) + Col;
        }

        private static BoardingPass CreateBoardingPass(string line)
        {
            return new BoardingPass(CalculateRow(line.Substring(0, 7)), CalculateCol(line.Substring(7)));
        }

        private static int CalculateRow(string code)
        {
            int lower = 0;
            int upper = 127;
            for (int i = 0; i < 6; i++)
            {
                char letter = code[i];
                if (letter == 'B') // Upper half
                    lower += (int) Math.Ceiling((upper - lower) / 2.0);
                if (letter == 'F') // Upper half
                    upper -= (int) Math.Ceiling((upper - lower) / 2.0);
            }
            return code[6] == 'B' ? upper : lower;
        }

        private static int CalculateCol(string code)
        {
            int lower = 0;
            int upper = 7;
            for (int i = 0; i < 2; i++)
            {
                char letter = code[i];
                if (letter == 'R') // Upper half
                    lower += (int) Math.Ceiling((upper - lower) / 2.0);
                if (letter == 'L') // Upper half
                    upper -= (int) Math.Ceiling((upper - lower) / 2.0);
            }
            return code[2] == 'R' ? upper : lower;
        }
    }
}