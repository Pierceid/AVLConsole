using AVLConsole.Entities;
using AVLConsole.Structures;

namespace AVLConsole.Objects {
    public class Traversal<K, T> where K : IKey<K> where T : Item {
        public static void InOrderTraversal(BST<K, T> tree, Action<Node<K, T>>? action) {
            Console.WriteLine("=== In Order Traversal ===");

            if (tree.Root == null) {
                Console.WriteLine("Tree is empty");
                return;
            }

            Node<K, T>? current = tree.Root;
            Stack<Node<K, T>> stack = new();

            while (stack.Count > 0 || current != null) {
                while (current != null) {
                    stack.Push(current);
                    current = current.LeftSon;
                }

                current = stack.Pop();
                action?.Invoke(current);
                current = current.RightSon;
            }

            Console.WriteLine();
        }

        public static void LevelOrderTraversal(BST<K, T> tree, Action<Node<K, T>>? action) {
            Console.WriteLine("=== Level Order Traversal ===");

            if (tree.Root == null) {
                Console.WriteLine("Tree is empty");
                return;
            }

            Queue<Node<K, T>> queue = new();
            queue.Enqueue(tree.Root);

            while (queue.Count > 0) {
                Node<K, T> current = queue.Dequeue();
                action?.Invoke(current);

                if (current.LeftSon != null) {
                    queue.Enqueue(current.LeftSon);
                }

                if (current.RightSon != null) {
                    queue.Enqueue(current.RightSon);
                }
            }

            Console.WriteLine();
        }
    }
}
