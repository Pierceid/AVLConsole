using AVLConsole.Objects;

namespace AVLConsole.Entities {
    public class Parcel : Item {
        public int Number { get; set; }
        public string Description { get; set; }
        public GPS Position { get; set; }

        public Parcel(int number, string description, GPS position) {
            Number = number;
            Description = description;
            Position = position;
        }

        public override string GetInfo() {
            return $"Parcel,{Id},{Number},{Description},{Position.GetKeys()}";
        }

        public override string? ToString() {
            return $"Parcel: {Number} - {Description}";
        }
    }
}
