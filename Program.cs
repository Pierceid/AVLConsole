using AVLConsole.Objects;
using System.Diagnostics;

namespace AVLConsole {
    internal class Program {
        private const int insertCount = 10_000_000;
        private const int deleteCount = 1_000_000;
        private const int pointFindCount = 5_000_000;
        private const int intervalFindCount = 1_000_000;
        private const int minCount = 2_000_000;
        private const int maxCount = 2_000_000;

        static void Main(string[] args) {
            TesterBST testerBST = new();

            // INSERTION TEST
            Benchmark(() => testerBST.Insert(insertCount), "===== INSERTION TEST =====", $"Insert ({insertCount})");
            testerBST.SortKeyList();

            // DELETE TEST
            //Benchmark(() => testerBST.Delete(deleteCount), "===== DELETE TEST =====", $"Delete ({deleteCount})");

            // POINT FIND TEST
            Benchmark(() => testerBST.PointFind(pointFindCount), "===== POINT FIND TEST =====", $"Point find ({pointFindCount})");

            // INTERVAL FIND TEST
            //Benchmark(() => testerBST.IntervalFind(intervalFindCount), "===== INTERVAL FIND TEST =====", $"Interval find ({intervalFindCount})");

            // MIN TEST
            Benchmark(() => testerBST.GetMinKey(minCount), "===== MIN TEST =====", $"Find min ({minCount})");

            // MAX TEST
            Benchmark(() => testerBST.GetMaxKey(maxCount), "===== MAX TEST =====", $"Find max ({maxCount})");
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
