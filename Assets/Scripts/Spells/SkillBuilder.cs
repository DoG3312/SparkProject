using UnityEngine;

public class SkillBuilder 
{
    private SkillConfig config;
    protected BaseSkill skill;

    public SkillBuilder(SkillConfig skillConfig)
    {
        config = skillConfig;
    }

    public virtual void Make()
    {
        if(skill != null)
        {
            skill.SetDescription(config.Title, config.Description, config.IconImage);
            skill.SetCooldownTime(config.CooldownTime);
            skill.ChangeStatus(SkillStatus.Ready);
        }
    }

    public virtual BaseSkill GetResult() => skill;
}
