using System;
using System.Collections.Generic;
using System.Text;

namespace SkillTree.Core
{
    public class Id : ValueObject<Id>
    {
        private readonly Guid guid;

        public Id(Guid guid)
        {
            this.guid = guid;
            
        }

        public static Id CreateNew()
        {
            return new Id(Guid.NewGuid());
        }

        public override string ToString() => guid.ToString();

        public override bool Equals(Id other) => guid == other?.guid;

        protected override int[] GetFieldsHashCodes() => new[] { guid.GetHashCode() };
    }
}
