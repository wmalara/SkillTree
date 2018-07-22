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
        public void SkillsGetUniqueIds()
        {
            // Arrange
            var skills = Enumerable.Range(1, 10).Select(i => Skill.Create("Skill" + i)).ToList();

            // Act
            var uniqueIds = skills.Select(s => s.Id).Distinct();

            // Assert
            Assert.StrictEqual(skills.Count(), uniqueIds.Count());
        }
    }
}
