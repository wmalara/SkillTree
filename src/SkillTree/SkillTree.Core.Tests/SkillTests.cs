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

        [Fact]
        public void CannotAddSameSubskills()
        {
            // Arrange
            var skill = Skill.Create("Programming");
            var subskill = Skill.Create("Algorithms");

            // Act
            skill.AddSubskill(subskill);

            // Assert
            Assert.ThrowsAny<Exception>(() => skill.AddSubskill(subskill));
        }

        [Fact]
        public void CannotAddSkillAsSubskill()
        {
            // Arrange
            var skill = Skill.Create("Programming");

            // Act
            // Assert
            Assert.ThrowsAny<Exception>(() => skill.AddSubskill(skill));
        }

        [Fact]
        public void CannotAddParentAsSubskill()
        {
            // Arrange
            var skill = Skill.Create("Programming");
            var subskill = Skill.Create("Algorithms");

            // Act
            skill.AddSubskill(subskill);

            // Assert
            Assert.ThrowsAny<Exception>(() => subskill.AddSubskill(skill));
        }

        [Fact]
        public void CannotAddSkillAsSubsubskill()
        {
            // Arrange
            var skill = Skill.Create("Programming");
            var subskill = Skill.Create("Algorithms");
            var subsubskill = Skill.Create("Sorting");

            // Act
            skill.AddSubskill(subskill);
            subskill.AddSubskill(subsubskill);

            // Assert
            Assert.ThrowsAny<Exception>(() => subsubskill.AddSubskill(skill));
        }

        [Fact]
        public void CannotAddSubskillWithSkillAsSubskill()
        {
            // Arrange
            var skill = Skill.Create("Programming");
            var subskill = Skill.Create("Algorithms");
            var subsubskill = Skill.Create("Sorting");

            // Act
            subskill.AddSubskill(subsubskill);
            subsubskill.AddSubskill(skill);

            // Assert
            Assert.ThrowsAny<Exception>(() => skill.AddSubskill(subskill));
        }

        [Fact]
        public void CannotAddSubsubskillAsSubskill()
        {
            // Arrange
            var skill = Skill.Create("Programming");
            var subskill = Skill.Create("Algorithms");
            var subsubskill = Skill.Create("Sorting");

            // Act
            skill.AddSubskill(subskill);
            subskill.AddSubskill(subsubskill);

            // Assert
            Assert.ThrowsAny<Exception>(() => skill.AddSubskill(subsubskill));
        }

        [Fact]
        public void CannotAddSubskillWhichHasTheSkillInDescendants()
        {
            // Arrange
            var skill = Skill.Create("Programming");
            var subskill = Skill.Create("Algorithms");

            // Act
            subskill.AddSubskill(skill);

            // Assert
            Assert.ThrowsAny<Exception>(() => skill.AddSubskill(subskill));
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
