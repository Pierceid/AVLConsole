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
            if (Root == null) {
                Root = new(keys, data);
                NodeCount++;
                return;
            }

            BSTNode<K, T>? current = Root;

            while (true) {
                int cmp = keys.Compare(current.KeyData);

                if (cmp == 1) {
                    // new key > current key
                    if (current.RightSon == null) {
                        current.RightSon = new(keys, data) { Parent = current };
                        NodeCount++;
                        return;
                    }

                    current = current.RightSon;
                } else {
                    // new key <= current key
                    if (current.LeftSon == null) {
                        current.LeftSon = new(keys, data) { Parent = current };
                        NodeCount++;
                        return;
                    }

                    current = current.LeftSon;
                }
            }
        }

        public List<BSTNode<K, T>> PointFind(K keys) {
            List<BSTNode<K, T>> matches = new();
            BSTNode<K, T>? current = Root;

            while (current != null) {
                int cmp = keys.Compare(current.KeyData);

                if (cmp == 1) {
                    // key to find > current key
                    current = current.RightSon;
                } else if (cmp == -1) {
                    // key to find < current key
                    current = current.LeftSon;
                } else {
                    // key to find = current key
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
            BSTNode<K, T>? current = Root;

            while (stack.Count > 0 || current != null) {
                while (current != null) {
                    int cmpLower = current.KeyData.Compare(lower);

                    if (cmpLower == -1) {
                        // if current key < lower bound then skip left subtree
                        current = current.RightSon;
                    } else {
                        // if current key >= lower bound then it could contain valid nodes
                        stack.Push(current);
                        current = current.LeftSon;
                    }
                }

                if (stack.Count == 0) break;

                current = stack.Pop();

                int cmpLowerBound = current.KeyData.Compare(lower);
                int cmpUpperBound = current.KeyData.Compare(upper);

                if (cmpLowerBound >= 0 && cmpUpperBound <= 0) {
                    matches.Add(current);
                }

                // if current key > upper bound then stop
                if (cmpUpperBound == 1) break;

                // else continue right
                current = current.RightSon;
            }

            if (matches.Count == 0) {
                throw new KeyNotFoundException($"No matches found for interval: ({lower.GetKeys()} , {upper.GetKeys()})");
            }

            return matches;
        }

        public void Delete(K keys, T data) {
            if (Root == null) {
                Console.WriteLine("Tree is empty");
                return;
            }

            BSTNode<K, T>? current = Root;
            BSTNode<K, T>? parent = null;

            while (current != null) {
                int cmp = keys.Compare(current.KeyData);

                if (cmp == 0 && current.NodeData.EqualsByID(data)) break;

                parent = current;
                current = (cmp == 1) ? current.RightSon : current.LeftSon;
            }

            if (current == null) return;

            // node has 0 or 1 child
            if (current.LeftSon == null || current.RightSon == null) {
                BSTNode<K, T>? child = current.LeftSon ?? current.RightSon;

                if (parent == null) {
                    Root = child;
                } else if (parent.LeftSon == current) {
                    parent.LeftSon = child;
                } else {
                    parent.RightSon = child;
                }

                if (child != null) {
                    child.Parent = parent;
                }

                NodeCount--;

                return;
            }

            // node has 2 children
            BSTNode<K, T>? predParent = current;
            BSTNode<K, T>? pred = current.LeftSon;

            while (pred.RightSon != null) {
                predParent = pred;
                pred = pred.RightSon;
            }

            // copy predecessor data into current
            current.KeyData = pred.KeyData;
            current.NodeData = pred.NodeData;

            // delete predecessor node
            BSTNode<K, T>? predChild = pred.LeftSon;

            if (predParent.LeftSon == pred) {
                predParent.LeftSon = predChild;
            } else {
                predParent.RightSon = predChild;
            }

            if (predChild != null) {
                predChild.Parent = predParent;
            }

            NodeCount--;
        }

        public void UpdateNode(K oldKeys, T oldData, K newKeys, T newData) {
            if (Root == null) {
                Console.WriteLine("Tree is empty");
                return;
            }

            BSTNode<K, T>? nodeToUpdate = PointFind(oldKeys).FirstOrDefault(p => p.NodeData.EqualsByID(oldData));

            if (nodeToUpdate == null) return;

            bool keysChanged = !oldKeys.Equals(newKeys);

            if (!keysChanged) {
                nodeToUpdate.NodeData = newData;
            } else {
                Delete(oldKeys, oldData);
                Insert(newKeys, newData);
            }
        }

        public K? GetMinKey() {
            if (Root == null) {
                Console.WriteLine("Tree is empty");
                return default;
            }

            BSTNode<K, T>? current = Root;

            while (current.LeftSon != null) {
                current = current.LeftSon;
            }

            return current.KeyData;
        }

        public K? GetMaxKey() {
            if (Root == null) {
                Console.WriteLine("Tree is empty");
                return default;
            }

            BSTNode<K, T>? current = Root;

            while (current.RightSon != null) {
                current = current.RightSon;
            }

            return current.KeyData;
        }

        public void InOrderTraversal() {
            int index = 0;
            Traversal<K, T>.InOrderTraversal(this, node => {
                Console.WriteLine($"{++index}. {node.KeyData.GetKeys()} - {node.NodeData.GetInfo()}");
            });
            Console.WriteLine($"\nNode Count: {NodeCount}, Real Count: {index}\n");
        }

        public void LevelOrderTraversal() {
            int index = 0;
            Traversal<K, T>.LevelOrderTraversal(this, node => {
                Console.WriteLine($"{++index}. {node.KeyData.GetKeys()} - {node.NodeData.GetInfo()}");
            });
            Console.WriteLine($"\nNode Count: {NodeCount}, Real Count: {index}\n");
        }
    }
}
