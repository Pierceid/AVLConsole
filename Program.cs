using AVLConsole.Objects;

namespace AVLConsole {
    internal class Program {
        static void Main(string[] args) {
            Generator generator = new();

            generator.InsertBST(1000);

            //generator.FindBST(1000);

            //generator.DeleteBST(1000);

            generator.InOrderTraversalBST();

            generator.LevelOrderTraversalBST();
        }
    }
}
