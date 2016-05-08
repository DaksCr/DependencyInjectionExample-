using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapiens.Domain.Model
{
    public abstract class Entity : IEquatable<Entity>
    {
        private readonly Guid id;

        protected Entity(Guid id)
        {
            Guard.NotDefault(id, "id");

            this.id = id;
        }

        public Guid Id { get { return this.id; } }

        public bool Equals(Entity other)
        {
            if (other.IsNull())
            {
                return false;
            }

            return this.Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            Entity other = obj as Entity;
            if (other.IsNull())
            {
                return base.Equals(obj);
            }

            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode() ^ 139;
        }
    }
}
