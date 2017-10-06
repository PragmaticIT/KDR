namespace Kdr.Core
{
    public class Location
    {
        public string Lat { get; set; }
        public string Long { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Location;
            return other != null
                && Lat == other.Lat
                && Long == other.Long;
        }
        public override int GetHashCode()
        {
            // return new { Lat, Long }.GetHashCode();

            unchecked
            {
                var hash = 17;
                hash = hash * 23 + (Lat == null ? 0 : Lat.GetHashCode());
                hash = hash * 23 + (Long == null ? 0 : Long.GetHashCode());
                
                return hash;
            }

        }
    }
}