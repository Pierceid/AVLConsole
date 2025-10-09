using AVLConsole.Entities;

namespace AVLConsole.Objects {
    public class Number : Item, IKey<Number> {
        public int Value { get; set; }

        public Number() {
            Value = 0;
        }

        public int Compare(Number other) {
            return Util.CompareIntegers(Value, other.Value);
        }

        public bool Equals(Number other) {
            return Value == other.Value;
        }

        public string GetKeys() {
            return $"Number,{Value}";
        }

        public override void PrintInfo() {
            Console.WriteLine($"Number: {Value}");
        }

        public override string GetInfo() {
            return $"Number,{Value}";
        }

        public override string? ToString() {
            return $"Number: {Value}";
        }
    }
}
