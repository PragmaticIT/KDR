namespace LiteDbTests.TestClasses
{
    public class SimpleClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }

        public override string ToString()
        {
            return string.Format("SimpleClass: Id:{0}, Name:{1}, Surname:{2}, City: {3}", Id, Name, Surname, City);
        }
        public override bool Equals(object obj)
        {
            var other = obj as SimpleClass;
            return other != null
                && Name == other.Name
                && Surname == other.Surname
                && City == other.City;
        }
        public override int GetHashCode()
        {
            //return new { name, Surname, City }.GetHashCode();
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + (Name == null ? 0 : Name.GetHashCode());
                hash = hash * 23 + (Surname == null ? 0 : Surname.GetHashCode());
                hash = hash * 23 + (City == null ? 0 : City.GetHashCode());
                return hash;
            }
        }
    }
}
