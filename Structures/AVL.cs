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

            // standard BST insert
            while (true) {
                int cmp = keys.Compare(current.KeyData);

                if (cmp < 0) {
                    if (current.LeftSon == null) {
                        current.LeftSon = new AVLNode<K, T>(keys, data) { Parent = current };
                        NodeCount++;
                        current = (AVLNode<K, T>)current.LeftSon;
                        break;
                    }
                    current = (AVLNode<K, T>)current.LeftSon;
                } else if (cmp > 0) {
                    if (current.RightSon == null) {
                        current.RightSon = new AVLNode<K, T>(keys, data) { Parent = current };
                        NodeCount++;
                        current = (AVLNode<K, T>)current.RightSon;
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
            base.Delete(keys, data);


        }

        public override BSTNode<K, T> PointFind(K keys) {
            return base.PointFind(keys);
        }

        public override List<BSTNode<K, T>> IntervalFind(K lower, K upper) {
            return base.IntervalFind(lower, upper);
        }

        public override K? GetMinKey() {
            return base.GetMinKey();
        }

        public override K? GetMaxKey() {
            return base.GetMaxKey();
        }

        private void RebalanceAfterInsert(AVLNode<K, T> node) {
            AVLNode<K, T>? current = node;

            while (current?.Parent is AVLNode<K, T> parent) {
                if (parent.LeftSon == current) {
                    parent.BalanceFactor--;
                } else if (parent.RightSon == current) {
                    parent.BalanceFactor++;
                }

                if (parent.BalanceFactor == 0) break;

                if (Math.Abs(parent.BalanceFactor) == 1) {
                    current = parent;
                    continue;
                }

                if (parent.BalanceFactor < -1) {
                    if (parent.LeftSon is not AVLNode<K, T> left) return;

                    if (left.BalanceFactor <= 0) {
                        RotateRight(parent);
                    } else {
                        RotateLeft(left);
                        RotateRight(parent);
                    }
                } else if (parent.BalanceFactor > 1) {
                    if (parent.RightSon is not AVLNode<K, T> right) return;

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

        private AVLNode<K, T> RotateLeft(AVLNode<K, T> rootNode) {
            if (rootNode.RightSon is not AVLNode<K, T> pivot) {
                return rootNode;
            }

            rootNode.RightSon = pivot.LeftSon;

            if (pivot.LeftSon != null) {
                pivot.LeftSon.Parent = rootNode;
            }

            pivot.LeftSon = rootNode;

            AVLNode<K, T>? oldParent = (AVLNode<K, T>?)rootNode.Parent;
            pivot.Parent = oldParent;
            rootNode.Parent = pivot;

            if (oldParent != null) {
                if (oldParent.LeftSon == rootNode) {
                    oldParent.LeftSon = pivot;
                } else {
                    oldParent.RightSon = pivot;
                }
            } else {
                Root = pivot;
            }

            if (pivot.BalanceFactor > 0) {
                rootNode.BalanceFactor += 1 - pivot.BalanceFactor;
                pivot.BalanceFactor += 1;
            } else {
                rootNode.BalanceFactor += 1;
                pivot.BalanceFactor += 1;
            }

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

            AVLNode<K, T>? oldParent = (AVLNode<K, T>?)rootNode.Parent;
            pivot.Parent = oldParent;
            rootNode.Parent = pivot;

            if (oldParent != null) {
                if (oldParent.LeftSon == rootNode) {
                    oldParent.LeftSon = pivot;
                } else {
                    oldParent.RightSon = pivot;
                }
            } else {
                Root = pivot;
            }

            if (pivot.BalanceFactor < 0) {
                rootNode.BalanceFactor -= 1 + pivot.BalanceFactor;
                pivot.BalanceFactor -= 1;
            } else {
                rootNode.BalanceFactor -= 1;
                pivot.BalanceFactor -= 1;
            }

            return pivot;
        }
    }
}
