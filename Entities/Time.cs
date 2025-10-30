using AVLConsole.Objects;

namespace AVLConsole.Entities {
    public class Time : Item, IKey<Time> {
        public DateTime Value { get; set; }
        public int Code { get; set; }

        public Time() {
            Value = DateTime.Now;
            Code = 0;
        }

        public Time(DateTime value, int code) {
            Value = value;
            Code = code;
        }

        public int Compare(Time other) {
            int cmp = Util.CompareTimes(Value, other.Value);
            return cmp == 0 ? Util.CompareNumbers(Code, other.Code) : cmp;
        }

        public Time ImportKeys(string line) {
            string[] parts = line.Split(',');
            if (DateTime.TryParse(parts[0], out DateTime parsedValue) && int.TryParse(parts[1], out int parsedCode)) {
                return new Time(parsedValue, parsedCode);
            }
            throw new FormatException($"Invalid key format: {line}");
        }

        public string ExportKeys() {
            return Value.ToString();
        }

        public override Item ImportItem(string line) {
            string[] parts = line.Split(',');
            if (DateTime.TryParse(parts[0], out DateTime parsedValue) && int.TryParse(parts[1], out int parsedCode)) {
                return new Time(parsedValue, parsedCode);
            }
            throw new FormatException($"Invalid item format: {line}");
        }

        public override string ExportItem() {
            return $"{Value.ToString(Constants.DATETIME_FORMAT)},{Code}";
        }
    }

}
