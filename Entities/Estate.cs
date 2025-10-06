using AVLConsole.Objects;

namespace AVLConsole.Entities {
    public class Estate : Item {
        public int Number { get; set; }
        public string Description { get; set; }
        public GPS Position { get; set; }

        public Estate(int number, string description, GPS position) {
            Number = number;
            Description = description;
            Position = position;
        }

        public override void PrintInfo() {
            Console.WriteLine($"Estate: {Number} - {Description} - [{Position.GetKeys()}]");
        }

        public override string GetInfo() {
            return $"Estate,{Id},{Number},{Description},{Position.GetKeys()}";
        }

        public override string? ToString() {
            return $"Estate: {Number} - {Description}";
        }
    }
}
