using System;
using System.Linq;

namespace SkillTree.Core.Shared
{
    public abstract class ComparableObject<T> : IEquatable<T>
    {
        public static bool operator == (ComparableObject<T> first, T second) => first.Equals(second);

        public static bool operator != (ComparableObject<T> first, T second) => first.Equals(second) == false;

        public abstract bool Equals(T other);

        public override bool Equals(object obj) => obj is T ? Equals((T)obj) : false;

        public override int GetHashCode() => 
            GetFieldsHashCodes().Aggregate(17, (hash, fieldHash) => hash * 31 + fieldHash);

        protected abstract int[] GetFieldsHashCodes();
    }
}