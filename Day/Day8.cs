using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day
{
    public class Day8 : IDay
    {
        private readonly string[] _inputs;
        
        public Day8()
        {
            _inputs = File.ReadAllLines("./data/day8.txt").Where(line => !string.IsNullOrEmpty(line)).ToArray();
        }

        public static void Run()
        {
            var day = new Day8();
            day.SolvePart1();
            day.SolvePart2();
        }

        public void SolvePart1()
        {
            var instructions = new Instructions(_inputs);

            var instructionLog = new List<Instruction>();
            int accumulator = 0;
            Instruction instruction;
            int index = 0;
            while (!instructionLog.Contains(instruction = instructions[index]))
            {
                instructionLog.Add(instruction);
                
                if (instruction.Operation == Op.Acc)
                    accumulator += instruction.Argument;

                if (instruction.Operation == Op.Jmp)
                    index += instruction.Argument;
                else
                    index++;
            }

            Console.WriteLine("Day 8\t> Part 1\n\t\t Accumulator: {0}", accumulator);
        }

        public void SolvePart2()
        {
            var instructions = new Instructions(_inputs);
            var posInstructions = instructions.GetAll().Where(i => i.Operation == Op.Jmp || i.Operation == Op.Nop);
            Instructions tempInstructions = instructions.Clone();
            int index;
            foreach (var posIns in posInstructions)
            {
                tempInstructions = instructions.Clone();
                index = instructions.IndexOf(posIns);
                var newIns = new Instruction(posIns.Operation == Op.Jmp ? Op.Nop : Op.Jmp, posIns.Argument);
                tempInstructions.Set(index, newIns);
                if (!IsInfiniteLooping(tempInstructions))
                    break;
            }

            int accumulator = 0;
            Instruction instruction;
            index = 0;
            while (index < instructions.Count)
            {
                instruction = tempInstructions[index];
                if (instruction.Operation == Op.Acc)
                    accumulator += instruction.Argument;

                if (instruction.Operation == Op.Jmp)
                    index += instruction.Argument;
                else
                    index++;
            }
            Console.WriteLine("\t> Part 2\n\t\t Accumulator: {0}", accumulator);
        }

        private bool IsInfiniteLooping(Instructions instructions)
        {
            var instructionLog = new List<Instruction>();
            int accumulator = 0;
            Instruction instruction;
            int index = 0;
            while (index < instructions.Count && !instructionLog.Contains(instruction = instructions[index]))
            {
                instructionLog.Add(instruction);
                
                if (instruction.Operation == Op.Acc)
                    accumulator += instruction.Argument;

                if (instruction.Operation == Op.Jmp)
                    index += instruction.Argument;
                else
                    index++;
            }
            return index != instructions.Count;
        }

        private enum Op
        {
            Nop,
            Acc,
            Jmp
        }

        private class Instruction
        {
            public Op Operation { get; set; }
            public int Argument { get; set; }

            public Instruction(Op op, int arg)
            {
                Operation = op;
                Argument = arg;
            }

            public Instruction Clone()
            {
                return new Instruction(Operation, Argument);
            }
        }

        private class Instructions
        {
            private readonly Instruction[] _instructions;

            public Instructions(Instruction[] instructions)
            {
                _instructions = instructions;
            }

            public Instructions(string[] inputs)
            {
                _instructions = new Instruction[inputs.Length];
                for (int i = 0; i < inputs.Length; i++)
                {
                    var input = inputs[i];
                    var split = input.Split(' ');

                    var op = Enum.Parse<Op>(split[0], true);
                    var arg = int.Parse(split[1].TrimStart('+'));
                    _instructions[i] = new Instruction(op, arg);
                }
            }

            public Instructions Clone()
            {
                return new Instructions(_instructions.Select(i => i.Clone()).ToArray());
            }

            public int Count => _instructions.Length;

            public Instruction this[int index] => Get(index);

            public Instruction Get(int index) => _instructions[index];

            public Instruction[] GetAll() => _instructions;

            public void Set(int index, Instruction instruction) => _instructions[index] = instruction;

            public int IndexOf(Instruction instruction) => Array.IndexOf(_instructions, instruction);
        }
    }
}