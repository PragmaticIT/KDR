using System.Collections.Generic;
using System.Linq;

namespace MongoTests.TestClasses
{
    public class ComplexClass
    {
        public int Id { get; set; }
        public SimpleClass Person1 { get; set; }
        public SimpleClass Person2 { get; set; }

        private IEnumerable<SimpleClass> _children;
        public IEnumerable<SimpleClass> Children {
            get { return _children ?? (_children = new List<SimpleClass>()); }
            set { _children = value; }
        }

        public string Remarks { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as ComplexClass;
            return other != null
                && Remarks == other.Remarks
                && ((Person1 == null && other.Person1 == null) || (Person1 != null && Person1.Equals(other.Person1)))
                && ((Person2 == null && other.Person2 == null) || (Person2 != null && Person2.Equals(other.Person2)))
                && ((Children == null && other.Children == null) || (Children != null && other.Children != null && Children.All(x => other.Children.Any(o => o.Equals(x)))));
        }
        public override int GetHashCode()
        {
            unchecked {
                var hash = 17;
                hash = hash * 23 + (Person1 == null ? 0 : Person1.GetHashCode());
                hash = hash * 23 + (Person2 == null ? 0 : Person2.GetHashCode());
                hash = hash * 23 + Children.GetHashCode();
                hash = hash * 23 + (Remarks == null ? 0 : Remarks.GetHashCode());
                return hash;
            }
        }
    }
}
