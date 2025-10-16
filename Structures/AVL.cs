using AVLConsole.Entities;
using AVLConsole.Objects;

namespace AVLConsole.Structures {
    public class AVL<K, T> : BST<K, T> where K : IKey<K> where T : Item {
        public override void Insert(K keys, T data) {
            AVLNode<K, T> nodeToInsert = new(keys, data);

            if (Root == null) {
                Root = nodeToInsert;
                NodeCount++;
                return;
            }

            AVLNode<K, T> current = (AVLNode<K, T>)Root;

            while (true) {
                int cmp = keys.Compare(current.KeyData);

                if (cmp == -1) {
                    if (current.LeftSon == null) {
                        nodeToInsert.Parent = current;
                        current.LeftSon = nodeToInsert;
                        NodeCount++;
                        break;
                    }

                    current = (AVLNode<K, T>)current.LeftSon;
                } else if (cmp == 1) {
                    if (current.RightSon == null) {
                        nodeToInsert.Parent = current;
                        current.RightSon = nodeToInsert;
                        NodeCount++;
                        break;
                    }

                    current = (AVLNode<K, T>)current.RightSon;
                } else {
                    throw new InvalidOperationException($"Duplicate key insertion attempted: ({keys.GetKeys()})");
                }
            }

            RebalanceAfterInsert(nodeToInsert);

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

            AVLNode<K, T>? rebalanceStart = null;
            bool deletedFromLeft = false;

            if (nodeToDelete.LeftSon == null || nodeToDelete.RightSon == null) {
                AVLNode<K, T>? child = (AVLNode<K, T>?)(nodeToDelete.LeftSon ?? nodeToDelete.RightSon);
                AVLNode<K, T>? parent = (AVLNode<K, T>?)nodeToDelete.Parent;

                if (parent != null) {
                    deletedFromLeft = (parent.LeftSon == nodeToDelete);
                    rebalanceStart = parent;
                }

                if (parent == null) {
                    Root = child;

                    if (child != null) {
                        child.Parent = null;
                    }
                } else {
                    if (parent.LeftSon == nodeToDelete) {
                        parent.LeftSon = child;
                    } else {
                        parent.RightSon = child;
                    }

                    if (child != null) {
                        child.Parent = parent;
                    }
                }

                NodeCount--;
            } else {
                AVLNode<K, T>? predParent = nodeToDelete;
                AVLNode<K, T> pred = (AVLNode<K, T>)nodeToDelete.LeftSon!;

                while (pred.RightSon != null) {
                    predParent = pred;
                    pred = (AVLNode<K, T>)pred.RightSon;
                }

                nodeToDelete.KeyData = pred.KeyData;
                nodeToDelete.NodeData = pred.NodeData;

                AVLNode<K, T>? predChild = (AVLNode<K, T>?)pred.LeftSon;

                deletedFromLeft = (predParent.LeftSon == pred);
                rebalanceStart = predParent;

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

            if (rebalanceStart != null) {
                RebalanceAfterDelete(rebalanceStart, deletedFromLeft);
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

        private void RebalanceAfterDelete(AVLNode<K, T>? start, bool deletedFromLeft) {
            AVLNode<K, T>? current = start;
            bool fromLeft = deletedFromLeft;

            while (current != null) {
                if (fromLeft) {
                    current.BalanceFactor++;
                } else {
                    current.BalanceFactor--;
                }

                if (current.BalanceFactor == 1 || current.BalanceFactor == -1) break;

                if (current.BalanceFactor == 0) {
                    AVLNode<K, T>? parent = (AVLNode<K, T>?)current.Parent;
                    fromLeft = (parent?.LeftSon == current);
                    current = parent;
                    continue;
                }

                if (current.BalanceFactor < -1) {
                    AVLNode<K, T> left = (AVLNode<K, T>)current.LeftSon!;

                    if (left.BalanceFactor <= 0) {
                        current = RotateRight(current);
                    } else {
                        RotateLeft(left);
                        current = RotateRight(current);
                    }
                } else if (current.BalanceFactor > 1) {
                    AVLNode<K, T> right = (AVLNode<K, T>)current.RightSon!;

                    if (right.BalanceFactor >= 0) {
                        current = RotateLeft(current);
                    } else {
                        RotateRight(right);
                        current = RotateLeft(current);
                    }
                }

                if (current == null) break;

                if (current.BalanceFactor == 0) {
                    AVLNode<K, T>? parent = (AVLNode<K, T>?)current.Parent;
                    fromLeft = (parent?.LeftSon == current);
                    current = parent;
                    continue;
                } else {
                    break;
                }
            }

            while (Root?.Parent != null) {
                Root = (AVLNode<K, T>)Root.Parent!;
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
