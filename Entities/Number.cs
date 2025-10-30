using AVLConsole.Objects;

namespace AVLConsole.Entities {
    public class Number : Item, IKey<Number> {
        public double Value { get; set; }

        public Number() {
            Value = 0;
        }

        public Number(double value) {
            Value = value;
        }

        public int Compare(Number other) {
            return Util.CompareNumbers(Value, other.Value);
        }

        public Number ImportKeys(string line) {
            if (double.TryParse(line, out double parsedValue)) {
                return new Number(parsedValue);
            }
            throw new FormatException($"Invalid key format: {line}");
        }

        public string ExportKeys() {
            return Value.ToString();
        }

        public override Item ImportItem(string line) {
            if (double.TryParse(line, out double parsedValue)) {
                return new Number(parsedValue);
            }
            throw new FormatException($"Invalid item format: {line}");
        }

        public override string ExportItem() {
            return Value.ToString();
        }
    }
}
