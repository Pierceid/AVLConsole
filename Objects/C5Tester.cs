using C5;

namespace AVLConsole.Objects {
    public class C5Number() : Number, IComparable<Number> {
        public int CompareTo(Number? other) {
            return Util.CompareIntegers(Value, other?.Value ?? 0);
        }
    }

    public class C5Tester {
        private TreeDictionary<C5Number, C5Number> tree;
        private List<C5Number> keys;
        private Random random;

        public C5Tester() {
            tree = new();
            keys = new();
            random = new();
        }

        public void Insert(int count) {
            for (int i = 0; i < count; i++) {
                int value = random.Next();
                C5Number key = new() { Value = value };

                try {
                    tree.Add(key, key);
                } catch (Exception) {
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

        public void Delete(int count) {
            if (keys.Count == 0) return;

            for (int i = 0; i < count; i++) {
                int idx = random.Next(keys.Count);
                C5Number key = keys[idx];

                tree.Remove(key);

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
                C5Number key = keys[random.Next(keys.Count)];

                if (!tree.Contains(key)) {
                    throw new KeyNotFoundException($"No matches found for keys: ({key.GetKeys()})");
                }

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
                C5Number lower = new() { Value = keys[start].Value };
                C5Number upper = new() { Value = keys[start + length].Value };

                var matches = tree.RangeFromTo(lower, upper).ToList();
                var expected = keys.GetRange(start, length);

                if (matches.Count != expected.Count || !matches.Select(m => m.Key.Value).SequenceEqual(expected.Select(e => e.Value))) {
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
                var foundMin = tree.FindMin().Value;
                var actualMin = keys[0];

                if (foundMin != actualMin) {
                    throw new InvalidDataException($"Found min doesnt match actual min: ({foundMin}) - ({actualMin})");
                }
            }
        }

        public void GetMaxKey(int count) {
            for (int i = 0; i < count; i++) {
                var foundMax = tree.FindMax().Value;
                var actualMax = keys[^1];

                if (foundMax != actualMax) {
                    throw new InvalidDataException($"Found max doesnt match actual max: ({foundMax}) - ({actualMax})");
                }
            }
        }

        public void GetKeys() {
            keys = tree.Keys?.ToList() ?? new();
            keys.Sort((a, b) => a.Value.CompareTo(b.Value));

            Console.WriteLine($"Node count: {keys.Count}");
            Console.WriteLine();
        }

        public void Clear() {
            tree = new();
            keys.Clear();
        }
    }
}
