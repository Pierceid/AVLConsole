using AVLConsole.Entities;

namespace AVLConsole.Structures {
    public class BSTNode<K, T> where K : IKey<K> where T : Item {
        public BSTNode<K, T>? Parent { get; set; }
        public BSTNode<K, T>? LeftSon { get; set; }
        public BSTNode<K, T>? RightSon { get; set; }
        public K KeyData { get; set; }
        public T NodeData { get; set; }

        public BSTNode(K keys, T data) {
            Parent = null;
            LeftSon = null;
            RightSon = null;
            KeyData = keys;
            NodeData = data;
        }
    }
}
