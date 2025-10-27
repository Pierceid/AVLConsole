using AVLConsole.Objects;

namespace AVLConsole.Entities {
    public class Number : Item, IKey<Number> {
        public int Value { get; set; }

        public Number() {
            Value = 0;
        }

        public Number(int value) {
            Value = value;
        }

        public int Compare(Number other) {
            return Util.CompareIntegers(Value, other.Value);
        }

        public Number ImportKeys(string line) {
            if (int.TryParse(line, out int parsedValue)) {
                return new Number(parsedValue);
            }
            throw new FormatException($"Invalid key format: {line}");
        }

        public string ExportKeys() {
            return Value.ToString();
        }

        public override Item ImportItem(string line) {
            if (int.TryParse(line, out int parsedValue)) {
                return new Number(parsedValue);
            }
            throw new FormatException($"Invalid item format: {line}");
        }

        public override string ExportItem() {
            return Value.ToString();
        }
    }
}
