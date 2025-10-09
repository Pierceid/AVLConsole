namespace AVLConsole.Objects {
    public interface IKey<K> {
        public int Compare(K other);
        public bool Equals(K other);
        public string GetKeys();
    }
}
