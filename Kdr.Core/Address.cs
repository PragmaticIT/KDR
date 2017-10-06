using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kdr.Core
{
    public class Address
    {
        private Location _location;

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        public Location Location
        {
            get
            {
                return _location ?? (_location = new Location());
            }

            set
            {
                _location = value;

            }
        }

        public override bool Equals(object obj)
        {
            var other = obj as Address;
            return other != null
                && AddressLine1 == other.AddressLine1
                && AddressLine2 == other.AddressLine2
                && Location.Equals(other.Location);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + (AddressLine1 == null ? 0 : AddressLine1.GetHashCode());
                hash = hash * 23 + (AddressLine2 == null ? 0 : AddressLine2.GetHashCode());
                hash = hash * 23 + Location.GetHashCode();

                return hash;
            }
        }
    }
}
