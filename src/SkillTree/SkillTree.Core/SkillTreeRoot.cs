using System;
using System.Collections.Generic;
using System.Text;

namespace SkillTree.Core
{
    public class SkillTreeRoot
    {
        private readonly List<Skill> skills;

        public SkillTreeRoot()
        {
            skills = new List<Skill>();
        }

        public IReadOnlyList<Skill> Skills => skills;

        public void AddSkill(Skill skill)
        {
            skills.Add(skill);
        }
    }
}
