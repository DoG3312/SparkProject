using UnityEngine;

public class HealingSkill : BaseSkill
{
    public float HealthCount {  get; private set; }
    public Controller target;

    public HealingSkill(float healthCount)
    {
        HealthCount = healthCount;
    }

    public override bool CheckCondition(Controller owner, Controller target, Vector3 location = default)
    {
        if(target == null || owner == null) return false;

        if (owner.CompareTag("Player"))
        {
            this.target = target;
            return true;
        }
        return false;
    }

    public override void ApplayCast()
    {
        if(target != null)
        {
            target.ApplyHealing(HealthCount);

            ChangeCooltownTimer(CooldownTime);
            ChangeStatus(SkillStatus.Cooldown);
        }
    }

    public override void EventTick(float deltaTick)
    {
        if(Status == SkillStatus.Cooldown)
        {
            ChangeCooltownTimer(CooldownTimer - Time.deltaTime);

            if (CooldownTimer <= 0) ChangeStatus(SkillStatus.Ready);
        }
    }
}
