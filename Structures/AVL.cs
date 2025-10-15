
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
            if (Root == null) return;

            AVLNode<K, T>? nodeToDelete = (AVLNode<K, T>?)PointFind(keys);

            if (nodeToDelete == null) return;

            AVLNode<K, T>? parent = (AVLNode<K, T>?)nodeToDelete.Parent;
            AVLNode<K, T>? rebalanceStart = null;

            if (nodeToDelete.LeftSon == null || nodeToDelete.RightSon == null) {
                AVLNode<K, T>? child = (AVLNode<K, T>?)(nodeToDelete.LeftSon ?? nodeToDelete.RightSon);

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

                rebalanceStart = parent;
                
                NodeCount--;
            } else {
                AVLNode<K, T>? predParent = nodeToDelete;
                AVLNode<K, T> pred = (AVLNode<K, T>)nodeToDelete.LeftSon;

                while (pred.RightSon != null) {
                    predParent = pred;
                    pred = (AVLNode<K, T>)pred.RightSon;
                }

                nodeToDelete.KeyData = pred.KeyData;
                nodeToDelete.NodeData = pred.NodeData;

                AVLNode<K, T>? predChild = (AVLNode<K, T>?)pred.LeftSon;

                if (predParent == nodeToDelete) {
                    predParent.LeftSon = predChild;
                } else if (predParent.LeftSon == pred) {
                    predParent.LeftSon = predChild;
                } else {
                    predParent.RightSon = predChild;
                }

                if (predChild != null) {
                    predChild.Parent = predParent;
                }

                rebalanceStart = predParent;

                NodeCount--;
            }

            if (rebalanceStart != null) {
                RebalanceAfterDelete(rebalanceStart);
            }

            while (Root?.Parent != null) {
                Root = (AVLNode<K, T>)Root.Parent!;
            }
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

            rootNode.BalanceFactor = rootNode.BalanceFactor - pivot.BalanceFactor - 1;
            pivot.BalanceFactor = pivot.BalanceFactor + rootNode.BalanceFactor - 1;

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

            rootNode.BalanceFactor = rootNode.BalanceFactor - pivot.BalanceFactor + 1;
            pivot.BalanceFactor = pivot.BalanceFactor + rootNode.BalanceFactor + 1;

            return pivot;
        }
    }
}
