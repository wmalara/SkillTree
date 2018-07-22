using System;
using System.Linq;
using System.Collections.Generic;

namespace SkillTree.Core.Shared
{
    public class TreeStructure<K, V>
    {
        private readonly Dictionary<K, TreeNodeInner<K, V>> nodeLookup;

        public TreeStructure()
        {
            this.nodeLookup = new Dictionary<K, TreeNodeInner<K, V>>();
        }

        public void AddToRoot(K key, V value)
        {
            AssertNotNull(key, nameof(key));
            AssertKeyNotExists(key);

            this.nodeLookup.Add(key, new TreeNodeInner<K, V> { Value = value });
        }

        public void Add(K key, V value, K parentKey)
        {
            AssertNotNull(key, nameof(key));
            AssertNotNull(parentKey, nameof(parentKey));
            AssertKeyNotExists(key);
            AssertKeyExists(parentKey);

            var parentNode = this.nodeLookup[parentKey];

            var newNode = new TreeNodeInner<K, V> 
            { 
                Value = value,
                HasParent = true,
                Parent = parentKey 
            };
            this.nodeLookup.Add(key, newNode);
            parentNode.Children.Add(key);
        }

        public void RemoveWithChildren(K key)
        {
            AssertNotNull(key, nameof(key));
            AssertKeyExists(key);

            var node = this.nodeLookup[key];

            if (node.HasParent)
            {
                var parentNode = this.nodeLookup[node.Parent];
                parentNode.Children.Remove(key);
            }

            foreach (var childKey in node.Children)
                this.nodeLookup.Remove(childKey);

            this.nodeLookup.Remove(key);
        }

        public void Move(K key, K newParentKey)
        {
            AssertNotNull(key, nameof(key));
            AssertNotNull(newParentKey, nameof(newParentKey));
            AssertKeyExists(key);
            AssertKeyExists(newParentKey);

            var node = this.nodeLookup[key];

            if (node.HasParent)
            {
                var oldParent = this.nodeLookup[node.Parent];
                oldParent.Children.Remove(key);
            }

            var newParent = this.nodeLookup[newParentKey];
            newParent.Children.Add(key);
            node.Parent = newParentKey;
        }

        public void MoveToRoot(K key)
        {
            AssertNotNull(key, nameof(key));
            AssertKeyExists(key);

            var node = this.nodeLookup[key];

            if (node.HasParent)
            {
                var oldParent = this.nodeLookup[node.Parent];
                oldParent.Children.Remove(key);
            }

            node.HasParent = false;
            node.Parent = default(K);
        }

        public V Get(K key)
        {
            AssertNotNull(key, nameof(key));
            AssertKeyExists(key);

            return this.nodeLookup[key].Value;
        }

        public K GetParentKey(K key)
        {
            AssertNotNull(key, nameof(key));
            AssertKeyExists(key);

            return this.nodeLookup[key].Parent;
        }

        public IList<K> GetChildrenKeys(K key)
        {
            AssertNotNull(key, nameof(key));
            AssertKeyExists(key);

            return this.nodeLookup[key].Children;
        }

        public IList<TreeNode<K, V>> GetTree()
        {
            var rootNodes = this.nodeLookup
                                .Where(kvp => kvp.Value.HasParent == false)
                                .Select(kvm => kvm.Key);

            return CreateTree(rootNodes);

            List<TreeNode<K, V>> CreateTree(IEnumerable<K> children)
            {
                return children
                    .Select(childKey => new { childKey, node = this.nodeLookup[childKey] })
                    .OrderBy(keNode => keNode.node.Order)
                    .Select(keyNode => new TreeNode<K, V>
                    {
                        Key = keyNode.childKey,
                        Value = keyNode.node.Value,
                        Children = CreateTree(keyNode.node.Children)
                    }).ToList();
            }
        }

        private void AssertNotNull(K key, string argName)
        {
            if (key == null)
                throw new ArgumentNullException(argName);
        }

        private void AssertKeyExists(K key)
        {
            if (this.nodeLookup.ContainsKey(key) == false)
                throw new ArgumentException("Key not in the tree");
        }

        private void AssertKeyNotExists(K key)
        {
            if (this.nodeLookup.ContainsKey(key))
                throw new ArgumentException("Element with this key is already in the tree");
        }

        private class TreeNodeInner<K, V>
        {
            public V Value { get; set; }

            public int Order { get; set; }

            public List<K> Children { get; set; } = new List<K>();

            public bool HasParent { get; set; }

            public K Parent { get; set; }
        }
    }

    public class TreeNode<K, V>
    {
        public K Key { get; set; }

        public V Value { get; set; }

        public List<TreeNode<K, V>> Children { get; set; } = new List<TreeNode<K, V>>();
    }
}