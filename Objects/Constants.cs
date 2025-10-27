namespace AVLConsole.Objects {
    public class Constants {
        public const string DATE_FORMAT = "dd.MM.yyyy";
        public const string DATETIME_FORMAT = "dd.MM.yyyy HH:mm:ss";
        public static string FILE_PATH = Path.GetFullPath(Path.Combine("..", "..", "..", "Files"));
        public static string PERSONS_FILE_PATH = Path.Combine(FILE_PATH, "persons.csv");
        public static string TESTS_FILE_PATH = Path.Combine(FILE_PATH, "tests.csv");
    }
}
