using UnityEngine;

[CreateAssetMenu]
public class MagicBombConfig : SkillConfig
{
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float ExplosionRadius { get; private set; }
    [field: SerializeField] public float RepulsionForce { get; private set; }
    [field: SerializeField] public LayerMask LayerMask { get; private set; }
    [field: SerializeField] public GameObject BombPrefab { get; private set; }

    public override SkillBuilder GetBuildet()
    {
        return new MagicBombBuilder(this);
    }
}
