namespace AVLConsole.Objects {
    public class GPS : IKey<GPS> {
        public double LatitudeValue { get; }
        public string LatitudeDirection { get; }
        public double LongitudeValue { get; }
        public string LongitudeDirection { get; }

        public GPS() {
            LatitudeValue = 0;
            LatitudeDirection = "N";
            LongitudeValue = 0;
            LongitudeDirection = "E";
        }

        public GPS(double latitudeValue, string latitudeDirection, double longitudeValue, string longitudeDirection) {
            LatitudeValue = latitudeValue;
            LatitudeDirection = latitudeDirection;
            LongitudeValue = longitudeValue;
            LongitudeDirection = longitudeDirection;
        }

        public int Compare(GPS other) {
            int result = Util.CompareDoubles(LatitudeValue, other.LatitudeValue);

            if (result != 0) {
                return result;
            } else {
                return Util.CompareDoubles(LongitudeValue, other.LongitudeValue);
            }
        }

        public string GetKeys() {
            return $"GPS,{Util.FormatDoubleForExport(LatitudeValue)},{LatitudeDirection}," +
                $"{Util.FormatDoubleForExport(LongitudeValue)},{LongitudeDirection}";
        }
    }
}
