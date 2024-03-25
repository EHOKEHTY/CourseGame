using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Player Movement Settings")]
    [SerializeField] protected float Speed = 3.0f;
    [SerializeField] protected float RunSpeed = 5.0f;
    [SerializeField] internal int HP = 100;
    [SerializeField] internal int MaxHP = 100;


    void Start()
    {
    
    }

    protected virtual void Walk(Vector3 moveTarget)
    {
        transform.position = Vector3.MoveTowards(transform.position, moveTarget, Time.deltaTime * Speed);
    }

    protected virtual void Run(Vector3 moveTarget)
    {
        transform.position = Vector3.MoveTowards(transform.position, moveTarget, Time.deltaTime * RunSpeed);
    }

    internal void Damage(int damage)
    {
        HP = HP - damage;
        if (HP == 0)
        {
            // DEATH
        }
    }

    internal void Heal(int healPoints)
    {
        if (HP+healPoints > MaxHP)
        {
            HP = MaxHP;
        }
        else
        {
            HP += healPoints;
        }
    }

    protected void Attack()
    {
        // ATTACK
    }


}