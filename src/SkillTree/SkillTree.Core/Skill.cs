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

        public IReadOnlyList<Skill> Subskills => subskills;

        public static Skill Create(string name)
        {
            return new Skill(Id.CreateNew(), name);
        }

        public void SetContent(TextContent content)
        {
            Content = content;
        }

        internal void AddSubskill(Skill subskill)
        {
            if (this == subskill)
                throw new ArgumentException("Cannot add skill as its subskill");

            if (ExistsInDescendants(subskill))
                throw new ArgumentException("Subskills already contain the skill");

            if (subskill.ExistsInDescendants(this))
                throw new ArgumentException("The subskill contains the skill in its descendants");

            subskills.Add(subskill);
        }

        private bool ExistsInDescendants(Skill skill)
        {
            return subskills.Any(s => s == skill || s.ExistsInDescendants(skill));
        }
    }
}
