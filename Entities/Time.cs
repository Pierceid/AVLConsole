using AVLConsole.Objects;

namespace AVLConsole.Entities {
    public class Time : Item, IKey<Time> {
        public DateTime Value { get; set; }

        public Time() {
            Value = DateTime.Now;
        }

        public Time(DateTime value) {
            Value = value;
        }

        public int Compare(Time other) {
            return Util.CompareTimes(Value, other.Value);
        }

        public Time ImportKeys(string line) {
            if (DateTime.TryParse(line, out DateTime parsedValue)) {
                return new Time(parsedValue);
            }
            throw new FormatException($"Invalid key format: {line}");
        }

        public string ExportKeys() {
            return Value.ToString();
        }

        public override Item ImportItem(string line) {
            if (DateTime.TryParse(line, out DateTime parsedValue)) {
                return new Time(parsedValue);
            }
            throw new FormatException($"Invalid item format: {line}");
        }

        public override string ExportItem() {
            return Value.ToString(Constants.DATETIME_FORMAT);
        }
    }

}
