using AVLConsole.Entities;
using AVLConsole.Objects;

namespace AVLConsole.Structures {
    public class AVLNode<K, T> : BSTNode<K, T> where K : IKey<K> where T : Item {


        public AVLNode(K keys, T data) : base(keys, data) {

        }
    }
}
