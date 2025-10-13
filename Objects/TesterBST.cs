using AVLConsole.Structures;

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
                        bst.Insert(key, key);

                        keyList.Add(key);

                        insertCount++;
                    } catch (InvalidOperationException) {
                        i--;
                        continue;
                    }
                } else if (rng < 0.75) {
                    if (keyList.Count == 0) {
                        i--;
                        continue;
                    }

                    int idx = random.Next(keyList.Count);
                    Number key = keyList[idx];

                    bst.Delete(key, key);

                    int lastIndex = keyList.Count - 1;
                    keyList[idx] = keyList[lastIndex];
                    keyList.RemoveAt(lastIndex);

                    try {
                        var match = bst.PointFind(key);

                        if (match != null) {
                            throw new InvalidDataException($"Found matches found for deleted keys: ({key})");
                        }
                    } catch (KeyNotFoundException) { }

                    deleteCount++;
                } else {
                    if (keyList.Count == 0) {
                        i--;
                        continue;
                    }

                    Number key = keyList[random.Next(keyList.Count)];

                    bst.PointFind(key);

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
                    bst.Insert(key, key);
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
            if (keyList.Count == 0) return;

            for (int i = 0; i < count; i++) {
                int idx = random.Next(keyList.Count);
                Number oldKey = keyList[idx];
                Number oldData = oldKey;

                int newValue = random.Next();
                Number newKey = new() { Value = newValue };
                Number newData = newKey;

                bst.Update(oldKey, oldData, newKey, newData);
                keyList[idx] = newKey;

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
            int length = 500;

            if (keyList.Count < length) return;

            int n = keyList.Count;

            for (int i = 0; i < count; i++) {
                int start = random.Next(0, n - length);
                Number lower = new() { Value = keyList[start].Value };
                Number upper = new() { Value = keyList[start + length - 1].Value };

                var matches = bst.IntervalFind(lower, upper);
                var expected = keyList.GetRange(start, length);

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

        public void Clear() {
            bst = new();
            keyList.Clear();
        }
    }
}
