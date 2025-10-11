using AVLConsole.Structures;
using System.Diagnostics;

namespace AVLConsole.Objects {
    public class TesterBST {
        private BST<Number, Number> bst;

        private List<Number> keyList;
        private SortedSet<Number> keySet;
        private Random random;

        public TesterBST() {
            bst = new();
            keyList = new();
            keySet = new(Comparer<Number>.Create((a, b) => a.Value.CompareTo(b.Value)));
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
                IntervalFind(nodeCount);

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
            if (keySet.Count == 0) return;

            var keys = keySet.ToArray();

            for (int i = 0; i < count; i++) {
                Number key = keys[random.Next(keys.Length)];
                bst.PointFind(key);
            }
        }

        public void IntervalFind(int count) {
            int n = keySet.Count;

            if (n < 500) return;

            var keys = keySet.ToArray();

            for (int i = 0; i < count; i++) {
                int startIndex = random.Next(0, n - 500);
                Number lower = keys[startIndex];
                Number upper = keys[startIndex + 499];

                bst.IntervalFind(lower, upper);

                int step = Math.Max(1, count / 10);

                if ((i + 1) % step == 0) {
                    Console.Write($"{(i + 1) * 100 / count}%");
                    Console.Write((i + 1) == count ? Environment.NewLine : " - ");
                }
            }
        }

        public void Delete(int count) {
            if (keySet.Count == 0) return;

            var keys = keySet.ToArray();

            for (int i = 0; i < count; i++) {
                Number key = keys[random.Next(keys.Length)];
                bst.Delete(key, key);
                keySet.Remove(key);
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
            int count = 0;
            Traversal<Number, Number>.InOrderTraversal(bst, node => count++);
            Console.WriteLine($"Node Count: {bst.NodeCount}, Real Count: {count}\n");
            Console.WriteLine($"Key set count: {keySet.Count}");
        }

        public void LevelOrderTraversal() {
            int count = 0;
            Traversal<Number, Number>.LevelOrderTraversal(bst, node => count++);
            Console.WriteLine($"Node Count: {bst.NodeCount}, Real Count: {count}\n");
        }

        public void GetBSTInfo() {
            Console.WriteLine($"BST Info: Root = {bst.Root?.KeyData}, Node Count = {bst.NodeCount}");
        }

        public void Clear() {
            bst = new();
            keySet.Clear();
        }

        public void SortKeyList() {
            keyList.ForEach(k => keySet.Add(k));
        }
    }
}
