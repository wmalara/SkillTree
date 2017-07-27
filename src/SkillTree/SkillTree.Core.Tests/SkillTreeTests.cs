using System.Linq;
using Xunit;

namespace SkillTree.Core.Tests
{
    public class SkillTreeTests
    {
        [Fact]
        public void CanAddSkills()
        {
            // Arrange
            var root = new SkillTree();
            var skill1 = Skill.Create("Programming");
            var skill2 = Skill.Create("Economics");

            // Act
            root.AddSkill(skill1);
            root.AddSkill(skill2);

            // Assert
            Assert.True(root.Skills.Contains(skill1));
            Assert.True(root.Skills.Contains(skill2));
        }
    }
}
