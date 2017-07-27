using Xunit;

namespace SkillTree.Core.Tests
{
    public class ContentTests
    {
        [Fact]
        public void CanCreateTextContent()
        {
            //Arrange
            var text = "Some text";

            //Act
            var content = new TextContent(text);

            //Assert
            Assert.Equal(text, content.Text);
        }
    }
}
