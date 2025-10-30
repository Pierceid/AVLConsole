
namespace AVLConsole.Objects {
    public static class Util {
        private static Random random = new();

        public static int CompareNumbers(double value1, double value2) {
            if (value1 < value2) return -1;
            if (value1 > value2) return 1;
            return 0;
        }

        public static int CompareTexts(string value1, string value2) {
            if (string.Compare(value1, value2) < 0) return -1;
            if (string.Compare(value1, value2) > 0) return 1;
            return 0;
        }

        public static int CompareTimes(DateTime value1, DateTime value2) {
            if (DateTime.Compare(value1, value2) < 0) return -1;
            if (DateTime.Compare(value1, value2) > 0) return 1;
            return 0;
        }

        public static string FormatDoubleForExport(double number) {
            return number.ToString().Replace(',', '.');
        }

        public static string FormatDoubleForImport(string number) {
            return number.Replace('.', ',');
        }

        public static string GenerateRandomString(int length) {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}