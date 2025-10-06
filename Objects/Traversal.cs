using AVLConsole.Structures;

namespace AVLConsole.Objects {
    public class Traversal<T> where T : IKey<T>, new() {
        public static void InOrderTraversal(BST<T> tree, Action<Node<T>>? action) {
            Console.WriteLine("=== In Order Traversal ===");

            if (tree.Root == null) {
                Console.WriteLine("Tree is empty");
                return;
            }

            Node<T>? current = tree.Root;
            Stack<Node<T>> stack = new();

            while (stack.Count > 0 || current != null) {
                while (current != null) {
                    stack.Push(current);
                    current = current.Left;
                }

                current = stack.Pop();
                action?.Invoke(current);
                current = current.Right;
            }

            Console.WriteLine();
        }

        public static void LevelOrderTraversal(BST<T> tree, Action<Node<T>>? action) {
            Console.WriteLine("=== Level Order Traversal ===");

            if (tree.Root == null) {
                Console.WriteLine("Tree is empty");
                return;
            }

            Queue<Node<T>> queue = new();
            queue.Enqueue(tree.Root);

            while (queue.Count > 0) {
                Node<T> current = queue.Dequeue();
                action?.Invoke(current);

                if (current.Left != null) {
                    queue.Enqueue(current.Left);
                }

                if (current.Right != null) {
                    queue.Enqueue(current.Right);
                }
            }

            Console.WriteLine();
        }
    }
}
