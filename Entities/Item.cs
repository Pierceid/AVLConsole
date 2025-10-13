namespace AVLConsole.Entities {
    public abstract class Item {
        private string id = Guid.NewGuid().ToString();

        public bool EqualsByID(Item other) => id == other.Id;

        public abstract string GetInfo();

        public string Id { get => id; set => id = value; }
    }
}
