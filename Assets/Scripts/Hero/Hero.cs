using System;
using UnityEngine;

public class Hero : Entity
{
    [Header("Hero Settings")]
    [SerializeField] private bool _gunMode = false;
    [SerializeField] private float _animSpeed = 1f;
    [SerializeField] private float _staminaSpendingSpeed = 1f;
    [SerializeField] private float _staminaRestoreSpeed = 1f;
    [SerializeField] private HeroBar _bar;



    private Animator _anim;

    private Vector3 _moveTo;
    void Start()
    {
        _anim = FindAnyObjectByType<Animator>();
    }

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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _gunMode = !_gunMode;
            _anim.SetBool("GunMode", _gunMode);
            if (_gunMode)
            {
                _animSpeed -= 0.1f;
                _anim.speed = _anim.speed;
            }
            else
            {
                _animSpeed += 0.1f;
                _anim.speed = _anim.speed;
                _anim.SetFloat("LookHorizontal", 0);
                _anim.SetFloat("LookVertical", 0);
            }
        }
    }

    private void FixedUpdate()
    {

        if (_gunMode)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 direction = mousePosition - transform.position;

            float angle = Vector3.Angle(Vector3.up, direction);

            Vector3 cross = Vector3.Cross(Vector3.up, direction);
            if (cross.z < 0)
            {
                angle = 360 - angle;
            }

            if (angle >= 315 || angle < 45)
            {
                _anim.SetFloat("LookVertical", 1);

                _anim.SetFloat("LookHorizontal", 0);
            }
            else if (angle >= 45 && angle < 135)
            {
                _anim.SetFloat("LookHorizontal", -1);

                _anim.SetFloat("LookVertical", 0);
            }
            else if (angle >= 135 && angle < 225)
            {
                _anim.SetFloat("LookVertical", -1);

                _anim.SetFloat("LookHorizontal", 0);
            }
            else
            {

                _anim.SetFloat("LookHorizontal", 1);

                _anim.SetFloat("LookVertical", 0);
            }
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
        if (_gunMode)
        {
            _anim.SetFloat("HorizontalMove", Input.GetAxisRaw("Horizontal"));
            _anim.SetFloat("VerticalMove", Input.GetAxisRaw("Vertical"));

            if (Input.GetAxisRaw("Debug Multiplier") > 0) //Если нажат shift, бежим
            {
                //_anim.speed = -_animSpeed * 1.5f;
                Stamina -= _staminaSpendingSpeed;
                base.Run(_moveTo);
            }
            else
            {
                //_anim.speed = -_animSpeed;
                base.Walk(_moveTo);
            }
        }
        else
        {
            _anim.SetFloat("HorizontalMove", Input.GetAxisRaw("Horizontal"));
            _anim.SetFloat("VerticalMove", Input.GetAxisRaw("Vertical"));

            if (Input.GetAxisRaw("Debug Multiplier") > 0 && Stamina > 0) //Если нажат shift, бежим
            {
                _anim.speed = _animSpeed * 1.5f;
                base.Run(_moveTo);
            }
            else
            {
                _anim.speed = _animSpeed;
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
