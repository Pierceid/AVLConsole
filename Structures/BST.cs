using AVLConsole.Entities;
using AVLConsole.Objects;

namespace AVLConsole.Structures {
    public class BST<K, T> where K : IKey<K> where T : Item {
        public BSTNode<K, T>? Root { get; set; }
        public int DataCount { get; set; }
        public int NodeCount { get; set; }

        public BST() {
            Root = null;
            DataCount = 0;
            NodeCount = 0;
        }

        public void Insert(K keys, T data) {
            DataCount++;

            if (Root == null) {
                NodeCount++;
                Root = new(keys, data);
                //Console.WriteLine($"Insert root: {data}");
                return;
            } else {
                var current = Root;

                while (current != null) {
                    if (current.KeyData.Equals(keys)) {
                        current.NodeData.Add(data);
                        //Console.WriteLine($"Insert data: {data}");
                        return;
                    }

                    if (current.KeyData.Compare(keys) == -1) {
                        if (current.RightSon == null) {
                            NodeCount++;
                            current.RightSon = new(keys, data) { Parent = current };
                            //Console.WriteLine($"Insert node: {data}");
                            return;
                        } else {
                            current = current.RightSon;
                        }
                    } else {
                        if (current.LeftSon == null) {
                            NodeCount++;
                            current.LeftSon = new(keys, data) { Parent = current };
                            //Console.WriteLine($"Insert node: {data}");
                            return;
                        } else {
                            current = current.LeftSon;
                        }
                    }
                }
            }
        }

        public void Find(K keys) {
            if (Root == null) {
                Console.WriteLine("Tree is empty");
                return;
            }

            var current = Root;

            while (current != null) {
                if (current.KeyData.Equals(keys)) {
                    //int index = 0;
                    //Console.WriteLine($"Found node: {keys.GetKeys()}");
                    //current.NodeData.ForEach(item => Console.WriteLine($"{++index}. {item}"));
                    return;
                }

                int cmp = current.KeyData.Compare(keys);

                if (cmp < 0) {
                    if (current.RightSon != null) {
                        current = current.RightSon;
                    } else {
                        break;
                    }
                } else {
                    if (current.LeftSon != null) {
                        current = current.LeftSon;
                    } else {
                        break;
                    }
                }
            }

            throw new KeyNotFoundException($"Node not found: {keys.GetKeys()}");
        }

        public void Delete(K keys, T data) {
            if (Root == null) {
                Console.WriteLine("Tree is empty");
                return;
            }

            DataCount--;
        }

        public void InOrderTraversal() {
            int index = 0;
            Traversal<K, T>.LevelOrderTraversal(this, node => {
                Console.WriteLine($"{++index}. {node.KeyData.GetKeys()}");
                node.NodeData.ForEach(item => Console.WriteLine(item));
            });
        }

        public void LevelOrderTraversal() {
            int index = 0;
            Traversal<K, T>.LevelOrderTraversal(this, node => {
                Console.WriteLine($"{++index}. {node.KeyData.GetKeys()}");
                node.NodeData.ForEach(item => Console.WriteLine(item));
            });
        }
    }
}
