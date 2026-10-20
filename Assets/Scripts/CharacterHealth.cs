using Fusion;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    private int health = 50;
    public int Health{ get => health;}

    public void RPC_TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log("Vida Actual: "+health);
    }

}
