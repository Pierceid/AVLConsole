
using AVLConsole.Entities;
using AVLConsole.Objects;

namespace AVLConsole.Structures {
    public class AVL<K, T> : BST<K, T> where K : IKey<K> where T : Item {
        public override void Insert(K keys, T data) {
            if (Root == null) {
                Root = new AVLNode<K, T>(keys, data);
                NodeCount++;
                return;
            }

            AVLNode<K, T> current = (AVLNode<K, T>)Root;

            while (true) {
                int cmp = keys.Compare(current.KeyData);

                if (cmp == -1) {
                    if (current.LeftSon == null) {
                        current.LeftSon = new AVLNode<K, T>(keys, data) { Parent = current };
                        NodeCount++;
                        break;
                    }

                    current = (AVLNode<K, T>)current.LeftSon;
                } else if (cmp == 1) {
                    if (current.RightSon == null) {
                        current.RightSon = new AVLNode<K, T>(keys, data) { Parent = current };
                        NodeCount++;
                        break;
                    }

                    current = (AVLNode<K, T>)current.RightSon;
                } else {
                    throw new InvalidOperationException($"Duplicate key insertion attempted: ({keys.GetKeys()})");
                }
            }

            RebalanceAfterInsert(current);

            while (Root.Parent != null) {
                Root = (AVLNode<K, T>)Root.Parent;
            }
        }

        public override void Update(K oldKeys, T oldData, K newKeys, T newData) {
            base.Update(oldKeys, oldData, newKeys, newData);
        }

        public override void Delete(K keys, T data) {
            if (Root == null) {
                Console.WriteLine("Tree is empty");
                return;
            }

            AVLNode<K, T>? nodeToDelete = (AVLNode<K, T>)PointFind(keys);
            AVLNode<K, T>? parent = (AVLNode<K, T>?)nodeToDelete.Parent;

            if (nodeToDelete == null) return;

            if (nodeToDelete.LeftSon != null && nodeToDelete.RightSon != null) {
                AVLNode<K, T>? successor = (AVLNode<K, T>?)base.GetMinNode(nodeToDelete.RightSon);

                if (successor == null) return;

                nodeToDelete.KeyData = successor.KeyData;
                nodeToDelete.NodeData = successor.NodeData;
                nodeToDelete = successor;
                parent = (AVLNode<K, T>?)nodeToDelete.Parent;
            }

            AVLNode<K, T>? child = (AVLNode<K, T>?)(nodeToDelete.LeftSon ?? nodeToDelete.RightSon);

            if (child != null) {
                child.Parent = parent;
            }

            if (parent == null) {
                Root = child;
            } else if (parent.LeftSon == nodeToDelete) {
                parent.LeftSon = child;
                parent.BalanceFactor++;

                RebalanceAfterDelete(parent);
            } else {
                parent.RightSon = child;
                parent.BalanceFactor--;

                RebalanceAfterDelete(parent);
            }

            NodeCount--;
        }

        public override BSTNode<K, T> PointFind(K keys) {
            return base.PointFind(keys);
        }

        public override List<BSTNode<K, T>> IntervalFind(K lower, K upper) {
            return base.IntervalFind(lower, upper);
        }

        private void RebalanceAfterInsert(AVLNode<K, T> node) {
            AVLNode<K, T>? current = node;

            while (current?.Parent is AVLNode<K, T> parent) {
                if (parent.LeftSon == current) {
                    parent.BalanceFactor--;
                } else {
                    parent.BalanceFactor++;
                }

                if (parent.BalanceFactor == 0) break;

                if (parent.BalanceFactor == 1 || parent.BalanceFactor == -1) {
                    current = parent;
                    continue;
                }

                if (parent.BalanceFactor < -1) {
                    AVLNode<K, T> left = (AVLNode<K, T>)parent.LeftSon!;

                    if (left.BalanceFactor <= 0) {
                        RotateRight(parent);
                    } else {
                        RotateLeft(left);
                        RotateRight(parent);
                    }
                } else if (parent.BalanceFactor > 1) {
                    AVLNode<K, T> right = (AVLNode<K, T>)parent.RightSon!;

                    if (right.BalanceFactor >= 0) {
                        RotateLeft(parent);
                    } else {
                        RotateRight(right);
                        RotateLeft(parent);
                    }
                }

                break;
            }
        }

        private void RebalanceAfterDelete(AVLNode<K, T> node) {
            AVLNode<K, T>? current = node;

            while (current?.Parent is AVLNode<K, T> parent) {
                if (parent.LeftSon == current) {
                    parent.BalanceFactor++;
                } else {
                    parent.BalanceFactor--;
                }

                if (parent.BalanceFactor < -1) {
                    AVLNode<K, T> left = (AVLNode<K, T>)parent.LeftSon!;

                    if (left.BalanceFactor <= 0) {
                        RotateRight(parent);
                    } else {
                        RotateLeft(left);
                        RotateRight(parent);
                    }
                } else if (parent.BalanceFactor > 1) {
                    AVLNode<K, T> right = (AVLNode<K, T>)parent.RightSon!;

                    if (right.BalanceFactor >= 0) {
                        RotateLeft(parent);
                    } else {
                        RotateRight(right);
                        RotateLeft(parent);
                    }
                }

                if (parent.BalanceFactor == 0) {
                    current = parent;
                    continue;
                }

                break;
            }
        }

        private AVLNode<K, T> RotateLeft(AVLNode<K, T> rootNode) {
            if (rootNode.RightSon is not AVLNode<K, T> pivot) {
                return rootNode;
            }

            rootNode.RightSon = pivot.LeftSon;

            if (pivot.LeftSon != null) {
                pivot.LeftSon.Parent = rootNode;
            }

            pivot.LeftSon = rootNode;
            pivot.Parent = rootNode.Parent;
            rootNode.Parent = pivot;

            if (pivot.Parent != null) {
                if (pivot.Parent.LeftSon == rootNode) {
                    pivot.Parent.LeftSon = pivot;
                } else {
                    pivot.Parent.RightSon = pivot;
                }
            } else {
                Root = pivot;
            }

            rootNode.BalanceFactor = rootNode.BalanceFactor - 1 - Math.Max(pivot.BalanceFactor, 0);
            pivot.BalanceFactor = pivot.BalanceFactor - 1 + Math.Min(rootNode.BalanceFactor, 0);

            return pivot;
        }

        private AVLNode<K, T> RotateRight(AVLNode<K, T> rootNode) {
            if (rootNode.LeftSon is not AVLNode<K, T> pivot) {
                return rootNode;
            }

            rootNode.LeftSon = pivot.RightSon;

            if (pivot.RightSon != null) {
                pivot.RightSon.Parent = rootNode;
            }

            pivot.RightSon = rootNode;
            pivot.Parent = rootNode.Parent;
            rootNode.Parent = pivot;

            if (pivot.Parent != null) {
                if (pivot.Parent.LeftSon == rootNode) {
                    pivot.Parent.LeftSon = pivot;
                } else {
                    pivot.Parent.RightSon = pivot;
                }
            } else {
                Root = pivot;
            }

            rootNode.BalanceFactor = rootNode.BalanceFactor + 1 - Math.Min(pivot.BalanceFactor, 0);
            pivot.BalanceFactor = pivot.BalanceFactor + 1 + Math.Max(rootNode.BalanceFactor, 0);

            return pivot;
        }
    }
}
