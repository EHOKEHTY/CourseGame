using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Entity Movement Settings")]
    [SerializeField] protected float Speed = 3.0f;
    [SerializeField] protected float RunSpeed = 5.0f;
    [SerializeField] protected float StaminaRestoreDelay = 10.0f;
    private float Timer = 10.0f;
    Time t = new Time();

    [Header("Entity Stats Settings")]
    [SerializeField] internal int HP = 100;
    [SerializeField] internal float Stamina = 100;
    [SerializeField] internal int MaxHP = 100;
    [SerializeField] internal int MaxStamina = 100;


    protected virtual void Walk(Vector3 moveTarget)
    {
        transform.position = Vector3.MoveTowards(transform.position, moveTarget, Time.deltaTime * Speed);
    }

    protected virtual void Run(Vector3 moveTarget)
    {
        transform.position = Vector3.MoveTowards(transform.position, moveTarget, Time.deltaTime * RunSpeed);
        Debug.Log(t.ToString());
    }

    internal void Damage(int damage)
    {
        if (HP - damage > 0)
        {
            HP = HP - damage;
        }
        else
        {
            HP = 0;
            Destroy(this.gameObject);
        }
    }

    internal void Heal(int healPoints)
    {
        if (HP + healPoints > MaxHP)
        {
            HP = MaxHP;
        }
        else
        {
            HP += healPoints;
        }
    }

    internal void StaminaOff(float staminaPoints)
    {
        if (Stamina - staminaPoints > 0)
        {
            Stamina -= staminaPoints;
        }
        else
        {
            Stamina = 0;
        }
    }

    internal void StaminaRestore(float staminaPoints)
    {
        if (Stamina + staminaPoints > MaxStamina)
        {
            Stamina = MaxStamina;
        }
        else
        {
            Stamina += staminaPoints;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bullet")
        {
            Damage(10); //!!!!!!!!!
        }
    }
}