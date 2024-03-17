using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Player Movement Settings")]
    [SerializeField] internal float Speed = 3.0f;
    [SerializeField] internal float RunSpeed = 5.0f;
    [SerializeField] internal int HP = 10;

    void Start()
    {
    
    }
    
    internal virtual void Walk(Vector3 moveTarget)
    {
        transform.position = Vector3.MoveTowards(transform.position, moveTarget, Time.deltaTime * Speed);
    }

    internal virtual void Run(Vector3 moveTarget)
    {
        transform.position = Vector3.MoveTowards(transform.position, moveTarget, Time.deltaTime * RunSpeed);
    }



    internal void Damage(int damage)
    {
        HP -= damage;
        if (HP == 0)
        {
            // DEATH
        }
    }

    internal void Heal(int healPoints)
    {
        HP += healPoints;
    }

    internal void Attack()
    {
        // ATTACK
    }


}