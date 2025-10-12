using AVLConsole.Entities;
using AVLConsole.Objects;

namespace AVLConsole.Structures {
    public class AVL<K, T> : BST<K, T> where K : IKey<K> where T : Item {
        public override void Insert(K keys, T data) {
            base.Insert(keys, data);


        }

        public override void Update(K oldKeys, T oldData, K newKeys, T newData) {
            base.Update(oldKeys, oldData, newKeys, newData);


        }

        public override void Delete(K keys, T data) {
            base.Delete(keys, data);


        }

        public override List<BSTNode<K, T>> PointFind(K keys) {
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
    }
}
