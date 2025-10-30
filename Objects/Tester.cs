using AVLConsole.Entities;
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
                    double value = random.Next();
                    Number key = new() { Value = value };

                    try {
                        tree.Insert(key, key);

                        if (tree is AVL<Number, Number>) {
                            CheckTreeBalance();
                        }

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

                    if (tree is AVL<Number, Number>) {
                        CheckTreeBalance();
                    }

                    int lastIndex = keys.Count - 1;
                    keys[idx] = keys[lastIndex];
                    keys.RemoveAt(lastIndex);

                    try {
                        var match = tree.PointFind(key);

                        if (match != null) {
                            throw new InvalidDataException($"Found match for deleted keys: ({key})");
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

        public void Insert(int count, bool rng) {
            for (int i = 0; i < count; i++) {
                double value = rng ? random.Next() : i;
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

                double newValue = random.Next();
                Number newKey = new() { Value = newValue };
                Number newData = newKey;

                try {
                    var match = tree.PointFind(newKey);

                    if (match != null) {
                        i--;
                        continue;
                    }
                } catch (KeyNotFoundException) { }

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
                var foundMin = tree.GetMinKey();
                var actualMin = keys[0];

                if (foundMin != actualMin) {
                    throw new InvalidDataException($"Found min doesnt match actual min: ({foundMin}) - ({actualMin})");
                }
            }
        }

        public void GetMaxKey(int count) {
            for (int i = 0; i < count; i++) {
                var foundMax = tree.GetMaxKey();
                var actualMax = keys[^1];

                if (foundMax != actualMax) {
                    throw new InvalidDataException($"Found max doesnt match actual max: ({foundMax}) - ({actualMax})");
                }
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

        public void GetKeys() {
            keys.Clear();

            int count = 0;

            Traversal<Number, Number>.InOrderTraversal(tree, node => {
                count++;
                keys.Add(node.KeyData);
            });

            Console.WriteLine($"Node count: {count}");
            Console.WriteLine();
        }

        public void Clear() {
            tree = new();
            keys.Clear();
        }

        private int GetHeight(BSTNode<Number, Number>? root) {
            if (root == null) return 0;

            Stack<BSTNode<Number, Number>> stack = new();
            Dictionary<BSTNode<Number, Number>, int> heightMap = new();

            stack.Push(root);

            while (stack.Count > 0) {
                BSTNode<Number, Number> node = stack.Peek();

                bool leftDone = node.LeftSon == null || heightMap.ContainsKey(node.LeftSon);
                bool rightDone = node.RightSon == null || heightMap.ContainsKey(node.RightSon);

                if (leftDone && rightDone) {
                    stack.Pop();
                    int leftHeight = node.LeftSon != null ? heightMap[node.LeftSon] : 0;
                    int rightHeight = node.RightSon != null ? heightMap[node.RightSon] : 0;
                    heightMap[node] = Math.Max(leftHeight, rightHeight) + 1;
                } else {
                    if (node.RightSon != null && !heightMap.ContainsKey(node.RightSon)) {
                        stack.Push(node.RightSon);
                    }

                    if (node.LeftSon != null && !heightMap.ContainsKey(node.LeftSon)) {
                        stack.Push(node.LeftSon);
                    }
                }
            }

            return heightMap[root];
        }

        private void CheckTreeBalance() {
            if (tree.Root == null) return;

            bool valid = ValidateBalance(tree.Root);

            if (!valid) {
                throw new InvalidDataException("AVL balance validation failed!");
            }
        }

        private bool ValidateBalance(BSTNode<Number, Number>? root) {
            if (root == null) return true;

            Stack<BSTNode<Number, Number>> stack = new();
            stack.Push(root);

            while (stack.Count > 0) {
                var node = stack.Pop();

                int leftHeight = GetHeight(node.LeftSon);
                int rightHeight = GetHeight(node.RightSon);
                int expectedBF = rightHeight - leftHeight;

                if (node is AVLNode<Number, Number> avlNode) {
                    if (avlNode.BalanceFactor != expectedBF) {
                        Console.WriteLine($"BF mismatch at node {avlNode.KeyData.Value}: Expected {expectedBF}, Found {avlNode.BalanceFactor}");
                        return false;
                    }

                    if (Math.Abs(avlNode.BalanceFactor) > 1) {
                        Console.WriteLine($"Invalid BF at node {avlNode.KeyData.Value}: {avlNode.BalanceFactor}");
                        return false;
                    }
                }

                if (node.LeftSon != null) stack.Push(node.LeftSon);

                if (node.RightSon != null) stack.Push(node.RightSon);
            }

            return true;
        }
    }
}
