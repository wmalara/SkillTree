using SkillTree.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SkillTree.Core
{
    public class Skill : Entity
    {
        private readonly List<Skill> subskills;

        private Skill(Id id, string name) : base(id)
        {
            Name = name;
            subskills = new List<Skill>();
        }

        public string Name { get; }

        public TextContent Content { get; private set; }

        public static Skill Create(string name)
        {
            return new Skill(Id.CreateNew(), name);
        }

        public void SetContent(TextContent content)
        {
            Content = content;
        }
    }
}
