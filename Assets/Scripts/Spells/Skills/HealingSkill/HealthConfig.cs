using UnityEngine;

[CreateAssetMenu]
public class HealthConfig : SkillConfig
{
    [field: SerializeField] public float HealthCount { get; private set; }

    public override SkillBuilder GetBuildet()
    {
        return new HealthBuilder(this);
    }
}
