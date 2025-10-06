namespace AVLConsole.Objects {
    public interface IKey<U> {
        public int Compare(U other);
        public bool Equals(U other);
        public string GetKeys();
    }
}
