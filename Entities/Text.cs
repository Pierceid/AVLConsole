using AVLConsole.Objects;

namespace AVLConsole.Entities {
    public class Text : Item, IKey<Text> {
        public string Value { get; set; }

        public Text() {
            Value = string.Empty;
        }

        public Text(string value) {
            Value = value;
        }

        public int Compare(Text other) {
            return Util.CompareTexts(Value, other.Value);
        }

        public Text ImportKeys(string line) {
            return new Text(line);
        }

        public string ExportKeys() {
            return Value;
        }

        public override Item ImportItem(string line) {
            return new Text(line);
        }

        public override string ExportItem() {
            return Value;
        }
    }
}
