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

        public virtual void Insert(K keys, T data) {
            BSTNode<K, T> nodeToInsert = new(keys, data);

            if (Root == null) {
                Root = nodeToInsert;
                NodeCount++;
                return;
            }

            BSTNode<K, T>? current = Root;

            while (true) {
                int cmp = keys.Compare(current.KeyData);

                if (cmp == -1) {
                    if (current.LeftSon == null) {
                        nodeToInsert.Parent = current;
                        current.LeftSon = nodeToInsert;
                        NodeCount++;
                        return;
                    }

                    current = current.LeftSon;
                } else if (cmp == 1) {
                    if (current.RightSon == null) {
                        nodeToInsert.Parent = current;
                        current.RightSon = nodeToInsert;
                        NodeCount++;
                        return;
                    }

                    current = current.RightSon;
                } else {
                    throw new InvalidOperationException($"Duplicate key insertion attempted: ({keys.GetKeys()})");
                }
            }
        }

        public virtual void Update(K oldKeys, T oldData, K newKeys, T newData) {
            if (Root == null) {
                Console.WriteLine("Tree is empty");
                return;
            }

            BSTNode<K, T>? nodeToUpdate = PointFind(oldKeys);

            if (nodeToUpdate == null) return;

            bool keysChanged = oldKeys.Compare(newKeys) != 0;

            if (keysChanged) {
                Delete(oldKeys, oldData);
                Insert(newKeys, newData);
            } else {
                nodeToUpdate.NodeData = newData;
            }
        }

        public virtual void Delete(K keys, T data) {
            if (Root == null) {
                Console.WriteLine("Tree is empty");
                return;
            }

            BSTNode<K, T>? nodeToDelete = PointFind(keys);
            BSTNode<K, T>? parent = nodeToDelete.Parent;

            if (nodeToDelete == null) return;

            if (nodeToDelete.LeftSon == null || nodeToDelete.RightSon == null) {
                BSTNode<K, T>? child = nodeToDelete.LeftSon ?? nodeToDelete.RightSon;

                if (parent == null) {
                    Root = child;
                } else if (parent.LeftSon == nodeToDelete) {
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

            BSTNode<K, T>? predParent = nodeToDelete;
            BSTNode<K, T>? pred = nodeToDelete.LeftSon;

            while (pred.RightSon != null) {
                predParent = pred;
                pred = pred.RightSon;
            }

            nodeToDelete.KeyData = pred.KeyData;
            nodeToDelete.NodeData = pred.NodeData;

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

        public virtual BSTNode<K, T> PointFind(K keys) {
            BSTNode<K, T>? current = Root;

            while (current != null) {
                int cmp = keys.Compare(current.KeyData);

                if (cmp == -1) {
                    current = current.LeftSon;
                } else if (cmp == 1) {
                    current = current.RightSon;
                } else {
                    return current;
                }
            }

            throw new KeyNotFoundException($"No matches found for keys: ({keys.GetKeys()})");
        }

        public virtual List<BSTNode<K, T>> IntervalFind(K lower, K upper) {
            List<BSTNode<K, T>> matches = new();
            Stack<BSTNode<K, T>> stack = new();
            BSTNode<K, T>? current = Root;

            while (stack.Count > 0 || current != null) {
                while (current != null) {
                    int cmpLower = current.KeyData.Compare(lower);

                    if (cmpLower == -1) {
                        current = current.RightSon;
                    } else {
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

                if (cmpUpperBound == 1) break;

                current = current.RightSon;
            }

            if (matches.Count == 0) {
                throw new KeyNotFoundException($"No matches found for interval: ({lower.GetKeys()} , {upper.GetKeys()})");
            }

            return matches;
        }

        public BSTNode<K, T>? GetMinNode(BSTNode<K, T>? node) {
            if (node == null) return null;

            while (node.LeftSon != null) {
                node = node.LeftSon;
            }

            return node;
        }

        public BSTNode<K, T>? GetMaxNode(BSTNode<K, T>? node) {
            if (node == null) return null;

            while (node.RightSon != null) {
                node = node.RightSon;
            }

            return node;
        }

        public K? GetMinKey() {
            BSTNode<K, T>? minNode = GetMinNode(Root);

            if (minNode == null) {
                Console.WriteLine("Tree is empty");
                return default;
            }

            return minNode.KeyData;
        }

        public K? GetMaxKey() {
            BSTNode<K, T>? maxNode = GetMaxNode(Root);

            if (maxNode == null) {
                Console.WriteLine("Tree is empty");
                return default;
            }

            return maxNode.KeyData;
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
