using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MagicBomb : BaseSkill
{
    public float Damage { get; private set; }
    public float ExplosionRadius { get; private set; }
    public float RepulsionForce {  get; private set; }
    public LayerMask LayerMask { get; private set; }
    public Transform AttackPoint { get; private set; }
    public GameObject BombPrefab { get; private set; }

    [Header("Internal")]
    private float baseThrowForce = 2f;
    private float distanceMultiplier = 0.5f;
    private float upwardModifier = 1f; 



    public MagicBomb(float damage, float explosionRadius, float repulsionForce, LayerMask attackMask, GameObject bombPrefab) 
    {
        Damage = damage;
        ExplosionRadius = explosionRadius;
        RepulsionForce = repulsionForce;
        LayerMask = attackMask;
        BombPrefab = bombPrefab;
    }

    public override void ApplayCast()
    {
        Activate();
        ChangeCooltownTimer(CooldownTime);
        ChangeStatus(SkillStatus.Cooldown);
    }

    public override bool CheckCondition(Controller owner, Controller target, Vector3 location = default)
    {
        AttackPoint = owner.GetAttackPoint();
        return true;
    }

    public override void EventTick(float deltaTick)
    {
        if (Status == SkillStatus.Cooldown)
        {
            ChangeCooltownTimer(CooldownTimer - Time.deltaTime);

            if (CooldownTimer <= 0) ChangeStatus(SkillStatus.Ready);
        }
    }

    public void Activate()
    {
        GameObject bomb = Object.Instantiate(BombPrefab, AttackPoint.position, Quaternion.identity);

        Bomb explosion = bomb.GetComponent<Bomb>();

        explosion.Initialise(LayerMask, Damage, ExplosionRadius, RepulsionForce);

        Rigidbody bombRb = bomb.GetComponent<Rigidbody>();

        bombRb.AddForce(Throw(AttackPoint.position), ForceMode.Impulse);
    }

    public Vector3 Throw(Vector3 attackPoint)
    {
        Vector3 mousePosition = MousePosition.GetMousePosition();

        //расстояние до точки попадания мыши
        float distance = Vector3.Distance(attackPoint, mousePosition);

        //силу броска на основе расстояния
        float throwForce = baseThrowForce + (distance * distanceMultiplier);

        //направление броска
        Vector3 direction = (mousePosition - attackPoint).normalized;
        direction.y += upwardModifier;
        direction *= throwForce;

        return direction;
    }
}
