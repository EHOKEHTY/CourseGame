using System;
using UnityEngine;

public class Hero : Entity
{
    [SerializeField] public bool GunMode { get; private set; } = false;
    [Header("Hero Settings")]
    [SerializeField] private float _animSpeed = 1f;
    [SerializeField] private float _staminaSpendingSpeed = 1f;
    [SerializeField] private float _staminaRestoreSpeed = 1f;
    [SerializeField] private HeroBar _bar;

    internal bool Walk_Up = false;
    internal bool Walk_Down = false;
    internal bool Walk_Left = false;
    internal bool Walk_Right = false;


    private Vector3 _moveTo;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            HandleDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            HandleHeal(10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            HandleStamina(10);

        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            HandleStaminaRestore(10);
        }

        _moveTo = new Vector3
    (
    transform.position.x + Input.GetAxisRaw("Horizontal")
    , transform.position.y + Input.GetAxisRaw("Vertical")
    , transform.position.z
    );
        HeroMove();

        if (Input.GetKeyDown(KeyCode.Alpha1)) // отдышка, когда закончилась стамина. 
        {
            GunMode = !GunMode;
            //_anim.SetBool("GunMode", GunMode);
            if (GunMode)
            {
                //_animSpeed -= 0.1f;
                //_anim.speed = _anim.speed;
            }
            else
            {
                //_animSpeed += 0.1f;
                //_anim.speed = _anim.speed;
                //_anim.SetFloat("LookHorizontal", 0);
                //_anim.SetFloat("LookVertical", 0);
            }
        }
    }

    private void FixedUpdate()
    {

        if (GunMode)
        {
            
        }

        if (Input.GetAxisRaw("Debug Multiplier") > 0 && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
        {
            HandleStamina(_staminaSpendingSpeed);
        }
        else if (Stamina < MaxStamina)
        {
            HandleStaminaRestore(_staminaRestoreSpeed);
        }
    }

    private void HeroMove()
    {
        if (GunMode)
        {
            if (Input.GetAxisRaw("Debug Multiplier") > 0 && Stamina > 0) //Если нажат shift, бежим
            {
                base.Run(_moveTo);
            }
            else
            {
                base.Walk(_moveTo);
            }
        }
        else
        {
            if (Input.GetAxisRaw("Debug Multiplier") > 0 && Stamina > 0) //Если нажат shift, бежим
            {
                base.Run(_moveTo);
            }
            else
            {
                base.Walk(_moveTo);
            }
        }
    }

    public void HandleDamage(int damage)
    {
        Damage(damage);
        _bar.HealthUpdate();
        Debug.Log("HP = " + HP);
    }

    public void HandleHeal(int heal)
    {
        Heal(heal);
        _bar.HealthUpdate();
        Debug.Log("HP = " + HP);
    }

    private void HandleStamina(float stamina)
    {
        StaminaOff(stamina);
        _bar.StaminaUpdate();
        Debug.Log("Stamina = " + Stamina);
    }

    private void HandleStaminaRestore(float stamina)
    {
        StaminaRestore(stamina);
        _bar.StaminaUpdate();
        Debug.Log("Stamina = " + Stamina);
    }


}
