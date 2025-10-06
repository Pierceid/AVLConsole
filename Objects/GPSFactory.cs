namespace AVLConsole.Objects {
    public class GPSFactory {
        private static GPSFactory? instance = null;
        private static readonly object lockObj = new();
        private readonly Dictionary<string, GPS> gpsPool = new();

        private GPSFactory() { }

        public static GPSFactory GetInstance() {
            if (instance == null) {
                lock (lockObj) {
                    instance ??= new GPSFactory();
                }
            }

            return instance;
        }
        public GPS GetGPS(double latitudeValue, string latitudeDirection, double longitudeValue, string longitudeDirection) {
            string key = string.Join("_", latitudeValue, latitudeDirection, longitudeValue, longitudeDirection);

            lock (lockObj) {
                if (!gpsPool.TryGetValue(key, out GPS? gps)) {
                    gps = new GPS(latitudeValue, latitudeDirection, longitudeValue, longitudeDirection);
                    gpsPool[key] = gps;
                }

                return gps;
            }
        }
    }
}