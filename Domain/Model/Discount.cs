using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapiens.Domain.Model
{
    public class Discount : IEquatable<Discount>
    {
        private readonly string description;
        private readonly string code;
        private readonly decimal amount;

        public Discount(string code, string description, decimal amount)
        {
            Guard.NotNullOrEmpty(code, "code");
            Guard.NotNullOrEmpty(description, "description");
            Guard.NotNegativeOrZero(amount, "amount");

            this.code = code;
            this.description = description;
            this.amount = amount;
        }

        public string Code { get { return this.code; } }
        public string Description { get { return this.description; } }
        public decimal Amount { get { return this.amount; } }

        public bool Equals(Discount other)
        {
            if (other.IsNull())
            {
                return false;
            }

            return String.Equals(this.Code, other.Code, StringComparison.InvariantCultureIgnoreCase)
                && String.Equals(this.Description, other.Description, StringComparison.InvariantCultureIgnoreCase)
                && this.Amount == other.Amount;
        }

        public override bool Equals(object obj)
        {
            Discount other = obj as Discount;
            if (other.IsNull())
            {
                return base.Equals(obj);
            }

            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            return this.Code.GetHashCode()
                 ^ this.Description.GetHashCode()
                 ^ this.Amount.GetHashCode()
                 ^ 11;
        }

        public override string ToString()
        {
            return "{0} - {1} [${2}]".FormatWith(this.Code, this.Description, this.Amount);
        }
    }
}
