using AVLConsole.Objects;
using System.Diagnostics;

namespace AVLConsole {
    internal class Program {
        static void Main(string[] args) {
            //Generator generator = new();
            //generator.InsertBST(1000);
            //generator.FindBST(1000);
            //generator.DeleteBST(100);
            //generator.InOrderTraversalBST();
            //generator.LevelOrderTraversalBST();

            Stopwatch stopwatch = new();
            Tester tester = new();

            stopwatch.Start();
            tester.InsertBST(10_000_000);
            Console.WriteLine($"Insert: {stopwatch.Elapsed}");

            stopwatch.Stop();
            //stopwatch.Restart();
            //tester.DeleteBST(2_000_000);
            //Console.WriteLine($"Delete: {stopwatch.Elapsed}");

            //stopwatch.Restart();
            //tester.FindBST(5_000_000);
            //Console.WriteLine($"Find: {stopwatch.Elapsed}");

            tester.InOrderTraversalBST();

            //tester.LevelOrderTraversalBST();
        }
    }
}
