using AVLConsole.Structures;

namespace AVLConsole.Objects {
    public class Tester<Tree> where Tree : BST<Number, Number>, new() {
        private Tree tree;
        private List<Number> keys;
        private Random random;

        public Tester() {
            tree = new();
            keys = new();
            random = new();
        }

        public void TestFunctions(int count) {
            int insertCount = 0;
            int deleteCount = 0;
            int findCount = 0;

            for (int i = 0; i < count; i++) {
                double rng = random.NextDouble();

                if (rng < 0.5) {
                    int value = random.Next();
                    Number key = new() { Value = value };

                    try {
                        tree.Insert(key, key);

                        keys.Add(key);

                        insertCount++;
                    } catch (InvalidOperationException) {
                        i--;
                        continue;
                    }
                } else if (rng < 0.75) {
                    if (keys.Count == 0) {
                        i--;
                        continue;
                    }

                    int idx = random.Next(keys.Count);
                    Number key = keys[idx];

                    tree.Delete(key, key);

                    int lastIndex = keys.Count - 1;
                    keys[idx] = keys[lastIndex];
                    keys.RemoveAt(lastIndex);

                    try {
                        var match = tree.PointFind(key);

                        if (match != null) {
                            throw new InvalidDataException($"Found matches found for deleted keys: ({key})");
                        }
                    } catch (KeyNotFoundException) { }

                    deleteCount++;
                } else {
                    if (keys.Count == 0) {
                        i--;
                        continue;
                    }

                    Number key = keys[random.Next(keys.Count)];

                    tree.PointFind(key);

                    findCount++;
                }

                int step = Math.Max(1, count / 10);
                if ((i + 1) % step == 0) {
                    Console.Write($"{(i + 1) * 100 / count}%");
                    Console.Write((i + 1) == count ? Environment.NewLine : " - ");
                }
            }

            Console.WriteLine();
            Console.WriteLine(
                $"Stats →\n" +
                $"  Inserts: {insertCount}\n" +
                $"  Deletes: {deleteCount}\n" +
                $"  Finds: {findCount}\n" +
                $"  Node count: {insertCount - deleteCount}"
            );
            Console.WriteLine();

            GetKeys();
        }

        public void Insert(int count) {
            for (int i = 0; i < count; i++) {
                int value = random.Next();
                Number key = new() { Value = value };

                try {
                    tree.Insert(key, key);
                } catch (InvalidOperationException) {
                    i--;
                    continue;
                }

                int step = Math.Max(1, count / 10);
                if ((i + 1) % step == 0) {
                    Console.Write($"{(i + 1) * 100 / count}%");
                    Console.Write((i + 1) == count ? Environment.NewLine : " - ");
                }
            }
        }

        public void Update(int count) {
            if (keys.Count == 0) return;

            for (int i = 0; i < count; i++) {
                int idx = random.Next(keys.Count);
                Number oldKey = keys[idx];
                Number oldData = oldKey;

                int newValue = random.Next();
                Number newKey = new() { Value = newValue };
                Number newData = newKey;

                tree.Update(oldKey, oldData, newKey, newData);

                keys[idx] = newKey;

                int step = Math.Max(1, count / 10);
                if ((i + 1) % step == 0) {
                    Console.Write($"{(i + 1) * 100 / count}%");
                    Console.Write((i + 1) == count ? Environment.NewLine : " - ");
                }
            }
        }

        public void Delete(int count) {
            if (keys.Count == 0) return;

            for (int i = 0; i < count; i++) {
                int idx = random.Next(keys.Count);
                Number key = keys[idx];

                tree.Delete(key, key);

                int lastIndex = keys.Count - 1;
                keys[idx] = keys[lastIndex];
                keys.RemoveAt(lastIndex);

                int step = Math.Max(1, count / 10);
                if ((i + 1) % step == 0) {
                    Console.Write($"{(i + 1) * 100 / count}%");
                    Console.Write((i + 1) == count ? Environment.NewLine : " - ");
                }
            }
        }

        public void PointFind(int count) {
            if (keys.Count == 0) return;

            for (int i = 0; i < count; i++) {
                Number key = keys[random.Next(keys.Count)];
                tree.PointFind(key);

                int step = Math.Max(1, count / 10);
                if ((i + 1) % step == 0) {
                    Console.Write($"{(i + 1) * 100 / count}%");
                    Console.Write((i + 1) == count ? Environment.NewLine : " - ");
                }
            }
        }

        public void IntervalFind(int count) {
            int length = 500;

            if (keys.Count < length) return;

            int n = keys.Count;

            for (int i = 0; i < count; i++) {
                int start = random.Next(0, n - length);
                Number lower = new() { Value = keys[start].Value };
                Number upper = new() { Value = keys[start + length - 1].Value };

                var matches = tree.IntervalFind(lower, upper);
                var expected = keys.GetRange(start, length);

                if (matches.Count != expected.Count || !matches.Select(m => m.KeyData.Value).SequenceEqual(expected.Select(e => e.Value))) {
                    throw new InvalidDataException($"IntervalFind mismatch: expected {expected.Count}, got {matches.Count} (range {lower.Value}-{upper.Value})");
                }

                int step = Math.Max(1, count / 10);
                if ((i + 1) % step == 0) {
                    Console.Write($"{(i + 1) * 100 / count}%");
                    Console.Write((i + 1) == count ? Environment.NewLine : " - ");
                }
            }
        }

        public void GetMinKey(int count) {
            for (int i = 0; i < count; i++) {
                tree.GetMinKey();
            }
        }

        public void GetMaxKey(int count) {
            for (int i = 0; i < count; i++) {
                tree.GetMaxKey();
            }
        }

        public void InOrderTraversal() {
            int count = 0;
            Traversal<Number, Number>.InOrderTraversal(tree, node => count++);
            Console.WriteLine($"Node Count: {tree.NodeCount}, Real Count: {count}");
            Console.WriteLine();
        }

        public void LevelOrderTraversal() {
            int count = 0;
            Traversal<Number, Number>.LevelOrderTraversal(tree, node => count++);
            Console.WriteLine($"Node Count: {tree.NodeCount}, Real Count: {count}");
            Console.WriteLine();
        }

        public void GetBSTInfo() {
            Console.WriteLine($"BST Info: Root = {tree.Root?.KeyData}, Node Count = {tree.NodeCount}");
            Console.WriteLine();
        }

        public void GetKeys() {
            keys.Clear();

            int count = 0;

            Traversal<Number, Number>.InOrderTraversal(tree, node => {
                count++;
                keys.Add(node.NodeData);
            });

            Console.WriteLine($"Node count: {count}");
            Console.WriteLine();
        }

        public void Clear() {
            tree = new();
            keys.Clear();
        }
    }
}
