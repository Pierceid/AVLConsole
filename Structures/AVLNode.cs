using AVLConsole.Entities;

namespace AVLConsole.Structures {
    public class AVLNode<K, T> : BSTNode<K, T> where K : IKey<K> where T : Item {
        public int BalanceFactor { get; set; }

        public AVLNode(K keys, T data) : base(keys, data) {
            BalanceFactor = 0;
        }
    }
}
