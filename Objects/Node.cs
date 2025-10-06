using AVLConsole.Entities;

namespace AVLConsole.Objects {
    public class Node<K, T> where K : IKey<K> where T : Item {
        public Node<K, T>? Parent { get; set; }
        public Node<K, T>? LeftSon { get; set; }
        public Node<K, T>? RightSon { get; set; }
        public K KeyData { get; set; }
        public List<T> NodeData { get; set; }

        public Node(K keys) {
            this.Parent = null;
            this.LeftSon = null;
            this.RightSon = null;
            this.KeyData = keys;
            this.NodeData = new();
        }

        public Node(K keys, T data) {
            this.Parent = null;
            this.LeftSon = null;
            this.RightSon = null;
            this.KeyData = keys;
            this.NodeData = new() { data };
        }
    }
}
