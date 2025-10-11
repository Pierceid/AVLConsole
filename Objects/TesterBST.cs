using AVLConsole.Structures;
using System.Diagnostics;

namespace AVLConsole.Objects {
    public class TesterBST {
        private BST<Number, Number> bst;
        private List<Number> keyList;
        private Random random;

        public TesterBST() {
            bst = new();
            keyList = new();
            random = new();
        }

        public void InsertCycle(int repCount, int nodeCount) {
            Stopwatch stopwatch = new();

            stopwatch.Start();

            for (int i = 0; i < repCount; i++) {
                Insert(nodeCount);

                int step = Math.Max(1, repCount / 10);

                if ((i + 1) % step == 0) {
                    Console.Write($"{(i + 1) * 100 / repCount}%");
                    Console.Write((i + 1) == repCount ? Environment.NewLine : " - ");
                }

                Clear();
            }

            stopwatch.Stop();

            Console.WriteLine($"Insert Cycle ({repCount}): {stopwatch.ElapsedMilliseconds / repCount} ms");
        }

        public void PointFindCycle(int repCount, int nodeCount) {
            Stopwatch stopwatch = new();

            stopwatch.Start();

            for (int i = 0; i < repCount; i++) {
                PointFind(nodeCount);

                int step = Math.Max(1, repCount / 10);

                if ((i + 1) % step == 0) {
                    Console.Write($"{(i + 1) * 100 / repCount}%");
                    Console.Write((i + 1) == repCount ? Environment.NewLine : " - ");
                }

                Clear();
            }

            stopwatch.Stop();

            Console.WriteLine($"Point Find Cycle ({repCount}): {stopwatch.ElapsedMilliseconds / repCount} ms");
        }

        public void IntervalFindCycle(int repCount, int nodeCount) {
            Stopwatch stopwatch = new();

            stopwatch.Start();

            for (int i = 0; i < repCount; i++) {
                PointFind(nodeCount);

                int step = Math.Max(1, repCount / 10);

                if ((i + 1) % step == 0) {
                    Console.Write($"{(i + 1) * 100 / repCount}%");
                    Console.Write((i + 1) == repCount ? Environment.NewLine : " - ");
                }

                Clear();
            }

            stopwatch.Stop();

            Console.WriteLine($"Interval Find Cycle ({repCount}): {stopwatch.ElapsedMilliseconds / repCount} ms");
        }

        public void DeleteCycle(int repCount, int nodeCount) {
            Stopwatch stopwatch = new();

            stopwatch.Start();

            var tree = bst;

            for (int i = 0; i < repCount; i++) {
                Delete(nodeCount);

                int step = Math.Max(1, repCount / 10);

                if ((i + 1) % step == 0) {
                    Console.Write($"{(i + 1) * 100 / repCount}%");
                    Console.Write((i + 1) == repCount ? Environment.NewLine : " - ");
                }

                bst = tree;
            }

            stopwatch.Stop();

            Console.WriteLine($"Delete Cycle ({repCount}): {stopwatch.ElapsedMilliseconds / repCount} ms");
        }

        public void Insert(int count) {
            for (int i = 0; i < count; i++) {
                int value = random.Next();
                Number key = new() { Value = value };

                bst.Insert(key, key);
                keyList.Add(key);
            }
        }

        public void PointFind(int count) {
            for (int i = 0; i < count; i++) {
                Number key = keyList.ElementAt(random.Next(keyList.Count));

                bst.PointFind(key);
            }
        }

        public void IntervalFind(int count) {
            int n = keyList.Count;

            if (n < 500) return;

            for (int i = 0; i < count; i++) {
                int startIndex = random.Next(0, n - 500);

                Number lower = keyList[startIndex];
                Number upper = keyList[startIndex + 499];

                bst.IntervalFind(lower, upper);
            }
        }

        public void Delete(int count) {
            for (int i = 0; i < count; i++) {
                int index = random.Next(keyList.Count);
                Number key = keyList.ElementAt(index);

                bst.Delete(key, key);
                keyList.Remove(key);
            }
        }

        public void GetMinKey(int count) {
            for (int i = 0; i < count; i++) {
                bst.GetMinKey();
            }
        }

        public void GetMaxKey(int count) {
            for (int i = 0; i < count; i++) {
                bst.GetMaxKey();
            }
        }

        public void InOrderTraversal() {
            //bst.InOrderTraversal();
            int count = 0;
            Traversal<Number, Number>.InOrderTraversal(bst, node => count++);
            Console.WriteLine($"Node Count: {bst.NodeCount}, Real Count: {count}\n");
        }

        public void LevelOrderTraversal() {
            //bst.LevelOrderTraversal();
            int count = 0;
            Traversal<Number, Number>.LevelOrderTraversal(bst, node => count++);
            Console.WriteLine($"Node Count: {bst.NodeCount}, Real Count: {count}\n");
        }

        public void GetBSTInfo() {
            Console.WriteLine($"BST Info: Root = {bst.Root?.KeyData}, Node Count = {bst.NodeCount}");
        }

        public void Clear() {
            bst = new();
            keyList.Clear();
        }

        public void SortKeyList() {
            keyList = keyList.OrderBy(k => k.Value).DistinctBy(k => k.Value).ToList();
        }
    }
}
