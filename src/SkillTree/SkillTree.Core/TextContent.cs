using SkillTree.Core.Shared;

namespace SkillTree.Core
{
    public class TextContent : ComparableObject<TextContent>
    {
        public TextContent(string text)
        {
            Text = text;
        }

        public string Text { get; }

        public override bool Equals(TextContent other) => Text == other.Text;

        protected override int[] GetFieldsHashCodes() => new[] { Text.GetHashCode() };
    }
}
