using AVLConsole.Entities;
using AVLConsole.Objects;

namespace AVLConsole.Structures {
    public class BST<K, T> where K : IKey<K> where T : Item {
        public BSTNode<K, T>? Root { get; set; }
        public int NodeCount { get; set; }

        public BST() {
            Root = null;
            NodeCount = 0;
        }

        public void Insert(K keys, T data) {
            NodeCount++;

            if (Root == null) {
                Root = new(keys, data);
                return;
            } else {
                var current = Root;

                while (true) {
                    int cmp = keys.Compare(current.KeyData);

                    if (cmp == 1) {
                        if (current.RightSon == null) {
                            current.RightSon = new(keys, data) { Parent = current };
                            return;
                        } else {
                            current = current.RightSon;
                        }
                    } else {
                        if (current.LeftSon == null) {
                            current.LeftSon = new(keys, data) { Parent = current };
                            return;
                        } else {
                            current = current.LeftSon;
                        }
                    }
                }
            }
        }

        public List<BSTNode<K, T>> PointFind(K keys) {
            List<BSTNode<K, T>> matches = new();
            var current = Root;

            while (current != null) {
                int cmp = keys.Compare(current.KeyData);

                if (cmp == 1) {
                    current = current.RightSon;
                } else if (cmp == -1) {
                    current = current.LeftSon;
                } else {
                    matches.Add(current);
                    current = current.LeftSon;
                }
            }

            if (matches.Count == 0) {
                throw new KeyNotFoundException($"No matches found for keys: ({keys.GetKeys()})");
            }

            return matches;
        }

        public List<BSTNode<K, T>> IntervalFind(K lower, K upper) {
            List<BSTNode<K, T>> matches = new();
            Stack<BSTNode<K, T>> stack = new();
            var current = Root;

            while (stack.Count > 0 || current != null) {
                while (current != null) {
                    // skip left subtree if all keys < lower
                    if (current.KeyData.Compare(lower) == -1) {
                        current = current.RightSon;
                        continue;
                    }

                    stack.Push(current);
                    current = current.LeftSon;
                }

                if (stack.Count == 0) break;

                current = stack.Pop();

                if (current.KeyData.Compare(lower) >= 0 && current.KeyData.Compare(upper) <= 0) {
                    matches.Add(current);
                }

                if (current.KeyData.Compare(upper) == 1) {
                    break;
                }

                current = current.RightSon;
            }

            if (matches.Count == 0) {
                throw new KeyNotFoundException($"No matches found for interval ({lower.GetKeys()} , {upper.GetKeys()})");
            }

            return matches;
        }

        public void Delete(K keys, T data) {
            if (Root == null) {
                Console.WriteLine("Tree is empty");
                return;
            }
        }

        public void InOrderTraversal() {
            int index = 0;
            Traversal<K, T>.LevelOrderTraversal(this, node => {
                Console.WriteLine($"{++index}. {node.KeyData.GetKeys()} - {node.NodeData.GetInfo()}");
            });
            Console.WriteLine($"\nNode Count: {NodeCount}\n");
        }

        public void LevelOrderTraversal() {
            int index = 0;
            Traversal<K, T>.LevelOrderTraversal(this, node => {
                Console.WriteLine($"{++index}. {node.KeyData.GetKeys()} - {node.NodeData.GetInfo()}");
            });
            Console.WriteLine($"\nNode Count: {NodeCount}\n");
        }
    }
}
