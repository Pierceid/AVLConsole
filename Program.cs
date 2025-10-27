using AVLConsole.Entities;
using AVLConsole.Objects;
using AVLConsole.Structures;
using System.Diagnostics;

namespace AVLConsole {
    internal class Program {
        private const int functionsCount = 10_000;
        private const int insertCount = 10_000_000;
        private const int updateCount = 2_000_000;
        private const int deleteCount = 2_000_000;
        private const int pointFindCount = 5_000_000;
        private const int intervalFindCount = 1_000_000;
        private const int minCount = 2_000_000;
        private const int maxCount = 2_000_000;

        static void Main(string[] args) {
            //Tester<BST<Number, Number>> tester = new();
            Tester<AVL<Number, Number>> tester = new();
            //C5Tester tester = new();

            // FUNCTIONS TEST
            //Benchmark(() => tester.TestFunctions(functionsCount), "===== FUNCTIONS TEST =====", $"Functions ({functionsCount})", functionsCount);

            // INSERT TEST
            Benchmark(() => tester.Insert(insertCount, true), "===== INSERT TEST (random) =====", $"Insert ({insertCount})", insertCount);

            // INSERT TEST
            //Benchmark(() => tester.Insert(insertCount, false), "===== INSERT TEST (in order) =====", $"Insert ({insertCount})", insertCount);

            // GET KEYS
            tester.GetKeys();

            // UPDATE TEST
            //Benchmark(() => tester.Update(updateCount), "===== UPDATE TEST =====", $"Update ({updateCount})", updateCount);

            //// DELETE TEST
            Benchmark(() => tester.Delete(deleteCount), "===== DELETE TEST =====", $"Delete ({deleteCount})", deleteCount);

            //// GET KEYS
            tester.GetKeys();

            //// POINT FIND TEST
            Benchmark(() => tester.PointFind(pointFindCount), "===== POINT FIND TEST =====", $"Point find ({pointFindCount})", pointFindCount);

            //// INTERVAL FIND TEST
            Benchmark(() => tester.IntervalFind(intervalFindCount), "===== INTERVAL FIND TEST =====", $"Interval find ({intervalFindCount})", intervalFindCount);

            //// MIN TEST
            Benchmark(() => tester.GetMinKey(minCount), "===== MIN TEST =====", $"Find min ({minCount})", minCount);

            //// MAX TEST
            Benchmark(() => tester.GetMaxKey(maxCount), "===== MAX TEST =====", $"Find max ({maxCount})", maxCount);

            //tester.LevelOrderTraversal();
        }

        static void Benchmark(Action action, string title, string description, int count) {
            Console.WriteLine(title);

            Stopwatch stopwatch = new();
            stopwatch.Start();
            action();
            stopwatch.Stop();

            Console.WriteLine($"{description}: {stopwatch.Elapsed.TotalSeconds:F2} s");
            Console.WriteLine($"1 operation: {(stopwatch.Elapsed.TotalMicroseconds / count):F2} us");
            Console.WriteLine();
        }
    }
}
