namespace AVLConsole.Objects {
    public class GPS : IKey<GPS> {
        public double X { get; set; }
        public string Latitude { get; set; }
        public double Y { get; set; }
        public string Longitude { get; set; }

        public GPS() {
            X = 0;
            Latitude = "";
            Y = 0;
            Longitude = "";
        }

        public GPS(double x, string latitude, double y, string longitude) {
            X = x;
            Latitude = latitude;
            Y = y;
            Longitude = longitude;
        }

        public int Compare(GPS other) {
            return Util.CompareDoubles(this.X, other.X);
        }

        public bool Equals(GPS other) {
            return X == other.X && Latitude == other.Latitude && Y == other.Y && Longitude == other.Longitude;
        }

        public string GetKeys() {
            return $"GPS,{X.ToString().Replace(',', '.')},{Latitude},{Y.ToString().Replace(',', '.')},{Longitude}";
        }
    }
}
