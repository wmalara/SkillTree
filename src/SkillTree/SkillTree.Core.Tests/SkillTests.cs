using System;
using System.Linq;
using Xunit;

namespace SkillTree.Core.Tests
{
    public class SkillTests
    {
        [Fact]
        public void CanCreateSkillWithName()
        {
            // Arrange
            var skillName = "Programming";

            // Act
            var skill = Skill.Create(skillName);

            // Assert
            Assert.Equal(skillName, skill.Name);
        }

        [Fact]
        public void CanSetContent()
        {
            // Arrange
            var content = new TextContent("Some text");
            var skill = Skill.Create("Programming");

            // Act
            skill.SetContent(content);

            //Assert
            Assert.Equal(content, skill.Content);
        }

        [Fact]
        public void CanAddSubskills()
        {
            // Arrange
            var skill = Skill.Create("Programming");
            var subskill1 = Skill.Create("Algorithms");
            var subskill2 = Skill.Create("Design patterns");

            // Act
            skill.AddSubskill(subskill1);
            skill.AddSubskill(subskill2);

            // Assert
            Assert.True(skill.Subskills.Contains(subskill1));
            Assert.True(skill.Subskills.Contains(subskill2));
        }
    }
}
