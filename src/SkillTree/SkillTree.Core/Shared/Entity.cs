namespace SkillTree.Core.Shared
{
    public abstract class Entity : ComparableObject<IEntity>, IEntity
    {
        protected Entity(Id id) => Id = id;

        public Id Id { get; }

        public override bool Equals(IEntity other) => Id.Equals(other.Id);

        protected override int[] GetFieldsHashCodes() => new[] { Id.GetHashCode() };
    }
}
