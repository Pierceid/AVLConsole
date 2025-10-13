using AVLConsole.Entities;
using AVLConsole.Objects;

namespace AVLConsole.Structures {
    public class AVL<K, T> : BST<K, T> where K : IKey<K> where T : Item {
        public override void Insert(K keys, T data) {
            if (Root == null) {
                Root = new(keys, data);
                NodeCount++;
                return;
            }

            Stack<AVLNode<K, T>> path = new();
            BSTNode<K, T>? current = Root;

            while (true) {
                int cmp = keys.Compare(current.KeyData);

                if (cmp == -1) {
                    if (current.LeftSon == null) {
                        current.LeftSon = new(keys, data) { Parent = current };
                        NodeCount++;
                        break;
                    }

                    current = current.LeftSon;
                } else if (cmp == 1) {
                    if (current.RightSon == null) {
                        current.RightSon = new(keys, data) { Parent = current };
                        NodeCount++;
                        break;
                    }

                    current = current.RightSon;
                } else {
                    throw new InvalidOperationException($"Duplicate key insertion attempted: ({keys.GetKeys()})");
                }
            }

            while (path.Count > 0) {
                AVLNode<K, T> parent = path.Pop();

                if (parent.RightSon == current) {
                    parent.BalanceFactor++;
                } else if (parent.LeftSon == current) {
                    parent.BalanceFactor--;
                }

                if (parent.BalanceFactor == 0) break;

                if (parent.BalanceFactor < -1) {
                    // left heavy
                    AVLNode<K, T> left = (AVLNode<K, T>)parent.LeftSon!;

                    if (left.BalanceFactor < 0) {
                        parent = RotateRight(parent);
                    } else {
                        parent.LeftSon = RotateLeft(left);
                        parent = RotateRight(parent);
                    }

                    break;
                } else if (parent.BalanceFactor > 1) {
                    // right heavy
                    AVLNode<K, T> right = (AVLNode<K, T>)parent.RightSon!;

                    if (right.BalanceFactor > 0) {
                        parent = RotateLeft(parent);
                    } else {
                        parent.RightSon = RotateRight(right);
                        parent = RotateLeft(parent);
                    }

                    break;
                }
            }

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

        private AVLNode<K, T> RotateRight(AVLNode<K, T> rootNode) {
            AVLNode<K, T> leftChild = (AVLNode<K, T>)rootNode.LeftSon!;
            AVLNode<K, T>? rightSubtreeOfLeftChild = (AVLNode<K, T>?)leftChild.RightSon;

            leftChild.RightSon = rootNode;
            rootNode.LeftSon = rightSubtreeOfLeftChild;

            if (rightSubtreeOfLeftChild != null) {
                rightSubtreeOfLeftChild.Parent = rootNode;
            }

            leftChild.Parent = rootNode.Parent;
            rootNode.Parent = leftChild;

            rootNode.BalanceFactor = rootNode.BalanceFactor - 1 - Math.Max(leftChild.BalanceFactor, 0);
            leftChild.BalanceFactor = leftChild.BalanceFactor - 1 + Math.Min(rootNode.BalanceFactor, 0);

            return leftChild;
        }

        private AVLNode<K, T> RotateLeft(AVLNode<K, T> rootNode) {
            AVLNode<K, T> rightChild = (AVLNode<K, T>)rootNode.RightSon!;
            AVLNode<K, T>? leftSubtreeOfRightChild = (AVLNode<K, T>?)rightChild.LeftSon;

            rightChild.LeftSon = rootNode;
            rootNode.RightSon = leftSubtreeOfRightChild;

            if (leftSubtreeOfRightChild != null) {
                leftSubtreeOfRightChild.Parent = rootNode;
            }

            rightChild.Parent = rootNode.Parent;
            rootNode.Parent = rightChild;

            rootNode.BalanceFactor = rootNode.BalanceFactor + 1 - Math.Min(rightChild.BalanceFactor, 0);
            rightChild.BalanceFactor = rightChild.BalanceFactor + 1 + Math.Max(rootNode.BalanceFactor, 0);

            return rightChild;
        }
    }
}
