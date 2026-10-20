using Fusion;
using UnityEngine;

public class BulletMovement : NetworkBehaviour
{
    [SerializeField] private float velocity = 20f;
    [SerializeField] private int damage = 10;

    public override void FixedUpdateNetwork()
    {
        Debug.Log("Posicion actualizada: "+transform.position);
        transform.position += transform.forward * velocity * Runner.DeltaTime;
    }

    void OnCollisionEnter(Collision other)
    {
        CharacterHealth health = other.gameObject.GetComponentInParent<CharacterHealth>();
    
        if(health != null)
        {
            health.RPC_TakeDamage(damage);
        }
    }
}
