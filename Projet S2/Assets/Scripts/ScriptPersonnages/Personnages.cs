using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Personnages : MonoBehaviour
{
    [SerializeField] protected float Health;
    protected float MaxHealth;
    public Vector3 Coordinates { get; protected set; }
    public float Speed { get; protected set; }
    public float Damages { get; protected set; }
    public List<Items> Bag { get; protected set; }
    protected CharacterController Character;
    protected float Gravity = -9.81f;

    public void ReceiveDamages(float damage) 
    { 
        Health -= damage;
        IsAlive();
    } 

    public void Attack(float damage, Personnages enemy) 
    {
        enemy.ReceiveDamages(damage);
    }

    public bool IsAlive()
    {
        return Health > 0;
    }

    public float GetHealth()
    { 
        return Health; 
    }
}
