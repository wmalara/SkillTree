using SkillTree.Core.Shared;
using System.Collections.Generic;

namespace SkillTree.Core
{
    public class SkillTree
    {
        private readonly Dictionary<Id, Skill> skills;

        public SkillTree()
        {
            skills = new Dictionary<Id, Skill>();
        }

        public IReadOnlyCollection<Skill> Skills => skills.Values;

        public void AddSkill(Skill skill)
        {
            skills.Add(skill.Id, skill);
        }
    }
}
