using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapiens.Domain.Model
{
    public class Product : Entity, IEquatable<Product>
    {
        private string name;
        private string description;
        private decimal price;

        public Product(Guid id, string name, string description, decimal price)
            : base(id)
        {
            this.Name = name;
            this.Description = description;
            this.Price = price;
        }

        public string Name
        {
            get { return this.name; }
            set {
                Guard.NotNullOrEmpty(value, "name");
                this.name = value;
            }
        }
        public string Description
        {
            get { return this.description; }
            set
            {
                Guard.NotNullOrEmpty(value, "description");
                this.description = value;
            }
        }
        public decimal Price
        {
            get { return this.price; }
            set
            {
                Guard.NotNegativeOrZero(value, "price");
                this.price = value;
            }
        }

        public bool Equals(Product other)
        {
            if (other.IsNull())
            {
                return false;
            }
            return this.Id == other.Id;
        }

        public override string ToString()
        {
            return "{0} [${1}]".FormatWith(this.Name, this.Price);
        }
    }
}
