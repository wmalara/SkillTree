using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SkillTree.Core.Tests
{
    public class ValueObjectTests
    {
        [Fact]
        public void EqualsForStructuralEquality()
        {
            //Arrange
            var first = new TestValueObject(123, "test");
            var second = new TestValueObject(123, "test");

            //Act
            //Assert
            Assert.True(first.Equals(second));
            Assert.True(first.Equals((object)second));
            Assert.True(object.Equals(first, second));
            Assert.True(first == second);
            Assert.False(first != second);
        }
        
        [Fact]
        public void EqualsForReferentialEquality()
        {
            //Arrange
            var first = new TestValueObject(123, "test");
            var second = first;

            //Act
            //Assert
            Assert.True(first.Equals(second));
            Assert.True(first.Equals((object)second));
            Assert.True(object.Equals(first, second));
            Assert.True(first == second);
            Assert.False(first != second);
        }

        [Theory]
        [InlineData(123, "test", 321, "test")]
        [InlineData(123, "test", 123, "test1")]
        [InlineData(123, "test", 321, "test1")]
        public void DoesntEqualForStructuralAndReferentialInequality(int age1, string name1, int age2, string name2)
        {
            //Arrange
            var first = new TestValueObject(age1, name1);
            var second = new TestValueObject(age2, name2);
            
            //Act
            //Assert
            Assert.False(first.Equals(second));
            Assert.False(first.Equals((object)second));
            Assert.False(object.Equals(first, second));
            Assert.False(first == second);
            Assert.True(first != second);
        }

        [Fact]
        public void DoesntEqualNull()
        {
            //Arrange
            var first = new TestValueObject(123, "test");
            TestValueObject second = null;

            //Act
            //Assert
            Assert.False(first.Equals(second));
            Assert.False(first.Equals((object)second));
            Assert.False(object.Equals(first, second));
            Assert.False(first == second);
            Assert.True(first != second);
        }

        [Fact]
        public void DoesntEqualObjectOfOtherType()
        {
            //Arrange
            var sut = new TestValueObject(123, "test");
            var other = "test";

            //Act
            //Assert
            Assert.False(sut.Equals(other));
            Assert.False(object.Equals(sut, other));
        }

        [Fact]
        public void HashCodeIsNotZero()
        {
            //Arrange
            var sut = new TestValueObject(123, "test");

            //Act
            var hashCode = sut.GetHashCode();

            //Assert
            Assert.NotEqual(0, hashCode);
        }

        [Fact]
        public void GetHashCodeReturnsTheSameValueForSameObject()
        {
            //Arrange
            var sut = new TestValueObject(123, "test");

            //Act
            var firstHashCode = sut.GetHashCode();
            var secondHashCode = sut.GetHashCode();

            //Assert
            Assert.Equal(firstHashCode, secondHashCode);
        }


        [Fact]
        public void GetHashCodeReturnsTheSameValueForEqualObject()
        {
            //Arrange
            var first = new TestValueObject(123, "test");
            var second = new TestValueObject(123, "test");

            //Act
            var firstHashCode = first.GetHashCode();
            var secondHashCode = second.GetHashCode();

            //Assert
            Assert.Equal(firstHashCode, secondHashCode);
        }

        [Theory]
        [InlineData(123, "test", 321, "test")]
        [InlineData(123, "test", 123, "test1")]
        [InlineData(123, "test", 321, "test1")]
        public void GetHashCodeReturnsDifferentValuesForNonEqualObject(int age1, string name1, int age2, string name2)
        {
            //Arrange
            var first = new TestValueObject(age1, name1);
            var second = new TestValueObject(age2, name2);

            //Act
            var firstHashCode = first.GetHashCode();
            var secondHashCode = second.GetHashCode();

            //Assert
            Assert.NotEqual(secondHashCode, firstHashCode);
        }

        class TestValueObject : ValueObject<TestValueObject>
        {
            private readonly int age;
            private readonly string name;

            public TestValueObject(int age, string name)
            {
                this.age = age;
                this.name = name;
            }

            public override bool Equals(TestValueObject other)
            {
                return age == other?.age && name == other?.name;
            }

            protected override int[] GetFieldsHashCodes()
            {
                return new[] { age.GetHashCode(), name.GetHashCode() };
            }
        }
    }
}
