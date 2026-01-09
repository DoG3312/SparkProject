using UnityEngine;

public class SkillConfig : Item
{
    [Header("Skill")]
    [field: SerializeField] public float CooldownTime { get; private set; }

    public virtual SkillBuilder GetBuildet() => new SkillBuilder(this);
}
