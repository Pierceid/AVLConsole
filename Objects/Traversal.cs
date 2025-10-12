using AVLConsole.Entities;
using AVLConsole.Structures;

namespace AVLConsole.Objects {
    public class Traversal<K, T> where K : IKey<K> where T : Item {
        public static void InOrderTraversal(BST<K, T> tree, Action<BSTNode<K, T>>? action) {
            Console.WriteLine("=== In Order Traversal ===");

            if (tree.Root == null) {
                Console.WriteLine("Tree is empty");
                return;
            }

            BSTNode<K, T>? current = tree.Root;
            Stack<BSTNode<K, T>> stack = new();

            while (stack.Count > 0 || current != null) {
                while (current != null) {
                    stack.Push(current);
                    current = current.LeftSon;
                }

                current = stack.Pop();
                action?.Invoke(current);
                current = current.RightSon;
            }
        }

        public static void LevelOrderTraversal(BST<K, T> tree, Action<BSTNode<K, T>>? action) {
            Console.WriteLine("=== Level Order Traversal ===");

            if (tree.Root == null) {
                Console.WriteLine("Tree is empty");
                return;
            }

            Queue<BSTNode<K, T>> queue = new();
            queue.Enqueue(tree.Root);

            while (queue.Count > 0) {
                BSTNode<K, T> current = queue.Dequeue();
                action?.Invoke(current);

                if (current.LeftSon != null) {
                    queue.Enqueue(current.LeftSon);
                }

                if (current.RightSon != null) {
                    queue.Enqueue(current.RightSon);
                }
            }
        }
    }
}
