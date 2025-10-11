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
                        // new key > current key
                        if (current.RightSon == null) {
                            current.RightSon = new(keys, data) { Parent = current };
                            return;
                        } else {
                            current = current.RightSon;
                        }
                    } else {
                        // new key <= current key
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
            var current = Root;

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

            // find the node to delete
            BSTNode<K, T>? nodeToDelete = PointFind(keys).FirstOrDefault(p => p.NodeData.EqualsByID(data));

            if (nodeToDelete == null) return;

            BSTNode<K, T>? parent = nodeToDelete.Parent;

            if (nodeToDelete.LeftSon == null && nodeToDelete.RightSon == null) {
                // leaf node
                ReplaceParentLink(parent, nodeToDelete, null);
            } else if (nodeToDelete.LeftSon == null) {
                // only right child
                ReplaceParentLink(parent, nodeToDelete, nodeToDelete.RightSon);

                if (nodeToDelete.RightSon != null) {
                    nodeToDelete.RightSon.Parent = parent;
                }
            } else if (nodeToDelete.RightSon == null) {
                // only left child
                ReplaceParentLink(parent, nodeToDelete, nodeToDelete.LeftSon);

                if (nodeToDelete.LeftSon != null) {
                    nodeToDelete.LeftSon.Parent = parent;
                }
            } else {
                // two children
                BSTNode<K, T>? predecessor = nodeToDelete.LeftSon;
                BSTNode<K, T>? predecessorParent = nodeToDelete;

                while (predecessor.RightSon != null) {
                    predecessorParent = predecessor;
                    predecessor = predecessor.RightSon;
                }

                // copy predecessor data into current node
                nodeToDelete.KeyData = predecessor.KeyData;
                nodeToDelete.NodeData = predecessor.NodeData;

                // remove predecessor node
                if (predecessor.LeftSon != null) {
                    ReplaceParentLink(predecessorParent, predecessor, predecessor.LeftSon);
                    predecessor.LeftSon.Parent = predecessorParent;
                } else {
                    ReplaceParentLink(predecessorParent, predecessor, null);
                }
            }

            NodeCount--;
        }

        private void ReplaceParentLink(BSTNode<K, T>? parent, BSTNode<K, T> target, BSTNode<K, T>? newChild) {
            if (parent == null) {
                Root = newChild;

                if (Root != null) {
                    Root.Parent = null;
                }

                return;
            }

            if (parent.LeftSon == target) {
                parent.LeftSon = newChild;
            } else if (parent.RightSon == target) {
                parent.RightSon = newChild;
            }
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

            var current = Root;

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

            var current = Root;

            while (current.RightSon != null) {
                current = current.RightSon;
            }

            return current.KeyData;
        }

        public void InOrderTraversal() {
            int index = 0;
            Traversal<K, T>.InOrderTraversal(this, node => {
                //Console.WriteLine($"{++index}. {node.KeyData.GetKeys()} - {node.NodeData.GetInfo()}");
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
