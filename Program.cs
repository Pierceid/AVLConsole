using AVLConsole.Objects;
using AVLConsole.Structures;
using System.Diagnostics;

namespace AVLConsole {
    internal class Program {
        private const int functionsCount = 1_000_000;
        private const int insertCount = 10_000_000;
        private const int updateCount = 2_000_000;
        private const int deleteCount = 2_000_000;
        private const int pointFindCount = 5_000_000;
        private const int intervalFindCount = 1_000_000;
        private const int minCount = 2_000_000;
        private const int maxCount = 2_000_000;

        static void Main(string[] args) {
            Tester<AVL<Number, Number>> tester = new();

            // FUNCTIONS TEST
            Benchmark(() => tester.TestFunctions(functionsCount), "===== FUNCTIONS TEST =====", $"Functions ({functionsCount})");

            // INSERT TEST
            //Benchmark(() => tester.Insert(insertCount), "===== INSERT TEST =====", $"Insert ({insertCount})");

            // GET KEYS
            //tester.GetKeys();

            // UPDATE TEST
            //Benchmark(() => tester.Update(updateCount), "===== UPDATE TEST =====", $"Update ({updateCount})");

            // DELETE TEST
            //Benchmark(() => tester.Delete(deleteCount), "===== DELETE TEST =====", $"Delete ({deleteCount})");

            // GET KEYS
            //tester.GetKeys();

            // POINT FIND TEST
            //Benchmark(() => tester.PointFind(pointFindCount), "===== POINT FIND TEST =====", $"Point find ({pointFindCount})");

            // INTERVAL FIND TEST
            //Benchmark(() => tester.IntervalFind(intervalFindCount), "===== INTERVAL FIND TEST =====", $"Interval find ({intervalFindCount})");

            // MIN TEST
            //Benchmark(() => tester.GetMinKey(minCount), "===== MIN TEST =====", $"Find min ({minCount})");

            // MAX TEST
            //Benchmark(() => tester.GetMaxKey(maxCount), "===== MAX TEST =====", $"Find max ({maxCount})");

            tester.InOrderTraversal();
        }

        static void Benchmark(Action action, string title, string description) {
            Console.WriteLine(title);

            Stopwatch stopwatch = new();
            stopwatch.Start();
            action();
            stopwatch.Stop();

            Console.WriteLine($"{description}: {stopwatch.Elapsed.TotalSeconds:F2} s");
            Console.WriteLine();
        }
    }
}
