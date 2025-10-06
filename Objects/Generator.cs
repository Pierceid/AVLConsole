using AVLConsole.Entities;
using AVLConsole.Structures;

namespace AVLConsole.Objects {
    public class Generator {
        private BST<GPS, Estate> bst = new();
        private List<GPS> keyList = new();

        public void InsertBST(int count) {
            Random random = new();

            for (int i = 0; i < count; i++) {
                int number = random.Next();
                string description = Util.GenerateRandomString(10);
                GPS position = Util.GenerateRandomGPS(100, 100);
                Estate estate = new(number, description, position);

                Console.Write($"{i + 1}. ");

                bst.Insert(position, estate);
                keyList.Add(position);
            }
        }

        public void FindBST(int count) {
            Random random = new();

            for (int i = 0; i < count; i++) {
                Console.Write($"{i + 1}. ");
                GPS gps = keyList.ElementAt(random.Next(keyList.Count));
                bst.Find(gps);
            }
        }

        public void DeleteBST(int count) {
            Random random = new();

            for (int i = 0; i < count; i++) {
                //Console.Write($"{i + 1}. ");
                //GPS gps = keyList.ElementAt(random.Next(keyList.Count));
                //bst.Delete(gps);
                //keyList.Remove(gps);
            }
        }

        public void InOrderTraversalBST() {
            bst.InOrderTraversal();
            Console.WriteLine($"Node Count: {bst.NodeCount}");
            Console.WriteLine($"Data Count: {bst.DataCount}");
        }

        public void LevelOrderTraversalBST() {
            bst.LevelOrderTraversal();
            Console.WriteLine($"Node Count: {bst.NodeCount}");
            Console.WriteLine($"Data Count: {bst.DataCount}");
        }
    }
}
