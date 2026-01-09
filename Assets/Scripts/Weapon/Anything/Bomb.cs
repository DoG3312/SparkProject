using UnityEngine;

public class Bomb : MonoBehaviour
{
    private LayerMask attackMask;
    private LayerMask surface;
    private float damage;
    private float explosionRadius;
    private float repulsionForce;

    void Awake() 
    {
        surface = LayerMask.GetMask("Surface");
    }

    public void Initialise(LayerMask layer, float damage, float explosionRadius, float repulsionForce)
    {
        attackMask = layer;
        this.damage = damage;
        this.explosionRadius = explosionRadius;
        this.repulsionForce = repulsionForce;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((surface & (1 << other.gameObject.layer)) != 0)
        {
            CreatingDamageArea.AbilityDamageArea(transform.position, attackMask, explosionRadius, damage, repulsionForce);
            Destroy(gameObject);
        }
        
    }
}
