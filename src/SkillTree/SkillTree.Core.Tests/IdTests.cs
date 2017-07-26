using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SkillTree.Core.Tests
{
    public class IdTests
    {
        [Fact]
        public void CanCreateWithGuid()
        {
            //Arrange
            var guid = new Guid();

            //Act
            //Assert
            var id = new Id(guid);
        }

        [Fact]
        public void ToStringReturnsUnderlyingGuid()
        {
            //Arrange
            var guid = new Guid();

            //Act
            var id = new Id(guid);

            //Assert
            Assert.Equal(guid.ToString(), id.ToString());
        }

        [Fact]
        public void CanGenerateNewValue()
        {
            //Act
            var id = Id.CreateNew();
            
            //Assert
            Assert.NotEqual(Guid.Empty.ToString(), id.ToString());
        }

        [Fact]
        public void AreTwoIdsWithSameGuidEqual()
        {
            //Arrange
            var guid = new Guid();

            //Act
            var id1 = new Id(guid);
            var id2 = new Id(guid);

            //Assert
            Assert.True(id1 == id2);
            Assert.StrictEqual(id1, id2);
        }
    }
}
