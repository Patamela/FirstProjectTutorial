using System.Diagnostics.Contracts;
using Fusion;
using UnityEngine;

public class BulletMovement : NetworkBehaviour
{
    [SerializeField] private float velocity = 20f;
    [SerializeField] private int damage = 20;

    public override void FixedUpdateNetwork()
    {
        transform.position += transform.forward * velocity * Runner.DeltaTime;
    }

    void OnCollisionEnter(Collision other)
    {
        CharacterHealth health = other.gameObject.GetComponentInParent<CharacterHealth>();
    
        if(health != null)
        {
            health.RPC_TakeDamage(damage);
        }


        if (Object != null && Object.HasStateAuthority)
        {
            Runner.Despawn(Object);
        }
    }

}
