using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SkillTree.Core.Tests
{
    public class ContentTests
    {
        [Fact]
        public void CanCreateTextContent()
        {
            var text = "Some text";

            var content = new TextContent(text);

            Assert.Equal(text, content.Text);
        }
    }
}
