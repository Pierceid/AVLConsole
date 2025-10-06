using AVLConsole.Structures;

namespace AVLConsole.Objects {
    public class Generator {
        private BST<GPS> bst = new();
        private List<GPS> keyList = new();

        public void InsertBST(int count) {
            for (int i = 0; i < count; i++) {
                GPS gps = Util.GenerateRandomGPS(100, 100);
                bst.Insert(gps);
                keyList.Add(gps);
            }
        }

        public void FindBST(int count) {
            Random random = new();

            for (int i = 0; i < count; i++) {
                GPS gps = keyList.ElementAt(random.Next(keyList.Count));
                bst.Find(gps);
            }
        }

        public void DeleteBST(int count) {
            Random random = new();

            for (int i = 0; i < count; i++) {
                GPS gps = keyList.ElementAt(random.Next(keyList.Count));
                bst.Delete(gps);
                keyList.Remove(gps);
            }
        }

        public void InOrderTraversalBST() {
            bst.InOrderTraversal();
        }

        public void LevelOrderTraversalBST() {
            bst.LevelOrderTraversal();
        }
    }
}
