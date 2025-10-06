using AVLConsole.Objects;

namespace AVLConsole.Structures {
    public class BST<T> where T : IKey<T>, new() {
        public Node<T>? Root { get; set; }
        public int Count { get; set; }

        public BST() {
            Root = null;
            Count = 0;
        }

        public void Insert(T data) {
            Count++;

            if (Root == null) {
                Root = new() { Data = data };
                Console.WriteLine($"Insert root: {data.GetKeys()}");
                return;
            } else {
                var current = Root;

                while (current != null) {
                    if (current.Data.Compare(data) == -1) {
                        if (current.Right == null) {
                            current.Right = new() { Data = data };
                            Console.WriteLine($"Insert node: {data.GetKeys()}");
                            return;
                        } else {
                            current = current.Right;
                        }
                    } else {
                        if (current.Left == null) {
                            current.Left = new() { Data = data };
                            Console.WriteLine($"Insert node: {data.GetKeys()}");
                            return;
                        } else {
                            current = current.Left;
                        }
                    }
                }
            }
        }

        public void Find(T data) {
            if (Root == null) {
                Console.WriteLine("Tree is empty");
                return;
            }

        }

        public void Delete(T data) {
            if (Root == null) {
                Console.WriteLine("Tree is empty");
                return;
            }

            Count--;
        }

        public void InOrderTraversal() {
            int index = 0;
            Traversal<T>.InOrderTraversal(this, (node) => Console.WriteLine($"{index++}. {node.Data.GetKeys()}"));
        }

        public void LevelOrderTraversal() {
            int index = 0;
            Traversal<T>.LevelOrderTraversal(this, node => Console.WriteLine($"{index++}. {node.Data.GetKeys()}"));
        }
    }
}
