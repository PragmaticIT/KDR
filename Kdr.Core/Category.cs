namespace Kdr.Core
{
    public class Category : IHasId
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Category;

            return other != null && other.Id == Id && other.Name == Name;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + Id.GetHashCode();
                hash = hash * 23 + (Name == null ? 0 : Name.GetHashCode());

                return hash;
            }
        }
    }

    
}