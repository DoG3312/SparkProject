using UnityEngine;

public class MagicBombBuilder : SkillBuilder
{
    private readonly MagicBombConfig magicBombConfig;
    public MagicBombBuilder(MagicBombConfig skillConfig) : base(skillConfig)
    {
        magicBombConfig = skillConfig;
    }

    public override void Make()
    {
        skill = new MagicBomb(
            magicBombConfig.Damage,
            magicBombConfig.ExplosionRadius,
            magicBombConfig.RepulsionForce,
            magicBombConfig.LayerMask,
            magicBombConfig.BombPrefab
            );
        base.Make();
    }
}
