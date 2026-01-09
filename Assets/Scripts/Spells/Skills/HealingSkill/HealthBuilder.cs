using UnityEngine;

public class HealthBuilder : SkillBuilder
{
    private readonly HealthConfig healthConfig;
    public HealthBuilder(HealthConfig skillConfig) : base(skillConfig)
    {
        healthConfig = skillConfig;
    }

    public override void Make()
    {
        skill = new HealingSkill(healthConfig.HealthCount);
        base.Make();
    }
}
