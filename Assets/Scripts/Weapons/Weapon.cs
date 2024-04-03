using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject _ammo;
    [SerializeField] private Transform _transform;
    [SerializeField] private Transform _weaponPivot;
    [SerializeField] private float bulletForce = 20f;
    [SerializeField] private Camera _cam;
    [SerializeField] private int _clipCapacity = 30;
    [SerializeField] private float _burstDelay = 0.5f;
    private int _ammoInClip;
    [SerializeField] private float _burstTimer = 0f;
    private System.Random rand = new System.Random();
    private FireMode _fireMode;
    private enum FireMode
    {
        single,
        burst
    }


    private void Awake()
    {
        _ammoInClip = _clipCapacity;
    }

    void Update()
    {
        _burstTimer -= 0.1f;

        RotateAroundHero();
        if (_ammoInClip > 0 && _burstTimer <= 0)
        {
            if (_fireMode == FireMode.single && Input.GetButtonDown("Fire1"))
            {
                Fire();
            }
            else if (_fireMode == FireMode.burst && Input.GetButton("Fire1"))
            {
                Fire();
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            ChangeFireMode();
        }
    }

    protected virtual void Fire()
    {
        _burstTimer = _burstDelay;
        _ammoInClip -= 1;
        var bullet = Instantiate(_ammo, _transform.position, _transform.rotation);
        var rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(_transform.up * (bulletForce + rand.Next(0, 10)), ForceMode2D.Impulse); ;
        Destroy(bullet, 4f);
    }

    protected virtual void ChangeFireMode()
    {
        switch (_fireMode)
        {
            case FireMode.single:
                _fireMode = FireMode.burst;
                break;
            case FireMode.burst:
                _fireMode = FireMode.single;
                break;

        }
    }

    protected void Reload()
    {
        _ammoInClip = _clipCapacity;
    }

    protected void RotateAroundHero()
    {
        var lookDir = _cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
