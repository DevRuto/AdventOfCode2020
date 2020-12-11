using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day
{
    public class Day4 : IDay
    {
        private readonly string[] _passports;
        private readonly string[] _requiredFields = { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" }; // cid - optional
        
        public Day4()
        {
            // The different kinds of line endings was probably unnecessary but eh
            _passports = File.ReadAllText("./data/day4.txt").Split( new[] { "\r\n\r\n", "\r\r", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public void SolvePart1()
        {
            int valid = 0;
            foreach (var strPassport in _passports)
            {
                var existingFields = strPassport.Split(new[] { " ", "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(field => field.Split(':')[0]).Where(field => field != "cid").ToArray();
                if (!_requiredFields.Except(existingFields).Any())
                    valid++;
            }
            Console.WriteLine("Day 4\t> Part 1\n\t\t {0} passports are valid", valid);
        }

        public void SolvePart2()
        {
            int valid = 0;
            foreach (var strPassport in _passports)
            {
                var passport = Passport.Create(strPassport);
                if (passport.IsValid())
                    valid++;
            }
            Console.WriteLine("\t> Part 2\n\t\t {0} passports are valid", valid);
        }

        public static void Run()
        {
            var day = new Day4();
            day.SolvePart1();
            day.SolvePart2();
        }

        private class Passport
        {
            public string byr { get; set; }
            public string iyr { get; set; }
            public string eyr { get; set; }
            public string hgt { get; set; }
            public string hcl { get; set; }
            public string ecl { get; set; }
            public string pid { get; set; }
            public string cid { get; set; }

            private readonly string[] _validEyeColors = { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

            public static Passport Create(string strPassport)
            {
                var fields = strPassport.Split(new[] { " ", "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                var passport = new Passport
                {
                    byr = fields.FirstOrDefault(f => f.StartsWith("byr"))?.Substring(4),
                    iyr = fields.FirstOrDefault(f => f.StartsWith("iyr"))?.Substring(4),
                    eyr = fields.FirstOrDefault(f => f.StartsWith("eyr"))?.Substring(4),
                    hgt = fields.FirstOrDefault(f => f.StartsWith("hgt"))?.Substring(4),
                    hcl = fields.FirstOrDefault(f => f.StartsWith("hcl"))?.Substring(4),
                    ecl = fields.FirstOrDefault(f => f.StartsWith("ecl"))?.Substring(4),
                    pid = fields.FirstOrDefault(f => f.StartsWith("pid"))?.Substring(4),
                    cid = fields.FirstOrDefault(f => f.StartsWith("cid"))?.Substring(4),
                };
                return passport;
            }

            public bool IsValid()
            {
                if (byr == null || byr.Length != 4 || !int.TryParse(byr, out int birthYear))
                    return false;
                if (birthYear < 1920 || birthYear > 2002)
                    return false;

                if (iyr == null || iyr.Length != 4 || !int.TryParse(iyr, out int issueYear))
                    return false;
                if (issueYear < 2010 || issueYear > 2020)
                    return false;

                if (eyr == null || eyr.Length != 4 || !int.TryParse(eyr, out int expirationYear))
                    return false;
                if (expirationYear < 2020 || expirationYear > 2030)
                    return false;

                if (hgt == null || !(hgt.Contains("cm") || hgt.Contains("in")))
                    return false;
                if (!int.TryParse(Regex.Match(hgt, @"\d+").Value, out int height))
                    return false;
                bool cm = hgt.Contains("cm");
                if (cm && (height < 150 || height > 193))
                    return false;
                if (!cm && (height < 59 || height > 76))
                    return false;
                
                if (hcl == null || hcl.Length != 7 || !Regex.IsMatch(hcl, @"\#[a-f0-9]{6}"))
                    return false;
                
                if (ecl == null || !_validEyeColors.Contains(ecl))
                    return false;

                if (pid == null || pid.Length != 9 || !int.TryParse(pid, out int passportId))
                    return false;

                return true;
            }
        }
    }
}