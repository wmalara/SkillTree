using System;
using System.Linq;
using SkillTree.Core.Shared;
using Xunit;

namespace SkillTree.Core.Tests
{
    public class TreeStructureTests
    {
        [Fact]
        public void CanAddToRootSingleElement()
        {
            var treeStructure = new TreeStructure<int, string>();

            treeStructure.AddToRoot(10, "test");

            var tree = treeStructure.GetTree();
            Assert.StrictEqual(1, tree.Count);
            Assert.StrictEqual(10, tree[0].Key);
            Assert.Equal("test", tree[0].Value);
            Assert.Empty(tree[0].Children);
        }

        [Fact]
        public void CanAddToRootThreeElements()
        {
            var treeStructure = new TreeStructure<int, string>();

            treeStructure.AddToRoot(1, "test1");
            treeStructure.AddToRoot(2, "test2");
            treeStructure.AddToRoot(3, "test3");

            var tree = treeStructure.GetTree();
            Assert.StrictEqual(3, tree.Count);
            Assert.Equal(new[] { 1, 2, 3 }, tree.Select(t => t.Key));
            Assert.Equal(new[] { "test1", "test2", "test3" }, tree.Select(t => t.Value));
            Assert.All(tree.Select(t => t.Children), c => Assert.StrictEqual(c.Count, 0));
        }

        [Fact]
        public void CannotAddTwoSameKeys()
        {
            var treeStructure = new TreeStructure<int, string>();

            treeStructure.AddToRoot(1, "test1");

            Assert.ThrowsAny<ArgumentException>(() => treeStructure.AddToRoot(1, "test2"));
        }

        [Fact]
        public void CanAddChildren()
        {
            var treeStructure = new TreeStructure<int, string>();

            treeStructure.AddToRoot(1, "test1");
            treeStructure.Add(2, "test2", 1);
            treeStructure.Add(3, "test3", 1);

            var tree = treeStructure.GetTree();
            Assert.StrictEqual(1, tree.Count);
            Assert.StrictEqual(1, tree[0].Key);
            Assert.Equal("test1", tree[0].Value);
            Assert.StrictEqual(2, tree[0].Children.Count);
            Assert.Equal(new[] { 2, 3 }, tree[0].Children.Select(c => c.Key));
            Assert.Equal(new[] { "test2", "test3" }, tree[0].Children.Select(c => c.Value));
            Assert.All(tree[0].Children.Select(t => t.Children), c => Assert.StrictEqual(c.Count, 0));
        }

        [Fact]
        public void CannotAddTwoSameChildrenKeysForDifferentParents()
        {
            var treeStructure = new TreeStructure<int, string>();

            treeStructure.AddToRoot(1, "test1");
            treeStructure.AddToRoot(2, "test2");

            treeStructure.Add(3, "test3", 1);
            Assert.ThrowsAny<ArgumentException>(() => treeStructure.Add(3, "test4", 2));
        }
    }
}