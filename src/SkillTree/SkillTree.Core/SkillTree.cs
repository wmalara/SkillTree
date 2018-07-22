using SkillTree.Core.Shared;
using System;
using System.Collections.Generic;

namespace SkillTree.Core
{
    public class SkillTree
    {
        private readonly TreeStructure<Id, Skill> skills;

        public SkillTree()
        {
            skills = new TreeStructure<Id, Skill>();
        }
    }
}
