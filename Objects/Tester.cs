using AVLConsole.Structures;

namespace AVLConsole.Objects {
    public class Tester {
        private BST<Number, Number> bst = new();
        private List<Number> keyList = new();

        public void InsertBST(int count) {
            Random random = new();

            for (int i = 0; i < count; i++) {
                int value = random.Next();
                Number number = new() { Value = value };

                if (i % 100000 == 0) Console.Write($"{i + 1}. ");

                bst.Insert(number, number);
                //keyList.Add(number);
            }
        }

        public void FindBST(int count) {
            Random random = new();

            for (int i = 0; i < count; i++) {
                Console.Write($"{i + 1}. ");
                Number number = keyList.ElementAt(random.Next(keyList.Count));
                bst.Find(number);
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
            //bst.InOrderTraversal();
            Console.WriteLine($"Node Count: {bst.NodeCount}");
            Console.WriteLine($"Data Count: {bst.DataCount}");
        }

        public void LevelOrderTraversalBST() {
            //bst.LevelOrderTraversal();
            Console.WriteLine($"Node Count: {bst.NodeCount}");
            Console.WriteLine($"Data Count: {bst.DataCount}");
        }
    }
}
