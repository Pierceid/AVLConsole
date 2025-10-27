namespace AVLConsole.Entities {
    public interface IKey<K> {
        public int Compare(K other);
        public K ImportKeys(string line);
        public string ExportKeys();
    }
}
