using System;
using System.Collections.Generic;

namespace SkillTree.Core
{
    public class Skill
    {
        private readonly List<Skill> subskills;

        private Skill(string name)
        {
            subskills = new List<Skill>();
            Name = name;
        }

        public string Name { get; }

        public TextContent Content { get; private set; }

        public IReadOnlyList<Skill> Subskills => subskills;

        public static Skill Create(string name)
        {
            return new Skill(name);
        }

        public void SetContent(TextContent content)
        {
            Content = content;
        }

        public void AddSubskill(Skill subskill)
        {
            subskills.Add(subskill);
        }
    }
}
