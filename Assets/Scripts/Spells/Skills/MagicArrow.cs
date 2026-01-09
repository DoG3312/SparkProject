using UnityEngine;

public class MagicArrow : BaseSkill
{
    protected float skillDamage;
    protected IAttackMask layerMask;
    protected Transform attackPoint;

    protected float bulletSpeed = 12;



    public MagicArrow(float skillCooldown, float damage, IAttackMask attackMask, Transform attackPoint)
    {
        skillDamage = damage;
        layerMask = attackMask;
        this.attackPoint = attackPoint;
    }

    public void Activate()
    {
        Bullet bulletObject = BulletPoolManager.Instance.GetBullet();

        if (bulletObject == null)
        {
            return;
        }

        Quaternion yRotation = Quaternion.Euler(0f, attackPoint.rotation.eulerAngles.y, 0f);
        bulletObject.transform.SetPositionAndRotation(attackPoint.position, yRotation);

        bulletObject.Initialize(bulletSpeed, skillDamage, 5, 5, layerMask);
        bulletObject.Deactivate();
    }
}
