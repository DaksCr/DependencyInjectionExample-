using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapiens.Domain.Model
{
    public class Tax : IEquatable<Tax>
    {
        private readonly string description;
        private readonly decimal amount;

        public Tax(string description, decimal amount)
        {
            Guard.NotNullOrEmpty(description, "description");
            Guard.NotNegativeOrZero(amount, "amount");

            this.description = description;
            this.amount = amount;
        }

        public string Description { get { return this.description; } }
        public decimal Amount { get { return this.amount; } }

        public bool Equals(Tax other)
        {
            if (other.IsNull())
            {
                return false;
            }

            return String.Equals(this.Description, other.Description, StringComparison.InvariantCultureIgnoreCase)
                && this.Amount == other.Amount;
        }

        public override bool Equals(object obj)
        {
            Tax other = obj as Tax;
            if (other.IsNull())
            {
                return base.Equals(obj);
            }

            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            return this.Description.GetHashCode()
                 ^ this.Amount.GetHashCode()
                 ^ 11;
        }

        public override string ToString()
        {
            return "{0} [${1}]".FormatWith(this.Description, this.Amount);
        }
    }
}
