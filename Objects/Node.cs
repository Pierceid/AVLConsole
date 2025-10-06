namespace AVLConsole.Objects {
    public class Node<T> where T : IKey<T>, new() {
        public Node<T>? Left { get; set; }
        public Node<T>? Right { get; set; }
        public T Data { get; set; }

        public Node() {
            Left = null;
            Right = null;
            Data = new T();
        }
    }
}
