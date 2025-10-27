namespace AVLConsole.Entities {
    public abstract class Item {
        public abstract Item ImportItem(string line);
        public abstract string ExportItem();
    }
}
