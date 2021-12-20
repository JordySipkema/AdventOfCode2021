namespace AOC2021.Day1 {
    class Program {
        static void Main(string[] args) {
            PartOne();
            PartTwo();
        }

        static void PartOne() {
            Console.WriteLine("Day 1 - Part One");

            int? previous = null;
            int smaller = 0;
            int larger = 0;

            foreach (string line in File.ReadLines("input.txt")) {
                int.TryParse(line, out int value);

                if (previous == null) {
                    previous = value;
                    continue;
                }

                if (value > previous) { larger++; } 
                if (value < previous) { smaller++; }

                previous = value;
            }

            Console.WriteLine($"smaller: {smaller} | larger {larger}");
        }

        static void PartTwo() {
            Console.WriteLine("Day 1 - Part Two");
        }
    }
}
