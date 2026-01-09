using UnityEngine;

public static class CreatingDamageArea
{
    public static void AbilityDamageArea(Vector3 position, LayerMask layer, float explosionRadius, float skillDamage, float repulsionForce)
    {
        Collider[] colliders = Physics.OverlapSphere(position, explosionRadius, layer);
        foreach (Collider hit1 in colliders)
        {
            Controller health = hit1.GetComponent<Controller>();
            if (health != null)
            {
                health.TakeDamage(skillDamage, repulsionForce, position);
            }
        }
    }
}

