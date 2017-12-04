using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Kdr.Core
{
    public class Gestor : IHasId
    {
        //Nazwa, adres, telefon, kategorie, opis ulgi, opis gestora, 
        //  strona www, mail, logo gestora
        private IEnumerable<Category> _categories;
        private Address _address;

        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address
        {
            get
            {

                return _address ?? (_address = new Address());

            }

            set
            {

                _address = value;


            }
        } //godziny otwarcia
        public IEnumerable<Category> Categories
        {
            get
            {
                return _categories ?? (_categories = new List<Category>());
            }
            set
            {
                _categories = value;
            }
        }
        public string DiscountDescription { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Email { get; set; }
        public byte[] LogoImage { get; set; }


        public override bool Equals(object obj)
        {
            var other = obj as Gestor;
            return other != null
                && Id == other.Id
                && Name == other.Name
                && Address.Equals(other.Address)
                && DiscountDescription == other.DiscountDescription
                && Description == other.Description
                && Url == other.Url
                && Email == other.Email;
                
        }
                
        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + Id ;
                hash = hash * 23 + (Name == null ? 0 : Name.GetHashCode());
                
                hash = hash * 23 + Address.GetHashCode();
                hash = hash * 23 + (DiscountDescription == null ? 0 : DiscountDescription.GetHashCode());
                hash = hash * 23 + (Description == null ? 0 : Description.GetHashCode());
                hash = hash * 23 + (Url == null ? 0 : Url.GetHashCode());
                hash = hash * 23 + (Email == null ? 0 : Email.GetHashCode());
                return hash;
            }
        }


    }
}
