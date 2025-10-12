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

            for (int i = 0; i < repCount; i++) {
                Delete(nodeCount);

                int step = Math.Max(1, repCount / 10);
                if ((i + 1) % step == 0) {
                    Console.Write($"{(i + 1) * 100 / repCount}%");
                    Console.Write((i + 1) == repCount ? Environment.NewLine : " - ");
                }

                Clear();
            }

            stopwatch.Stop();
            Console.WriteLine($"Delete Cycle ({repCount}): {stopwatch.ElapsedMilliseconds / repCount} ms");
        }

        public void Insert(int count) {
            for (int i = 0; i < count; i++) {
                int value = random.Next();
                Number key = new() { Value = value };

                bst.Insert(key, key);

                int step = Math.Max(1, count / 10);
                if ((i + 1) % step == 0) {
                    Console.Write($"{(i + 1) * 100 / count}%");
                    Console.Write((i + 1) == count ? Environment.NewLine : " - ");
                }
            }
        }

        public void PointFind(int count) {
            if (keyList.Count == 0) return;

            for (int i = 0; i < count; i++) {
                Number key = keyList[random.Next(keyList.Count)];
                bst.PointFind(key);

                int step = Math.Max(1, count / 10);
                if ((i + 1) % step == 0) {
                    Console.Write($"{(i + 1) * 100 / count}%");
                    Console.Write((i + 1) == count ? Environment.NewLine : " - ");
                }
            }
        }

        public void IntervalFind(int count) {
            if (keyList.Count < 500) return;

            int n = keyList.Count;

            for (int i = 0; i < count; i++) {
                int start = random.Next(0, n - 500);
                Number lower = new() { Value = keyList[start].Value };
                Number upper = new() { Value = keyList[start + 499].Value };

                bst.IntervalFind(lower, upper);

                int step = Math.Max(1, count / 10);
                if ((i + 1) % step == 0) {
                    Console.Write($"{(i + 1) * 100 / count}%");
                    Console.Write((i + 1) == count ? Environment.NewLine : " - ");
                }
            }
        }

        public void Delete(int count) {
            if (keyList.Count == 0) return;

            for (int i = 0; i < count; i++) {
                int idx = random.Next(keyList.Count);
                Number key = keyList[idx];

                bst.Delete(key, key);

                int lastIndex = keyList.Count - 1;
                keyList[idx] = keyList[lastIndex];
                keyList.RemoveAt(lastIndex);

                int step = Math.Max(1, count / 10);
                if ((i + 1) % step == 0) {
                    Console.Write($"{(i + 1) * 100 / count}%");
                    Console.Write((i + 1) == count ? Environment.NewLine : " - ");
                }
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
            Console.WriteLine($"Node Count: {bst.NodeCount}, Real Count: {count}");
            Console.WriteLine();
        }

        public void LevelOrderTraversal() {
            int count = 0;
            Traversal<Number, Number>.LevelOrderTraversal(bst, node => count++);
            Console.WriteLine($"Node Count: {bst.NodeCount}, Real Count: {count}");
            Console.WriteLine();
        }

        public void GetBSTInfo() {
            Console.WriteLine($"BST Info: Root = {bst.Root?.KeyData}, Node Count = {bst.NodeCount}");
            Console.WriteLine();
        }

        public void Clear() {
            bst = new();
            keyList.Clear();
        }

        public void GetKeys() {
            keyList.Clear();

            int count = 0;

            Traversal<Number, Number>.InOrderTraversal(bst, node => {
                count++;
                keyList.Add(node.NodeData);
            });

            Console.WriteLine($"Node count: {count}");
            Console.WriteLine();
        }
    }
}
